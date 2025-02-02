using UnityEditor;
using System.Collections.Generic;


[CustomEditor(typeof(SolvedFlare))]
[CanEditMultipleObjects]
public class SolvedFlareEditor: Editor
{
    private List<SerializedProperty> properties;

    private void OnEnable()
    {
        string[] hiddenProperties = new string[] { "m_input", "m_output", "m_rewindMana", "m_interactMessage" }; 
        properties = EditorHelper.GetExposedProperties(this.serializedObject, hiddenProperties);
    }

    public override void OnInspectorGUI()
    {        
        foreach (SerializedProperty property in properties)
        {
            EditorGUILayout.PropertyField(property, true);
        }
        serializedObject.ApplyModifiedProperties();
    }
}