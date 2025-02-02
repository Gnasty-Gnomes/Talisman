using System;
using UnityEditor;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace AISystem.BehaviourTrees.Editor
{
    public class BTEditorWindow : EditorWindow
    {
        BTGraphView m_gView;
        BTInspectorView m_iView;

        [MenuItem("BehaviourTree/BTEditor")]
        public static void ShowBT()
        {
            BTEditorWindow window = GetWindow<BTEditorWindow>();
            window.titleContent = new GUIContent("Behaviour Tree Editor");
        }

        public void CreateGUI()
        {
            var vta = Resources.Load<VisualTreeAsset>("BehaviourTreeEditorDocument");
            vta.CloneTree(rootVisualElement);

            m_gView = rootVisualElement.Q<BTGraphView>("graph-view");
            m_gView.SelectedNode += InspectNode;

            m_iView = rootVisualElement.Q<BTInspectorView>("inspector-view");
        }

        void OnSelectionChange()
        {
            if (Selection.objects.Length is > 1 or 0)
            {
                return;
            }

            if (Selection.objects[0] is BTAsset bTAsset)
            {
                m_iView?.SetBehaviourTree(bTAsset);
                m_gView?.PopulateGraph(bTAsset);
            }
        }

        void InspectNode(Node node)
        {
            m_iView.InspectNode(node);
        }
    }
}