using UnityEditor.Experimental.GraphView;
using UnityEngine;
using BTNode = AISystem.BehaviourTrees.Node;
using UNode = UnityEditor.Experimental.GraphView.Node;

namespace AISystem.BehaviourTrees.Editor
{
    public class NodeView : UNode
    {
        public event System.Action<NodeView> m_nodeSelected;

        public BTNode m_sourceNode;
        public BTNode m_parentNode;

        public Port m_input;
        public Port m_output;

        public NodeView(BTNode node)
        {
            m_sourceNode = node;
            title = m_sourceNode.GetType().Name;

            m_input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Node));
            inputContainer.Add(m_input);

            if (node is CompositeNode or DecoratorNode)
            {
                var capacity = node is CompositeNode ? Port.Capacity.Multi : Port.Capacity.Single;
                m_output = InstantiatePort(Orientation.Horizontal, Direction.Output, capacity, typeof(Node));
                outputContainer.Add(m_output);
            }
        }

        public void SetParentNode(BTNode parentNode)
        {
            m_parentNode = parentNode;
        }

        public override void OnSelected()
        {
            base.OnSelected();
            m_nodeSelected?.Invoke(this);
        }
    }

}
