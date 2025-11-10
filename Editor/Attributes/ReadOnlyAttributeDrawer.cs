using UnityEditor;
using UnityEngine;

namespace NaxtorGames.Utilities.Attributes.EditorScripts
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public sealed class ReadOnlyAttributeDrawer : PropertyDrawer
    {
        public sealed override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            bool oldGUIState = GUI.enabled;
            GUI.enabled = false;
            _ = EditorGUI.PropertyField(position, property, label);
            GUI.enabled = oldGUIState;
        }
    }
}