using UnityEngine;
using UnityEngine.Events;

namespace NaxtorGames.Utilities.Components
{
    [AddComponentMenu(Constants.ComponentMenu.Author.MISC + "Physics Event Trigger")]
    public sealed class PhysicsEventTriggerMono : MonoBehaviour
    {
        [Tooltip("Objects that will not count as valid.")]
        [SerializeField] private GameObject[] _forbiddenObjects = System.Array.Empty<GameObject>();

        [Tooltip("Only objects with those layers are allowed.")]
        [SerializeField] private LayerMask _allowedLayers = Physics.AllLayers;
        [Tooltip("Emits 'On Invalid Trigger Event' or 'On Invalid Collision Event' when a object is not valid.")]
        [SerializeField] private bool _informAboutInvalid = false;
        [Tooltip("Valid tags for the game objects.\nWhen none is set all tags are allowed.")]
        [SerializeField] private string[] _allowedTags = System.Array.Empty<string>();
        [Tooltip("Invalid tags for game objects.\nWhen none is set no tag is disallowed.")]
        [SerializeField] private string[] _disallowedTags = System.Array.Empty<string>();

        [SerializeField] private UnityEvent<Collider> _onTriggerEnterEvent = null;
        [SerializeField] private UnityEvent<Collider> _onTriggerExitEvent = null;
        [SerializeField] private UnityEvent<Collider> _onCollisionEnterEvent = null;
        [SerializeField] private UnityEvent<Collider> _onCollisionExitEvent = null;
        [SerializeField] private UnityEvent<Collider, bool> _onInvalidTriggerEvent = null;
        [SerializeField] private UnityEvent<Collider, bool> _onInvalidCollisionEvent = null;

        public UnityEvent<Collider> OnTriggerEnterEvent => _onTriggerEnterEvent;
        public UnityEvent<Collider> OnTriggerExitEvent => _onTriggerExitEvent;
        public UnityEvent<Collider> OnCollisionEnterEvent => _onCollisionEnterEvent;
        public UnityEvent<Collider> OnCollisionExitEvent => _onCollisionExitEvent;
        public UnityEvent<Collider, bool> OnInvalidTriggerEvent => _onInvalidTriggerEvent;
        public UnityEvent<Collider, bool> OnInvalidCollisionEvent => _onInvalidCollisionEvent;

        private void Awake()
        {
            _onTriggerEnterEvent ??= new UnityEvent<Collider>();
            _onTriggerExitEvent ??= new UnityEvent<Collider>();
            _onCollisionEnterEvent ??= new UnityEvent<Collider>();
            _onCollisionExitEvent ??= new UnityEvent<Collider>();
            _onInvalidTriggerEvent ??= new UnityEvent<Collider, bool>();
            _onInvalidCollisionEvent ??= new UnityEvent<Collider, bool>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsValidObject(other.gameObject))
            {
                if (_informAboutInvalid)
                {
                    _onInvalidTriggerEvent.Invoke(other, true);

                }
                return;
            }

            _onTriggerEnterEvent.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!IsValidObject(other.gameObject))
            {
                if (_informAboutInvalid)
                {
                    _onInvalidTriggerEvent.Invoke(other, false);
                }
                return;
            }

            _onTriggerExitEvent.Invoke(other);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Collider other = collision.collider;
            if (!IsValidObject(other.gameObject))
            {
                if (_informAboutInvalid)
                {
                    _onInvalidCollisionEvent.Invoke(other, true);
                }
                return;
            }

            _onCollisionEnterEvent.Invoke(other);
        }

        private void OnCollisionExit(Collision collision)
        {
            Collider other = collision.collider;
            if (!IsValidObject(other.gameObject))
            {
                if (_informAboutInvalid)
                {
                    _onInvalidCollisionEvent.Invoke(other, true);
                }
                return;
            }

            _onCollisionExitEvent.Invoke(other);
        }

        private bool IsValidObject(GameObject gameObject)
        {
            if (!_allowedLayers.Contains(gameObject.layer))
            {
                return false;
            }

            if (!IsAllowedObject(gameObject))
            {
                return false;
            }

            if (!HasValidTag(gameObject))
            {
                return false;
            }

            return true;
        }

        private bool IsAllowedObject(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return false;
            }

            foreach (GameObject forbiddenObject in _forbiddenObjects)
            {
                if (gameObject == forbiddenObject)
                {
                    return false;
                }
            }

            return true;
        }

        private bool HasValidTag(GameObject gameObject)
        {
            foreach (string disallowedTag in _disallowedTags)
            {
                if (gameObject.CompareTag(disallowedTag))
                {
                    return false;
                }
            }

            if (_allowedTags.Length > 0)
            {
                foreach (string allowedTag in _allowedTags)
                {
                    if (gameObject.CompareTag(allowedTag))
                    {
                        return true;
                    }
                }

                return false;
            }

            return true;
        }
    }
}