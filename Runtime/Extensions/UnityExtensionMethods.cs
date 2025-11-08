using UnityEngine;

namespace NaxtorGames
{
    [System.Flags]
    public enum TransformResetFlags
    {
        None = 0,
        Position = 1 << 0,
        Rotation = 1 << 1,
        Scale = 1 << 2,
        PositionRotation = Position | Rotation,
        All = Position | Rotation | Scale
    }

    public static class RichTextExtensions
    {
        public static string Colorize(this string value, Color color, bool withAlpha = false)
        {
            return $"<color={color.ToHex(withAlpha)}>{value}</color>";
        }

        public static string Size(this string value, int fontSize)
        {
            return $"<size={fontSize}>{value}</size>";
        }

        public static string Bold(this string value)
        {
            return $"<b>{value}</b>";
        }

        public static string Italic(this string value)
        {
            return $"<i>{value}</i>";
        }
    }

    public static class TransformExtensions
    {
        /// <summary>
        /// Sets the transform values to its default.
        /// </summary>
        /// <param name="transform">The transform that will be affected.</param>
        /// <param name="resetOptions">The properties to reset.</param>
        /// <param name="inWorldSpace">Should the position and rotation set in local or world space. Scale can only be set in local scale.</param>
        public static void ResetTransform(this Transform transform, TransformResetFlags resetOptions, bool inWorldSpace)
        {
            bool resetPosition = (resetOptions & TransformResetFlags.Position) == TransformResetFlags.Position;
            bool resetRotation = (resetOptions & TransformResetFlags.Rotation) == TransformResetFlags.Rotation;
            bool resetScale = (resetOptions & TransformResetFlags.Scale) == TransformResetFlags.Scale;

            if (resetPosition && resetRotation)
            {
                if (inWorldSpace)
                {
                    transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                }
                else
                {
                    transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                }
            }
            else if (resetPosition)
            {
                if (inWorldSpace)
                {
                    transform.position = Vector3.zero;
                }
                else
                {
                    transform.localPosition = Vector3.zero;
                }
            }
            else if (resetRotation)
            {
                if (inWorldSpace)
                {
                    transform.rotation = Quaternion.identity;
                }
                else
                {
                    transform.localRotation = Quaternion.identity;
                }
            }

            if (resetScale)
            {
                transform.localScale = Vector3.one;
            }
        }
    }

    public static class Vector3Extensions
    {
        /// <summary>
        /// Checks whether the vector's magnitude is within the specified maximum distance.
        /// </summary>
        public static bool IsWithinDistance(this Vector3 vector, float maxDistance)
        {
            if (maxDistance <= 0.0f)
            {
                return false;
            }

            return vector.sqrMagnitude <= maxDistance.Squared();
        }

        public static Vector3 DirectionTo(this Vector3 from, Vector3 to, bool normalized = true)
        {
            Vector3 direction = to - from;
            if (normalized)
            {
                if (direction.sqrMagnitude.IsZeroApprox())
                {
                    return Vector3.forward;
                }
                else
                {
                    return direction.normalized;
                }
            }
            else
            {
                return direction;
            }
        }
    }

    public static class ColorExtensions
    {
        /// <summary>
        /// Returns a copy of the color with its red component replaced by the given value, clamped between 0 and 1.
        /// </summary>
        public static Color WithRed(this Color color, float red)
        {
            color.r = red.Clamp01();
            return color;
        }

        /// <summary>
        /// Returns a copy of the color with its green component replaced by the given value, clamped between 0 and 1.
        /// </summary>
        public static Color WithGreen(this Color color, float green)
        {
            color.g = green.Clamp01();
            return color;
        }

        /// <summary>
        /// Returns a copy of the color with its blue component replaced by the given value, clamped between 0 and 1.
        /// </summary>
        public static Color WithBlue(this Color color, float blue)
        {
            color.b = blue.Clamp01();
            return color;
        }

        /// <summary>
        /// Returns a copy of the color with its alpha (transparency) component replaced by the given value, clamped between 0 and 1.
        /// </summary>
        public static Color WithAlpha(this Color color, float alpha)
        {
            color.a = alpha.Clamp01();
            return color;
        }

