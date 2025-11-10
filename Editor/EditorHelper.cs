using UnityEngine;
using UnityEditor;

namespace NaxtorGames.Utilities.EditorScripts
{
    public static class EditorHelper
    {
        private const string PROPERTY_NAME_SCRIPT = "m_Script";

        private static GUIStyle s_containerStyle = null;
        private static GUIStyle s_boxStyle = null;
        private static GUIStyle s_headerLabelStyle = null;

        public static GUIStyle ContainerStyle
        {
            get
            {
                return s_containerStyle ??= new GUIStyle(GUI.skin.window)
                {
                    margin = new RectOffset(2, 2, 4, 4),
                    padding = new RectOffset(4, 4, 4, 4)
                };
            }
        }
        public static GUIStyle BoxStyle
        {
            get
            {
                return s_boxStyle ??= new GUIStyle(GUI.skin.box)
                {
                    margin = new RectOffset(2, 2, 4, 4),
                    padding = new RectOffset(4, 4, 4, 4)
                };
            }
        }
        public static GUIStyle HeaderLabelStyle
        {
            get
            {
                return s_headerLabelStyle ??= new GUIStyle(EditorStyles.boldLabel)
                {
                    richText = true
                };
            }
        }

        public static SerializedProperty GetSerializedProperty(Object unityObject, string propertyPath)
        {
            SerializedObject serializedPropertyField = GetSerializedObject(unityObject);
            return GetSerializedProperty(serializedPropertyField, propertyPath);
        }

        public static SerializedProperty GetSerializedProperty(SerializedObject serializedObject, string propertyPath)
        {
            return serializedObject.FindProperty(propertyPath);
        }

        public static SerializedObject GetSerializedObject(Object serializingObject)
        {
            return new SerializedObject(serializingObject);
        }

        public static bool DrawProperty(Object unityObject, string propertyPath, bool includeChildren = false)
        {
            SerializedObject serializedObject = GetSerializedObject(unityObject);

            return DrawProperty(serializedObject, propertyPath, includeChildren);
        }

        public static bool DrawProperty(SerializedObject serializedObject, string propertyPath, bool includeChildren = false)
        {
            serializedObject.Update();
            bool state = EditorGUILayout.PropertyField(serializedObject.FindProperty(propertyPath), includeChildren);
            _ = serializedObject.ApplyModifiedProperties();

            return state;
        }

        public static bool DrawProperty(SerializedProperty serializedProperty, bool includeChildren = false)
        {
            return EditorGUILayout.PropertyField(serializedProperty, includeChildren);
        }

        public static void DrawHeader(string text, bool spaceAbove = true, bool spaceBelow = false)
        {
            if (spaceAbove)
            {
                DrawSpace(3.0f);
            }

            EditorGUILayout.LabelField(text, HeaderLabelStyle);

            if (spaceBelow)
            {
                DrawSpace(3.0f);
            }
        }

        public static bool DrawFoldoutHeader(bool state, string text, string tooltip = null)
        {
            return EditorGUILayout.Foldout(state, new GUIContent(text, tooltip), true, EditorStyles.foldoutHeader);
        }

        public static void DrawSpace(float pixel = 6.0f)
        {
            EditorGUILayout.Space(pixel);
        }

        public static void DrawScript(SerializedObject serializedObject)
        {
            GUI.enabled = false;
            _ = EditorGUILayout.PropertyField(serializedObject.FindProperty(PROPERTY_NAME_SCRIPT));
            DrawSpace(0.3f);
            GUI.enabled = true;
        }
    }
}