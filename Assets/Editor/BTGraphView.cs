using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

using BTNode = AISystem.BehaviourTrees.Node;
using UNode = UnityEditor.Experimental.GraphView.Node;

namespace AISystem.BehaviourTrees.Editor
{
    public class BTGraphView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BTGraphView, GraphView.UxmlTraits> { }

        public event System.Action<Node> SelectedNode;

        BTAsset m_bTAsset;
        Dictionary<Node, NodeView> m_nodeViewLUT = new();
        NodeView m_rootNode;

        public BTGraphView()
        {
            var styleSheet = Resources.Load<StyleSheet>("BehaviourTreeEditorStyle");
            styleSheets.Add(styleSheet);
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            if (m_bTAsset == null)
            {
                evt.menu.AppendAction("No BT Asset Selected.", a => { });
                return;
            }

            var validTypes = TypeCache.GetTypesDerivedFrom<BTNode>().Where(t => t.IsAbstract == false);
            foreach (var nodeType in validTypes)
            {
                evt.menu.AppendAction(nodeType.Name, action => AddNode(nodeType));
            }
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.Where(other => other.direction != startPort.direction && other.node != startPort.node).ToList();
        }

        public void PopulateGraph(BTAsset bTAsset)
        {
            graphViewChanged -= OnGraphViewChanged;

            DeleteElements(graphElements);
            m_nodeViewLUT.Clear();

            m_bTAsset = bTAsset;
            var bt = bTAsset.m_behaviourTree;
            var editorData = m_bTAsset.m_nodeData;

            if (bt == null || bt.m_nodes == null || bt.m_nodes.Length == 0)
            {
                graphViewChanged += OnGraphViewChanged;
                return;
            }

            if (editorData == null || bt.m_nodes.Length != editorData.Count)
            {
                Debug.LogError("Incorrect BT editor data. May be incorrect format");
            }

            LoadNodeViews(bt, editorData);
            m_rootNode = m_nodeViewLUT[bt.m_parent];
            LoadNodeEdges(bt);

            graphViewChanged += OnGraphViewChanged;
        }

        void OnNodeSelected(NodeView nodeView)
        {
            SelectedNode?.Invoke(nodeView.m_sourceNode);
        }

        void LoadNodeEdges(BehaviourTree bt)
        {
            foreach (var node in bt.m_nodes)
            {
                if (node is CompositeNode composite)
                {
                    var parentNodeView = m_nodeViewLUT[composite];
                    foreach (var child in composite.m_children)
                    {
                        if (child == null)
                        {
                            continue;
                        }
                        var childView = m_nodeViewLUT[child];
                        childView.SetParentNode(parentNodeView.m_sourceNode);
                        var edge = parentNodeView.m_output.ConnectTo(childView.m_input);
                        AddElement(edge);
                    }
                }
                if (node is DecoratorNode decorator)
                {
                    if (decorator.m_child != null)
                    {
                        var parentNodeView = m_nodeViewLUT[decorator];
                        var childNodeView = m_nodeViewLUT[decorator.m_child];
                        var edge = parentNodeView.m_output.ConnectTo(childNodeView.m_input);
                        AddElement(edge);
                    }
                }
            }
        }

        void LoadNodeViews(BehaviourTree bt, List<NodeData> editorData)
        {
            for (int i = 0; i < bt.m_nodes.Length; i++)
            {
                NodeView nodeView = CreateNodeView(bt.m_nodes[i]);
                if (editorData == null || editorData.Count <= i)
                {
                    continue;
                }

                NodeData nodeData = editorData[i];
                nodeView.SetPosition(nodeData.m_position);
            }
        }

        GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.movedElements != null)
            {
                ProcessMovement(graphViewChange.movedElements);
            }
            if (graphViewChange.elementsToRemove != null)
            {
                ProcessRemovals(graphViewChange.elementsToRemove);
            }
            if (graphViewChange.edgesToCreate != null)
            {
                ProcessEdgeChange(graphViewChange.edgesToCreate);
            }

            RebuildBehaviourTree();
            return graphViewChange;
        }

        void ProcessMovement(List<GraphElement> movedElements)
        {
            foreach (GraphElement movedElement in movedElements)
            {
                if (movedElement is not NodeView nodeView)
                {
                    continue;
                }

                if (nodeView.m_parentNode is CompositeNode composite)
                {
                    composite.m_children = composite.m_children.OrderBy(x => m_nodeViewLUT[x].GetPosition().y).ToArray();
                }
            }
        }

        void ProcessRemovals(List<GraphElement> elementsToRemove)
        {
            foreach (GraphElement element in elementsToRemove)
            {
                if (element is NodeView nodeView)
                {
                    RemoveNodeView(nodeView);
                }
                else if (element is Edge edge)
                {
                    RemoveChild(edge.output.node as NodeView, edge.input.node as NodeView);
                }
            }
        }

        void ProcessEdgeChange(List<Edge> edgesToCreate)
        {
            foreach (Edge edge in edgesToCreate)
            {
                AddChild(edge.output.node as NodeView, edge.input.node as NodeView);
            }
        }

        void RebuildBehaviourTree()
        {
            if (m_rootNode == null)
            {
                return;
            }

            List<Node> nodes = new();
            List<NodeData> editorData = new();
            UnPackNodesRecursive(m_rootNode.m_sourceNode);

            void UnPackNodesRecursive(Node parent)
            {
                nodes.Add(parent);
                editorData.Add(BuildNodeData(parent));
                if (parent is CompositeNode composite)
                {
                    foreach (var child in composite.m_children)
                    {
                        UnPackNodesRecursive(child);
                    }
                }
                else if (parent is DecoratorNode decorator)
                {
                    UnPackNodesRecursive(decorator.m_child);
                }
            }

            Undo.RecordObject(m_bTAsset, "Changed Behaviour Tree");
            m_bTAsset.m_behaviourTree = new BehaviourTree(nodes.ToArray());
            m_bTAsset.m_nodeData = editorData;
            EditorUtility.SetDirty(m_bTAsset);
        }

        NodeData BuildNodeData(Node node)
        {
            if (node == null)
            {
                return new NodeData();
            }
            if (m_nodeViewLUT.TryGetValue(node, out var nodeView))
            {
                return new NodeData()
                {
                    m_position = nodeView.GetPosition(),
                };
            }
            return new NodeData();
        }

        public NodeView CreateNodeView(Node node)
        {
            NodeView nodeView = new NodeView(node);
            nodeView.m_nodeSelected += OnNodeSelected;
            AddElement(nodeView);

            m_nodeViewLUT.Add(node, nodeView);
            m_rootNode ??= nodeView;

            return nodeView;
        }

        void AddNode(System.Type type)
        {
            if (type.IsAbstract)
            {
                return;
            }

            Node newNode = Activator.CreateInstance(type) as Node;
            CreateNodeView(newNode);
            RebuildBehaviourTree();
        }

        void RemoveNodeView(NodeView nodeView)
        {
            RemoveElement(nodeView);
        }

        void AddChild(NodeView parent, NodeView child)
        {
            if (child == m_rootNode)
            {
                m_rootNode = parent;
            }

            child.SetParentNode(parent.m_sourceNode);

            if (parent.m_sourceNode is CompositeNode compositeNode)
            {
                compositeNode.m_children = compositeNode.m_children.Append(child.m_sourceNode).ToArray();
            }
            else if (parent.m_sourceNode is DecoratorNode decoratorNode)
            {
                decoratorNode.m_child = child.m_sourceNode;
            }
        }

        void RemoveChild(NodeView parent, NodeView child)
        {
            if (parent.m_sourceNode is CompositeNode compositeNode)
            {
                compositeNode.m_children = compositeNode.m_children.Where(e => e != child.m_sourceNode).ToArray();
            }
            else if (parent.m_sourceNode is DecoratorNode decoratorNode)
            {
                decoratorNode.m_child = null;
            }
        }
    }
}