        /// <summary>
        /// Returns a copy of the color with any combination of red, green, blue, and alpha components replaced by the given values.
        /// Each component is optional and clamped between 0 and 1 if provided.
        /// </summary>
        public static Color WithRGBA(this Color color, float? red = null, float? green = null, float? blue = null, float? alpha = null)
        {
            if (red.HasValue)
            {
                color.r = red.Value.Clamp01();
            }
            if (green.HasValue)
            {
                color.g = green.Value.Clamp01();
            }
            if (blue.HasValue)
            {
                color.b = blue.Value.Clamp01();
            }
            if (alpha.HasValue)
            {
                color.a = alpha.Value.Clamp01();
            }

            return color;
        }

        /// <summary>
        /// Returns a copy of the color with its hue component replaced by the given value (0–1), preserving alpha.
        /// </summary>
        public static Color WithHue(this Color color, float hue)
        {
            Color.RGBToHSV(color, out _, out float saturation, out float value);
            float alpha = color.a;
            color = Color.HSVToRGB(hue, saturation, value);
            color.a = alpha;
            return color;
        }

        /// <summary>
        /// Returns a copy of the color with its saturation component replaced by the given value (0–1), preserving alpha.
        /// </summary>
        public static Color WithSaturation(this Color color, float saturation)
        {
            Color.RGBToHSV(color, out float hue, out _, out float value);
            float alpha = color.a;
            color = Color.HSVToRGB(hue, saturation, value);
            color.a = alpha;
            return color;
        }

        /// <summary>
        /// Returns a copy of the color with its value (brightness) component replaced by the given value (0–1), preserving alpha.
        /// </summary>
        public static Color WithValue(this Color color, float value)
        {
            Color.RGBToHSV(color, out float hue, out float saturation, out _);
            float alpha = color.a;
            color = Color.HSVToRGB(hue, saturation, value);
            color.a = alpha;
            return color;
        }

        /// <summary>
        /// Returns a copy of the color with its brightness (value) component replaced by the given value (0–1), preserving alpha.
        /// </summary>
        public static Color WithBrightness(this Color color, float brightness)
        {
            return color.WithValue(brightness);
        }

        /// <summary>
        /// Returns a copy of the color with any combination of hue, saturation, value, and alpha replaced by the given values.
        /// Each component is optional and clamped between 0 and 1 if provided.
        /// </summary>
        public static Color WithHSVA(this Color color, float? hue = null, float? saturation = null, float? value = null, float? alpha = null)
        {
            Color.RGBToHSV(color, out float h, out float s, out float v);
            float oldAlpha = color.a;

            color = Color.HSVToRGB(
                (hue ?? h).Clamp01(),
                (saturation ?? s).Clamp01(),
                (value ?? v).Clamp01());

            color.a = (alpha ?? oldAlpha).Clamp01();

            return color;
        }

        /// <summary>
        /// Extracts the color's hue, saturation, value (brightness), and alpha components as separate float values.
        /// </summary>
        public static void ToHSVA(this Color color, out float hue, out float saturation, out float value, out float alpha)
        {
            Color.RGBToHSV(color, out hue, out saturation, out value);
            alpha = color.a;
        }

        /// <summary>
        /// Converts the color to a hexadecimal string representation.
        /// Includes the alpha channel if <paramref name="withAlpha"/> is true.
        /// Adds a leading '#' if <paramref name="includeHash"/> is true.
        /// </summary>
        public static string ToHex(this Color color, bool withAlpha, bool includeHash = true)
        {
            string hex = withAlpha ?
                ColorUtility.ToHtmlStringRGBA(color) :
                ColorUtility.ToHtmlStringRGB(color);

            return includeHash ? $"#{hex}" : hex;
        }
    }

    public static class LayerMaskExtensions
    {
        /// <summary>
        /// Checks whether the specified layer is included in the given LayerMask.
        /// </summary>
        /// <param name="layerMask">The LayerMask to check against.</param>
        /// <param name="compareLayer">The layer index to compare.</param>
        /// <returns>True if the layer is contained within the LayerMask; otherwise, false.</returns>
        public static bool Contains(this LayerMask layerMask, int compareLayer)
        {
            return ((1 << compareLayer) & layerMask.value) != 0;
        }
    }
}