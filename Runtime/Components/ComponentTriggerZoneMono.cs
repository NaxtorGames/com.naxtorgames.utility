using System.Collections.Generic;
using UnityEngine;

namespace NaxtorGames.Utilities.Components
{
    /// <summary>
    /// A generic trigger zone that detects and tracks components of type <typeparamref name="TComponent"/> 
    /// entering and exiting a collider. Supports filtering by unique GameObjects and optional logging.
    /// </summary>
    /// <typeparam name="TComponent">The type of component to detect within the trigger zone.</typeparam>
    public abstract class ComponentTriggerZoneMono<TComponent> : MonoBehaviour where TComponent : Component
    {
        /// <summary>
        /// Internal list of components currently inside the trigger zone.
        /// </summary>
        private readonly List<TComponent> _componentsInTrigger = new List<TComponent>();
        /// <summary>
        /// Internal list of GameObjects associated with the tracked components, used for uniqueness filtering.
        /// </summary>
        private readonly List<GameObject> _gameObjectsInTrigger = new List<GameObject>();

        [Header(Constants.Script.SETTINGS + " (Trigger Zone)")]
        /// <summary>
        /// If enabled, only one component per GameObject will be tracked in the trigger zone.
        /// </summary>
        [Tooltip("If enabled, only one component per GameObject will be tracked in the trigger zone.")]
        [SerializeField] protected bool onlyUniqueGameObjects = true;
        /// <summary>
        /// Enables verbose logging when components are detected or removed.
        /// </summary>
        [Tooltip("Enables verbose logging when components are detected or removed.")]
        [SerializeField] protected bool logInfos = false;

        /// <summary>
        /// Read-only list of components currently inside the trigger zone.
        /// </summary>
        public IReadOnlyList<TComponent> ComponentsInTrigger => _componentsInTrigger;
        /// <summary>
        /// Read-only list of game objects currently inside the trigger zone.
        /// </summary>
        public IReadOnlyList<GameObject> GameObjectsInTrigger => _gameObjectsInTrigger;

        protected virtual void Reset()
        {
            if (!this.gameObject.TryGetComponent<Collider>(out Collider collider))
            {
                collider = this.gameObject.AddComponent<BoxCollider>();
            }

            collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!Utility.TryFindComponentFromCollider<TComponent>(other, out TComponent component))
            {
                return;
            }
            if (ContainsComponent(component))
            {
                if (logInfos)
                {
                    Debug.LogWarning($"A component of '{component.gameObject.name}' has already entered.", this);
                }
                return;
            }
            if (onlyUniqueGameObjects && ContainsGameObject(component.gameObject))
            {
                if (logInfos)
                {
                    Debug.LogWarning($"The game object '{component.gameObject.name}' has already entered. Only one component per game object allowed while 'OnlyUniqueGameObjects' is set to true.", this);
                }
                return;
            }

            _componentsInTrigger.Add(component);
            _gameObjectsInTrigger.Add(component.gameObject);
            OnComponentEntered(component);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!Utility.TryFindComponentFromCollider<TComponent>(other, out TComponent component)
                || !ContainsComponent(component))
            {
                return;
            }

            _ = _componentsInTrigger.Remove(component);
            _ = _gameObjectsInTrigger.Remove(component.gameObject);
            OnComponentExited(component);
        }

        /// <summary>
        /// Called when a valid component enters the trigger zone.
        /// This method can be used to implement custom behavior on entry.
        /// </summary>
        /// <param name="component">The component that entered the zone.</param>
        protected virtual void OnComponentEntered(TComponent component) { }
        /// <summary>
        /// Called when a tracked component exits the trigger zone.
        /// This method can be used to implement custom behavior on exit.
        /// </summary>
        /// <param name="component">The component that exited the zone.</param>
        protected virtual void OnComponentExited(TComponent component) { }

        /// <summary>
        /// Checks whether the specified component is currently tracked inside the trigger zone.
        /// </summary>
        /// <param name="component">The component to check.</param>
        /// <returns>True if the component is tracked; otherwise, false.</returns>
        protected bool ContainsComponent(TComponent component)
        {
            if (component == null)
            {
                return false;
            }

            return _componentsInTrigger.Contains(component);
        }

        /// <summary>
        /// Checks whether the specified GameObject is currently associated with a tracked component.
        /// </summary>
        /// <param name="gameObject">The GameObject to check.</param>
        /// <returns>True if the GameObject is tracked; otherwise, false.</returns>
        protected bool ContainsGameObject(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return false;
            }

            return _gameObjectsInTrigger.Contains(gameObject);
        }
    }
}