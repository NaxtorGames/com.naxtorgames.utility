using UnityEngine;

namespace NaxtorGames.Utilities.Components
{
    [AddComponentMenu(Constants.ComponentMenu.Author.MISC + "LOD Bias Calculator")]
    public sealed class LodBiasCalculatorMono : MonoBehaviour
    {
        [Header(Constants.Script.REFERENCES)]
        [SerializeField] private LODGroup[] _lodGroups = System.Array.Empty<LODGroup>();
        [Header(Constants.Script.SETTINGS)]
        [SerializeField] private float _refSize = 100.0f;
        [SerializeField, Range(0.0f, 1.0f)] private float _lod0Bias = 0.5f;
        [SerializeField, Range(0.0f, 1.0f)] private float _lod1Bias = 0.25f;
        [SerializeField, Range(0.0f, 1.0f)] private float _lod2Bias = 0.175f;
        [SerializeField, Range(0.0f, 1.0f)] private float _cullBias = 0.1f;

        public float Lod0Bias
        {
            get => _lod0Bias;
            set => _lod0Bias = Mathf.Clamp01(value);
        }
        public float Lod1Bias
        {
            get => _lod1Bias;
            set => _lod1Bias = Mathf.Clamp01(value);
        }
        public float Lod2Bias
        {
            get => _lod2Bias;
            set => _lod2Bias = Mathf.Clamp01(value);
        }
        public float CullBias
        {
            get => _cullBias;
            set => _cullBias = Mathf.Clamp01(value);
        }

        private void Reset()
        {
            CollectChildLods();
        }

        [ContextMenu("Calculate Bias")]
        public void CalculateBias()
        {
            if (_lodGroups.IsNullOrEmpty())
            {
                return;
            }

            int groupId = -1;
            bool notPlaying = !Application.isPlaying;
#if UNITY_EDITOR
            if (notPlaying)
            {
                string biasInfo = $"LOD0: {_lod0Bias * 100:N1}% | LOD1: {_lod1Bias * 100:N1}% | LOD2: {_lod2Bias * 100:N1}% | Cull: {_cullBias * 100:N1}% ";
                UnityEditorActions.UndoStartRecording($"Set LOD bias: {biasInfo}", out groupId);
            }
#endif

            for (int i = 0; i < _lodGroups.Length; i++)
            {
                LODGroup lodGroup = _lodGroups[i];
                if (lodGroup == null)
                {
                    continue;
                }
#if UNITY_EDITOR
                if (notPlaying)
                {
                    UnityEditorActions.UndoRecordObject(lodGroup, $"Set LOD bias '{lodGroup.gameObject.name}'");
                }
#endif

                float size = lodGroup.size;
                float biasSize = size / _refSize;

                LOD[] lods = lodGroup.GetLODs();

                if (lods.Length == 0)
                {
                    continue;
                }

                if (lods.Length > 0)
                {
                    lods[0].screenRelativeTransitionHeight = biasSize * _lod0Bias;
                }
                if (lods.Length > 1)
                {
                    lods[1].screenRelativeTransitionHeight = biasSize * _lod1Bias;
                }
                if (lods.Length > 2)
                {
                    lods[2].screenRelativeTransitionHeight = biasSize * _lod2Bias;
                }

                lods[lods.Length - 1].screenRelativeTransitionHeight = biasSize * _cullBias;

                lodGroup.SetLODs(lods);

#if UNITY_EDITOR
                if (notPlaying)
                {
                    UnityEditorActions.SetDirty(lodGroup);
                }
#endif
            }

#if UNITY_EDITOR
            if (notPlaying)
            {
                UnityEditorActions.UndoEndRecording(groupId);
            }
#endif
        }

        public void UpdateSizes()
        {
            if (_lodGroups.IsNullOrEmpty())
            {
                return;
            }

            int groupId = -1;
            bool notPlaying = !Application.isPlaying;
#if UNITY_EDITOR
            if (notPlaying)
            {
                UnityEditorActions.UndoStartRecording("Calculate LOD sizes", out groupId);
            }
#endif

            for (int i = 0; i < _lodGroups.Length; i++)
            {
                LODGroup lodGroup = _lodGroups[i];
                if (lodGroup == null)
                {
                    continue;
                }
#if UNITY_EDITOR
                if (notPlaying)
                {
                    UnityEditorActions.UndoRecordObject(lodGroup, $"Calculate LOD size '{lodGroup.gameObject.name}'");
                }
#endif

                lodGroup.RecalculateBounds();

#if UNITY_EDITOR
                if (notPlaying)
                {
                    UnityEditorActions.SetDirty(lodGroup);
                }
#endif
            }

#if UNITY_EDITOR
            if (notPlaying)
            {
                UnityEditorActions.UndoEndRecording(groupId);
            }
#endif
        }

        [ContextMenu("Collect Child LODs")]
        public void CollectChildLods()
        {
            _lodGroups = this.gameObject.GetComponentsInChildren<LODGroup>(true);
        }
    }
}
