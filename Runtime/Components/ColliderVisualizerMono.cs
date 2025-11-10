using System.Collections.Generic;
using UnityEngine;

namespace NaxtorGames.Utilities.Components
{
    /// <summary>
    /// Draws gizmo shapes for assigned colliders.
    /// <para>NOTE: On builds, this class is empty.</para>
    /// </summary>
    [AddComponentMenu(Constants.ComponentMenu.Author.MISC + "Collider Visualizer")]
    public sealed class ColliderVisualizerMono : MonoBehaviour
    {
#if UNITY_EDITOR
        private enum DrawSituationMode
        {
            None,
            WhenSelected,
            Always
        }

        private enum DrawMode
        {
            None,
            Wired,
            Solid
        }

        [Header(Constants.Script.REFERENCES)]
        [Tooltip("Colliders to draw. (Wheel Collider not supported.)")]
        [SerializeField] private List<Collider> _collider = new List<Collider>();

        [Header(Constants.Script.SETTINGS)]
        [Tooltip("How the collider should be drawn.")]
        [SerializeField] private DrawMode _drawMode = DrawMode.Solid;
        [Tooltip("When the collider should be drawn.")]
        [SerializeField] private DrawSituationMode _drawSituation = DrawSituationMode.Always;
        [Tooltip("The color for the gizmo when the collider is enabled.")]
        [SerializeField] private Color _debugColorEnabled = new Color(0.0f, 1.0f, 0.0f, 0.2f);
        [Tooltip("The color for the gizmo when the collider is disabled.")]
        [SerializeField] private Color _debugColorDisabled = new Color(0.5f, 1.0f, 0.5f, 0.1f);
        [Tooltip("The color for the gizmo when the collider is selected (Independent of enabled or disabled collider).")]
        [SerializeField] private Color _debugColorSelected = new Color(0.8f, 0.2f, 0.8f, 0.2f);
        [Tooltip("The amount of spheres drawn for capsule colliders.")]
        [SerializeField, Range(0, 10)] private int _capsuleSphereDensity = 2;

        private void Reset()
        {
            CollectCollider();
        }

        private void OnDrawGizmosSelected()
        {
            if (_drawSituation == DrawSituationMode.WhenSelected)
            {
                DrawCollider(false);
            }
        }

        private void OnDrawGizmos()
        {
            if (_drawSituation == DrawSituationMode.Always)
            {
                bool drawAsSelected = UnityEditorActions.IsInSelection(this.gameObject);
                DrawCollider(drawAsSelected);
            }
        }
#endif

        public void AddCollider(Collider colliderToAdd)
        {
#if UNITY_EDITOR
            if (colliderToAdd == null)
            {
                return;
            }

            if (!_collider.Contains(colliderToAdd))
            {
                _collider.Add(colliderToAdd);
            }
            else
            {
                Debug.LogWarning($"Collider '{colliderToAdd.gameObject.name}' already exists.");
            }
#endif
        }

#if UNITY_EDITOR
        private void DrawCollider(bool drawAsSelected)
        {
            if (_drawMode == DrawMode.None)
            {
                return;
            }

            foreach (Collider collider in _collider)
            {
                DrawCollider(collider, drawAsSelected);
            }
        }

        private void DrawCollider(Collider collider, bool drawAsSelected)
        {
            if (collider == null)
            {
                return;
            }

            Gizmos.color = drawAsSelected
                ? _debugColorSelected : collider.enabled
                    ? _debugColorEnabled : _debugColorDisabled;
            Gizmos.matrix = this.transform.localToWorldMatrix;

            if (collider is SphereCollider sphereCollider)
            {
                if (_drawMode == DrawMode.Solid)
                {
                    Gizmos.DrawSphere(sphereCollider.center, sphereCollider.radius);
                }
                else if (_drawMode == DrawMode.Wired)
                {
                    Gizmos.DrawWireSphere(sphereCollider.center, sphereCollider.radius);
                }
            }
            else if (collider is BoxCollider boxCollider)
            {
                if (_drawMode == DrawMode.Solid)
                {
                    Gizmos.DrawCube(boxCollider.center, boxCollider.size);
                }
                else if (_drawMode == DrawMode.Wired)
                {
                    Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
                }
            }
            else if (collider is CapsuleCollider capsuleCollider)
            {
                Vector3 centerOffset = capsuleCollider.center;
                float radius = capsuleCollider.radius;
                float height = capsuleCollider.height;
                DrawCapsuleCollider(centerOffset, radius, height, _drawMode, _capsuleSphereDensity);
            }
            else if (collider is CharacterController characterController)
            {
                Vector3 centerOffset = characterController.center;
                float radius = characterController.radius;
                float height = characterController.height;
                DrawCapsuleCollider(centerOffset, radius, height, _drawMode, _capsuleSphereDensity);
            }
            else if (collider is MeshCollider meshCollider)
            {
                if (meshCollider.sharedMesh != null)
                {
                    if (_drawMode == DrawMode.Solid)
                    {
                        Gizmos.DrawMesh(meshCollider.sharedMesh);
                    }
                    else if (_drawMode == DrawMode.Wired)
                    {
                        Gizmos.DrawWireMesh(meshCollider.sharedMesh);
                    }
                }
            }
        }

        [ContextMenu("Collect Collider")]
        private void CollectCollider()
        {
            this.gameObject.GetComponents<Collider>(_collider);
        }

        private static void DrawCapsuleCollider(Vector3 center, float radius, float height, DrawMode drawMode, int capsuleSphereDensity = 1)
        {
            if (radius < 0.01f || height < 0.01f)
            {
                return;
            }

            System.Action<Vector3, float> sphereDraw;
            switch (drawMode)
            {
                case DrawMode.Solid:
                    sphereDraw = Gizmos.DrawSphere;
                    break;
                case DrawMode.Wired:
                    sphereDraw = Gizmos.DrawWireSphere;
                    break;
                default:
                    return;
            }

            float diameter = radius * 2.0f;
            if (height <= diameter)
            {
                sphereDraw(center, radius);
            }
            else
            {
                float halfHeight = height / 2.0f;
                float topY = center.y + halfHeight - radius;
                float bottomY = center.y - halfHeight + radius;

                sphereDraw(new Vector3(center.x, topY, center.z), radius);
                sphereDraw(new Vector3(center.x, bottomY, center.z), radius);

                float remainingSpace = topY - bottomY;
                if (remainingSpace >= radius && capsuleSphereDensity > 0)
                {
                    int numMiddleSpheres = Mathf.Max(1, Mathf.FloorToInt(remainingSpace / diameter) * capsuleSphereDensity);

                    float step = remainingSpace / (numMiddleSpheres + 1);

                    for (int i = 1; i <= numMiddleSpheres; i++)
                    {
                        float yPosition = topY - i * step;
                        sphereDraw(new Vector3(center.x, yPosition, center.z), radius);
                    }
                }
            }
        }
#endif
    }
}