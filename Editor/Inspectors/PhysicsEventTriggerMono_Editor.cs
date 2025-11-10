using UnityEditor;
using NaxtorGames.Utilities.EditorScripts;

namespace NaxtorGames.Utilities.Components.EditorScripts.CustomInspectors
{
    [CustomEditor(typeof(PhysicsEventTriggerMono))]
    [CanEditMultipleObjects()]
    public sealed class PhysicsEventTriggerMono_Editor : Editor
    {
        private const string PROPERTY_NAME_FORBIDDEN_OBJECTS = "_forbiddenObjects";

        private const string PROPERTY_NAME_ALLOWED_LAYERS = "_allowedLayers";
        private const string PROPERTY_NAME_INFORM_ABOUT_INVALID = "_informAboutInvalid";
        private const string PROPERTY_NAME_ALLOWED_TAGS = "_allowedTags";
        private const string PROPERTY_NAME_DISALLOWED_TAGS = "_disallowedTags";

        private const string PROPERTY_NAME_ON_TRIGGER_ENTER_EVENT = "_onTriggerEnterEvent";
        private const string PROPERTY_NAME_ON_TRIGGER_EXIT_EVENT = "_onTriggerExitEvent";
        private const string PROPERTY_NAME_ON_COLLISION_ENTER_EVENT = "_onCollisionEnterEvent";
        private const string PROPERTY_NAME_ON_COLLISION_EXIT_EVENT = "_onCollisionExitEvent";
        private const string PROPERTY_NAME_ON_INVALID_TRIGGER_EVENT = "_onInvalidTriggerEvent";
        private const string PROPERTY_NAME_ON_INVALID_COLLISION_EVENT = "_onInvalidCollisionEvent";

        private const string TOOLTIP_ON_TRIGGER_ENTER_EVENT = "Gets emitted when a valid collider enters this trigger.";
        private const string TOOLTIP_ON_TRIGGER_EXIT_EVENT = "Gets emitted when a valid collider exits this trigger.";
        private const string TOOLTIP_ON_COLLISION_ENTER_EVENT = "Gets emitted when a valid collider starts touching this collider.";
        private const string TOOLTIP_ON_COLLISION_EXIT_EVENT = "Gets emitted when a valid collider ends touching this collider.";
        private const string TOOLTIP_ON_INVALID_TRIGGER_EVENT = "Gets emitted when a invalid collider enters or exits this trigger.";
        private const string TOOLTIP_ON_INVALID_COLLISION_EVENT = "Gets emitted when a invalid collider starts or ends collision with this colider.";

        public override void OnInspectorGUI()
        {
            this.DrawScript();

            this.UpdateProperties();

            EditorHelper.DrawHeader(Constants.Script.REFERENCES);
            _ = this.DrawProperty(PROPERTY_NAME_FORBIDDEN_OBJECTS);

            EditorHelper.DrawHeader(Constants.Script.SETTINGS);
            _ = this.DrawProperty(PROPERTY_NAME_ALLOWED_LAYERS);
            _ = this.DrawProperty(PROPERTY_NAME_INFORM_ABOUT_INVALID);
            _ = this.DrawProperty(PROPERTY_NAME_ALLOWED_TAGS);
            _ = this.DrawProperty(PROPERTY_NAME_DISALLOWED_TAGS);

            EditorHelper.DrawHeader("Events", false, false);
            _ = EditorGUILayout.BeginVertical(EditorHelper.BoxStyle);
            EditorGUI.indentLevel++;
            _ = this.DrawEventProperty(PROPERTY_NAME_ON_TRIGGER_ENTER_EVENT, TOOLTIP_ON_TRIGGER_ENTER_EVENT);
            _ = this.DrawEventProperty(PROPERTY_NAME_ON_TRIGGER_EXIT_EVENT, TOOLTIP_ON_TRIGGER_EXIT_EVENT);
            _ = this.DrawEventProperty(PROPERTY_NAME_ON_COLLISION_ENTER_EVENT, TOOLTIP_ON_COLLISION_ENTER_EVENT);
            _ = this.DrawEventProperty(PROPERTY_NAME_ON_COLLISION_EXIT_EVENT, TOOLTIP_ON_COLLISION_EXIT_EVENT);
            _ = this.DrawEventProperty(PROPERTY_NAME_ON_INVALID_TRIGGER_EVENT, TOOLTIP_ON_INVALID_TRIGGER_EVENT);
            _ = this.DrawEventProperty(PROPERTY_NAME_ON_INVALID_COLLISION_EVENT, TOOLTIP_ON_INVALID_COLLISION_EVENT);
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();

            _ = this.ApplyChanges();
        }
    }
}
