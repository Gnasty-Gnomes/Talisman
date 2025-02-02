using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

using UEditor = UnityEditor.Editor;

namespace AISystem.BehaviourTrees.Editor
{
    public class BTInspectorView : VisualElement
    {
        public class UxmlFactory : UxmlFactory<BTInspectorView, VisualElement.UxmlTraits> { }

        BTAsset m_bTAsset;
        BTInspectorEditor m_bTInspectorEditor;

        public void SetBehaviourTree(BTAsset asset)
        {
            m_bTAsset = asset;
        }

        public void InspectNode(Node node)
        {
            Clear();

            if (m_bTInspectorEditor != null)
            {
                Object.DestroyImmediate(m_bTInspectorEditor);
            }

            int index = GetNodeIndex(node);

            m_bTInspectorEditor = UEditor.CreateEditor(m_bTAsset, typeof(BTInspectorEditor)) as BTInspectorEditor;
            if (m_bTInspectorEditor is null)
            {
                return;
            }

            m_bTInspectorEditor.SetBoundIndex(index);
            Add(m_bTInspectorEditor.CreateInspectorGUI());
        }

        int GetNodeIndex(Node node)
        {
            var bt = m_bTAsset.m_behaviourTree;
            for (int i = 0; i < bt.m_nodes.Length; ++i)
            {
                if (bt.m_nodes[i] == node)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}