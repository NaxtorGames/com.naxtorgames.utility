using UnityEngine;

using MethodImpl = System.Runtime.CompilerServices.MethodImplAttribute;
using MethodImplOptions = System.Runtime.CompilerServices.MethodImplOptions;

namespace NaxtorGames
{
    public static class Utility
    {
        /// <summary>
        /// Checks if an asset at the given path exists.
        /// <para>When a asset was found it will be unloaded afterwards.</para>
        /// </summary>
        /// <param name="resourceAssetFilePath">The resource path to the asset without asset extensions. (asset name only)</param>
        /// <param name="safeCheck">When set to true, if an asset was found it will stay loaded.</param>
        /// <returns>true if the asset exists; otherwise, false.</returns>
        public static bool DoseResourceAssetExist<T>(string resourceAssetFilePath, bool safeCheck) where T : Object
        {
            if (resourceAssetFilePath.IsNullOrWhiteSpace())
            {
                return false;
            }

            T resourceAsset = Resources.Load<T>(resourceAssetFilePath);
            bool doseExist = resourceAsset != null;

            if (doseExist && !safeCheck)
            {
                Resources.UnloadAsset(resourceAsset);
            }

            return doseExist;
        }

        /// <summary>
        /// Checks whether the vector's magnitude is within the specified maximum distance.
        /// </summary>
        public static bool IsWithinDistance(Vector3 from, Vector3 to, float maxDistance)
        {
            return (to - from).IsWithinDistance(maxDistance);
        }

        public static Vector3 GetDirection(Vector3 from, Vector3 to, bool normalized = true)
        {
            return from.DirectionTo(to, normalized);
        }

        /// <summary>
        /// Attempts to find a component of type <typeparamref name="T"/> from the given collider.
        /// Searches via attached Rigidbody first, then via parent hierarchy.
        /// </summary>
        /// <param name="collider">The collider to search from.</param>
        /// <param name="component">The found component, if any.</param>
        /// <returns>True if a component was found; otherwise, false.</returns>
        public static bool TryFindComponentFromCollider<T>(this Collider collider, out T component) where T : Component
        {
            if (collider == null)
            {
                component = null;
                return false;
            }

            if (collider.attachedRigidbody == null
                || !collider.attachedRigidbody.gameObject.TryGetComponent(out component))
            {
                component = collider.gameObject.GetComponentInParent<T>();
            }

            return component != null;
        }

        /// <summary>
        /// Calculates the ratio between a current value and its maximum.
        /// Returns 0 if the maximum is zero to avoid division by zero.
        /// Optionally clamps the result between 0 and 1.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="clamped">If true, clamps the result to the [0, 1] range.</param>
        /// <returns>The ratio of current to max, optionally clamped.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateRatio(float current, float max, bool clamped = true)
        {
            if (max == 0.0f)
            {
                return 0.0f;
            }

            float ratio = current / max;

            return clamped ? ratio.Clamp01() : ratio;
        }

        /// <summary>
        /// Calculates the ratio between a current value and its maximum.
        /// Returns 0 if the maximum is zero to avoid division by zero.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The ratio of current to max, optionally clamped.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateRatio(float current, float min, float max)
        {
            float ratio = Mathf.InverseLerp(min,max,current);

            return ratio;
        }

        /// <summary>
        /// Converts a normalized ratio into a value between a minimum and maximum.
        /// Optionally clamps the ratio to ensure the result stays within bounds.
        /// </summary>
        /// <param name="ratio">The normalized ratio to convert.</param>
        /// <param name="min">The minimum output value.</param>
        /// <param name="max">The maximum output value.</param>
        /// <param name="clamped">If true, clamps the ratio to the [0, 1] range before conversion.</param>
        /// <returns>The interpolated value between min and max based on the ratio.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EvaluateRatio(float ratio, float min, float max, bool clamped = true)
        {
            return Mathf.LerpUnclamped(min, max, clamped ? ratio.Clamp01() : ratio);
        }

        /// <summary>
        /// Converts a normalized ratio into a value between 0 and a maximum.
        /// Optionally clamps the ratio to ensure the result stays within bounds.
        /// </summary>
        /// <param name="ratio">The normalized ratio to convert.</param>
        /// <param name="max">The maximum output value.</param>
        /// <param name="clamped">If true, clamps the ratio to the [0, 1] range before conversion.</param>
        /// <returns>The scaled value between 0 and max based on the ratio.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EvaluateRatio(float ratio, float max, bool clamped = true)
        {
            return max * (clamped ? ratio.Clamp01() : ratio);
        }

        /// <summary>Safely divides a value by a divisor, with fallback behavior if the divisor is zero.</summary>
        /// <param name="value">The numerator of the division.</param>
        /// <param name="divisor">The denominator of the division.</param>
        /// <param name="fallbackToValue">If true and the divisor is zero, returns the original value; otherwise returns 0.</param>
        /// <returns>The result of value / divisor, or a fallback value if divisor is zero.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SafeDivide(float value, float divisor, bool fallbackToValue)
        {
            if (divisor == 0.0f)
            {
                return fallbackToValue ? value : 0.0f;
            }
            else
            {
                return value / divisor;
            }
        }
    }
}
