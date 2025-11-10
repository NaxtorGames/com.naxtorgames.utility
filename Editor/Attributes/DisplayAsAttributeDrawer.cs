using UnityEngine;
using UnityEditor;

namespace NaxtorGames.Utilities.Attributes.EditorScripts
{
    [CustomPropertyDrawer(typeof(DisplayAsAttribute))]
    public sealed class DisplayAsAttributeDrawer : PropertyDrawer
    {
        public sealed override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _ = EditorGUI.PropertyField(position, property, (this.attribute as DisplayAsAttribute).Display);
        }
    }
}