using UnityEditor;
using UnityEngine;

namespace NaxtorGames.Utilities.Attributes.EditorScripts
{
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public sealed class RequiredAttributeDrawer : PropertyDrawer
    {
        public sealed override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference || property.objectReferenceValue != null)
            {
                _ = EditorGUI.PropertyField(position, property, label);
                return;
            }


            const string PREFIX = "[Required Field] ";
            if (string.IsNullOrWhiteSpace(label.tooltip))
            {
                label.tooltip = PREFIX;
            }
            else if (!label.tooltip.StartsWith(PREFIX))
            {
                label.tooltip = PREFIX + label.tooltip;
            }

            Color current = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;
            _ = EditorGUI.PropertyField(position, property, label);
            GUI.backgroundColor = current;
        }
    }
}