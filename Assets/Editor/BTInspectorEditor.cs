using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace AISystem.BehaviourTrees.Editor
{ 
    public class BTInspectorEditor : UnityEditor.Editor
    {
        int m_index = -1;

        public void SetBoundIndex(int index)
        {
            m_index = index;
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement inspector = new VisualElement();
            if (m_index == -1)
            {
                inspector.Add(new Label("No Nodes Selected"));
                return inspector;
            }

            var bt = serializedObject.FindProperty("m_behaviourTree");
            var nodes = bt.FindPropertyRelative("m_nodes");
            var node = nodes.GetArrayElementAtIndex(m_index);

            inspector.Add(new Label(node.managedReferenceValue.GetType().Name));
            foreach (var property in node.FindChildrenProperties())
            {
                var propertyField = new PropertyField(property);
                propertyField.Bind(property.serializedObject);
                inspector.Add(propertyField);
            }

            return inspector;
        }


    }
    internal static class PropertyExtensions
    {
        public static IEnumerable<SerializedProperty> FindChildrenProperties(this SerializedProperty parent, 
            int depth = 1)
        {
            var depthOfParent = parent.depth;
            var enumerator = parent.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (enumerator.Current is not SerializedProperty childProperty)
                {
                    continue;
                }
                if (childProperty.depth > depthOfParent + depth)
                {
                    continue;
                }
                yield return childProperty.Copy();
            }
        }
    }
}