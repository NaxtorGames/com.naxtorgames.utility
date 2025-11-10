using UnityEngine;
using UnityEditor;

namespace NaxtorGames.Utilities.Attributes.EditorScripts
{
    [CustomPropertyDrawer(typeof(ButtonAttribute))]
    public sealed class ButtonAttributeDrawer : PropertyDrawer
    {
        public sealed override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = EditorGUIUtility.singleLineHeight;
            _ = EditorGUI.PropertyField(position, property, label);

            Rect buttonPosition = position;
            buttonPosition.y += EditorGUIUtility.singleLineHeight * 1.1f;
            buttonPosition.height = EditorGUIUtility.singleLineHeight;

            ButtonAttribute button = (ButtonAttribute)this.attribute;

            bool isClickable = button.ExecutionMode switch
            {
                ButtonExecutionMode.PlayModeOnly => Application.isPlaying,
                ButtonExecutionMode.EditModeOnly => !Application.isPlaying,
                _ => true
            };

            GUI.enabled = isClickable;

            if (GUI.Button(buttonPosition, new GUIContent(button.Label)))
            {
                Object targetObject = property.serializedObject.targetObject;
                System.Type targetType = targetObject.GetType();
                System.Reflection.MethodInfo method = targetType.GetMethod(button.MethodName);

                _ = method?.Invoke(targetObject, null);
            }

            GUI.enabled = true;
        }

        public sealed override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (EditorGUIUtility.singleLineHeight * 1.1f) + EditorGUIUtility.singleLineHeight;
        }
    }
}
