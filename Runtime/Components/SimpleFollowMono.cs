using UnityEngine;

namespace NaxtorGames.Misc
{
    [AddComponentMenu(Constants.ComponentMenu.Author.MISC + "Simple Follow")]
    public sealed class SimpleFollowMono : MonoBehaviour
    {
        [Header(Constants.Script.REFERENCES)]
        [SerializeField] private Transform _transformToFollow = null;
        [Header(Constants.Script.SETTINGS)]
        [SerializeField] private bool _disableWhenNull = true;
        [Header("Position")]
        [SerializeField] private bool _forceXPosition = false;
        [SerializeField] private float _forcedXPosition = 0.0f;
        [SerializeField] private bool _forceYPosition = false;
        [SerializeField] private float _forcedYPosition = 0.0f;
        [SerializeField] private bool _forceZPosition = false;
        [SerializeField] private float _forcedZPosition = 0.0f;
        [Header("Rotation")]
        [SerializeField] private bool _forceXRotation = false;
        [SerializeField] private float _forcedXRotation = 0.0f;
        [SerializeField] private bool _forceYRotation = false;
        [SerializeField] private float _forcedYRotation = 0.0f;
        [SerializeField] private bool _forceZRotation = false;
        [SerializeField] private float _forcedZRotation = 0.0f;

        private void OnEnable()
        {
            SetTarget(_transformToFollow);
        }

        private void LateUpdate()
        {
            if (_transformToFollow == null)
            {
                return;
            }

            _transformToFollow.GetPositionAndRotation(out Vector3 worldPosition, out Quaternion worldRotation);

            if (_forceXPosition)
            {
                worldPosition.x = _forcedXPosition;
            }
            if (_forceYPosition)
            {
                worldPosition.y = _forcedYPosition;
            }
            if (_forceZPosition)
            {
                worldPosition.z = _forcedZPosition;
            }

            Vector3 eulerRotation = worldRotation.eulerAngles;
            if (_forceXRotation)
            {
                eulerRotation.x = _forcedXRotation;
            }
            if (_forceYRotation)
            {
                eulerRotation.y = _forcedYRotation;
            }
            if (_forceZRotation)
            {
                eulerRotation.z = _forcedZRotation;
            }

            worldRotation = Quaternion.Euler(eulerRotation);
            this.transform.SetPositionAndRotation(worldPosition, worldRotation);
        }

        public void SetTarget(Transform targetTransform)
        {
            _transformToFollow = targetTransform;

            if (_disableWhenNull && _transformToFollow == null)
            {
                this.gameObject.SetActive(false);
            }
            else if (!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }
        }
    }
}
