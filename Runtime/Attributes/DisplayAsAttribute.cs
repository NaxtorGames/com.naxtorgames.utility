using UnityEngine;

namespace NaxtorGames.Utilities.Attributes
{
    /// <summary>
    /// Allows to override the default displayed name for the field.
    /// </summary>
    public sealed class DisplayAsAttribute : PropertyAttribute
    {
        public GUIContent Display { get; private set; }

        /// <summary>
        /// Overrides the name of this field.
        /// </summary>
        /// <param name="displayName">The new text displayed for this field.</param>
        /// <param name="tooltip">(Optional) A tooltip for this field. This may conflict with <see cref="UnityEngine.TooltipAttribute"/>.</param>
        public DisplayAsAttribute(string displayName, string tooltip = null)
        {
            if (string.IsNullOrWhiteSpace(displayName))
            {
                this.Display = new GUIContent("");
            }
            else if (string.IsNullOrWhiteSpace(tooltip))
            {
                this.Display = new GUIContent(displayName);
            }
            else
            {
                this.Display = new GUIContent(displayName, tooltip);
            }
        }
    }
}