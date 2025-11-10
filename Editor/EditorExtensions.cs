using UnityEditor;

namespace NaxtorGames.Utilities.EditorScripts
{
    public static class EditorExtensions
    {
        /// <summary>
        /// Draws a readonly Vector2Field with current window size.
        /// </summary>
        public static void DrawWindowSize(this EditorWindow editorWindow) => _ = EditorGUILayout.Vector2Field("Window Size:", editorWindow.position.size);

        public static bool DrawProperty(this Editor editor, string propertyPath) => EditorHelper.DrawProperty(editor.serializedObject, propertyPath);

        public static bool DrawProperty(SerializedProperty property) => EditorGUILayout.PropertyField(property);

        public static bool DrawEventProperty(this Editor editor, string propertyPath, string tooltip = null)
        {
            SerializedProperty property = editor.serializedObject.FindProperty(propertyPath);

            _ = EditorGUILayout.BeginVertical(EditorHelper.ContainerStyle);
            property.isExpanded = EditorHelper.DrawFoldoutHeader(property.isExpanded, property.displayName, tooltip);

            bool state = false;
            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;
                state = DrawProperty(property);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndVertical();

            return state;
        }

        public static void DrawScript(this Editor editor) => EditorHelper.DrawScript(editor.serializedObject);

        public static void UpdateProperties(this Editor editor) => editor.serializedObject.Update();

        public static bool ApplyChanges(this Editor editor, bool withUndo = true)
        {
            if (withUndo)
            {
                return editor.serializedObject.ApplyModifiedProperties();
            }
            else
            {
                return editor.serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }
        }
    }
}
