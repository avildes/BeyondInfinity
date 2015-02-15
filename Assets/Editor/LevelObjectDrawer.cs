using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer (typeof(LevelObject))]
public class LevelObjectDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        Rect prefabRect = new Rect(position.x, position.y, 200, position.height);
        Rect qttRect = new Rect(position.x + 220, position.y, 30, position.height);
        //Rect nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(prefabRect, property.FindPropertyRelative("prefab"), GUIContent.none);
        EditorGUI.PropertyField(qttRect, property.FindPropertyRelative("quantity"), GUIContent.none);
        //EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();

	}
}
