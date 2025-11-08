using UnityEngine;

namespace NaxtorGames
{
    /// <summary>
    /// A collection of Unity Editor methods commonly used to save changes while editing runtime objects.
    /// This class removes editor references on build without any error.
    /// </summary>
    public static class UnityEditorActions
    {
        /// <summary>
        /// Records any changes done on the object after the RecordObject function.
        /// </summary>
        /// <param name="objectToUndo">The reference to the object that you will be modifying.</param>
        /// <param name="message">The title of the action to appear in the undo history (i.e. visible in the undo menu).</param>
        public static void UndoRecordObject(Object objectToUndo, string message)
        {
#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(objectToUndo, message);
#endif
        }

        /// <summary>
        /// Registers an undo operation to undo the creation of an object.
        /// </summary>
        /// <param name="objectToUndo">
        /// The newly created object. This object is destroyed when the undo operation is
        /// performed. When the value is a GameObject, Unity registers the GameObject and
        /// its child GameObjects, but not sibling or parent GameObjects.
        /// </param>
        /// <param name="name">
        /// The name of the action to undo. Use this string to provide a short description
        /// of the action to be undone. For Undo or Redo items in the Edit menu, Unity adds
        /// "Undo" or "Redo" to the string that you provide. For example, if you provide
        /// the string "Create GameObject", the Unity Editor displays the menu item Edit
        /// > Undo Create GameObject.
        /// </param>
        public static void UndoRegisterCreatedObject(Object objectToUndo, string name)
        {
#if UNITY_EDITOR
            UnityEditor.Undo.RegisterCreatedObjectUndo(objectToUndo, name);
#endif
        }

        /// <summary>
        /// Begins a undo group.
        /// <para>Undo groups needs to be collapsed with <see cref="UnityEditorActions.UndoEndRecording(int)"/>.</para>
        /// </summary>
        /// <param name="undoGroupName">The undo group name.</param>
        /// <param name="groupIndex">The reference index of the group.</param>
        public static void UndoStartRecording(string undoGroupName, out int groupIndex)
        {
            groupIndex = -1;

#if UNITY_EDITOR
            UnityEditor.Undo.SetCurrentGroupName(undoGroupName);
            groupIndex = UnityEditor.Undo.GetCurrentGroup();
#endif
        }

        /// <summary>
        /// Ends a undo group.
        /// <para>Undo groups get started with <see cref="UnityEditorActions.UndoStartRecording(string, out int)"/></para>
        /// </summary>
        /// <param name="groupIndex">The reference index of the group provided by <see cref="UnityEditorActions.UndoStartRecording(string, out int)"/> 'groupIndex' out parameter.</param>
        public static void UndoEndRecording(int groupIndex)
        {
#if UNITY_EDITOR
            UnityEditor.Undo.CollapseUndoOperations(groupIndex);
#endif
        }

        /// <summary>
        /// Marks an object as dirty/required to be saved.
        /// </summary>
        /// <param name="target">The object to mark as dirty.</param>
        public static void SetDirty(Object target)
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(target);
#endif
        }

        /// <summary>
        /// Adds a component with undo operation.
        /// <para>When in build a component with the default <see cref="GameObject.AddComponent{T}()"/> gets added to the <paramref name="gameObject"/>.</para>
        /// </summary>
        /// <typeparam name="T">The type of component to add.</typeparam>
        /// <param name="gameObject">The game object to add the component to.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static T AddComponent<T>(GameObject gameObject) where T : Component
        {
            if (gameObject == null)
            {
                throw new System.ArgumentNullException(nameof(gameObject), "'gameObject' cannot be null.");
            }

            T component;
#if UNITY_EDITOR
            component = UnityEditor.Undo.AddComponent<T>(gameObject);
#else
            component = gameObject.AddComponent<T>();
#endif
            return component;
        }

        /// <summary>
        /// Returns whether an object is contained in the current selection.
        /// <para>In builds always <see langword="false"/> gets returned.</para>
        /// </summary>
        public static bool IsInSelection(Object target)
        {
#if UNITY_EDITOR
            if (target == null)
            {
                return false;
            }

            return UnityEditor.Selection.Contains(target.GetInstanceID());
#else
            return false;
#endif
        }

        /// <summary>
        /// Creates a text label for a handle that is positioned in 3D space.
        /// </summary>
        /// <param name="position">The matrix related position.</param>
        /// <param name="text">The text to display.</param>
        /// <param name="style">The label style (optional).</param>
        public static void DrawLabel(Vector3 position, string text, GUIStyle style = null)
        {
#if UNITY_EDITOR
            UnityEditor.Handles.Label(position, text, style);
#endif
        }

        /// <summary>
        /// Draws a line from p1 to p2.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        public static void DrawLine(Vector3 p1, Vector3 p2, Color color, float thickness = 1.0f)
        {
#if UNITY_EDITOR
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.DrawLine(p1, p2, thickness);
#endif
        }
    }
}