using UnityEngine;

namespace NaxtorGames.Utilities.Attributes
{
    /// <summary>
    /// Defines when a button in the inspector should be interactable,
    /// based on the current Unity editor or runtime state.
    /// </summary>
    public enum ButtonExecutionMode
    {
        /// <summary>
        /// The button is always available, both in edit mode and during play mode.
        /// </summary>
        Always,
        /// <summary>
        /// The button is only available while the application is running in play mode.
        /// </summary>
        PlayModeOnly,
        /// <summary>
        /// The button is only available in the Unity Editor's edit mode (when not playing).
        /// </summary>
        EditModeOnly
    }

    /// <summary>
    /// Creates a Button below a property field.
    /// </summary>
    public sealed class ButtonAttribute : PropertyAttribute
    {
        /// <summary>
        /// The text displayed on the button.
        /// </summary>
        public string Label { get; set; } = null;
        /// <summary>
        /// The <see langword="nameof"/> of the method that gets called when the button is pressed.
        /// </summary>
        public string MethodName { get; set; } = null;
        /// <summary>
        /// When the button can be pressed.
        /// </summary>
        public ButtonExecutionMode ExecutionMode { get; set; } = ButtonExecutionMode.Always;

        /// <summary>
        /// Creates a button below this field.
        /// </summary>
        /// <param name="label">The text displayed on the button.</param>
        /// <param name="methodName">The <see langword="nameof"/> of the method that gets called when the button is pressed.</param>
        /// <param name="executionMode">When the button can be pressed.</param>
        public ButtonAttribute(string label, string methodName, ButtonExecutionMode executionMode = ButtonExecutionMode.Always)
        {
            this.Label = label;
            this.MethodName = methodName;
            this.ExecutionMode = executionMode;
        }
    }
}
