using UnityEditor;
using System.Collections.Generic;


[CustomEditor(typeof(RotateCircularMana))]
[CanEditMultipleObjects]
public class RotateCircularManaEditor: Editor
{
    private List<SerializedProperty> properties;

    private void OnEnable()
    {
        string[] hiddenProperties = new string[] { "m_rewindMana", "m_bridge" }; 
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