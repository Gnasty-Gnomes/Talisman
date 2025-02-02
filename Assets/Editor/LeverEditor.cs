using UnityEditor;
using System.Collections.Generic;


[CustomEditor(typeof(Lever))]
[CanEditMultipleObjects]
public class LeverEditor : Editor
{
    private List<SerializedProperty> properties;

    private void OnEnable()
    {
        string[] hiddenProperties = new string[] { "m_input", "m_output", "m_rewindMana", "m_bridge", "m_isOn", "m_canBeInteracted" }; 
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