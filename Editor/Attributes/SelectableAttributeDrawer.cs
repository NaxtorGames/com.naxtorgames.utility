using UnityEngine;
using UnityEditor;

namespace NaxtorGames.Utilities.Attributes.EditorScripts
{
    [CustomPropertyDrawer(typeof(SelectableAttribute))]
    public sealed class SelectableAttributeDrawer : PropertyDrawer
    {
        public sealed override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference ||
                !IsNullOrRequiredType(property.objectReferenceValue))
            {
                _ = EditorGUI.PropertyField(position, property, label);
                return;
            }

            const float BUTTON_WIDTH = 60.0f;
            const string BUTTON_LABEL = "Select";
            const int SPACE = 4;

            Rect propertyRect = new Rect(position.x, position.y, position.width - SPACE - BUTTON_WIDTH, position.height);
            _ = EditorGUI.PropertyField(propertyRect, property, label);

            bool oldGUIState = GUI.enabled;
            GUI.enabled = oldGUIState && property.objectReferenceValue != null;

            Rect buttonRect = new Rect(propertyRect.x + propertyRect.width + SPACE, propertyRect.y, BUTTON_WIDTH, propertyRect.height);
            if (GUI.Button(buttonRect, BUTTON_LABEL))
            {
                Selection.activeObject = property.objectReferenceValue;
            }
            GUI.enabled = oldGUIState;
        }

        private static bool IsNullOrRequiredType(Object unityObject)
        {
            return unityObject == null || unityObject is Transform || unityObject is GameObject || unityObject is Component;
        }
    }
}