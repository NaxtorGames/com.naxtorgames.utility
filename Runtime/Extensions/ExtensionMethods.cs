using MethodImpl = System.Runtime.CompilerServices.MethodImplAttribute;
using MethodImplOptions = System.Runtime.CompilerServices.MethodImplOptions;

namespace NaxtorGames
{
    public static class IntExtensions
    {
        /// <summary>
        /// Returns the absolute value of the integer.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Abs(this int value) => System.Math.Abs(value);

        /// <summary>
        /// Returns the square of the integer.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Squared(this int value) => value * value;

        /// <summary>
        /// Clamps the int to the specified minimum and maximum range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(this int value, int min, int max) => System.Math.Clamp(value, min, max);

        /// <summary>
        /// Returns the sign of the integer as int: -1 for negative, 1 for positive, 0 for zero.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(this int value) => System.Math.Sign(value);

        /// <summary>
        /// Returns the sign of the integer as int: -1.0 for negative, 1.0 for positive, 0.0 for zero.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SignF(this int value) => value > 0 ? 1.0f : value < 0 ? -1.0f : 0.0f;

        /// <summary>
        /// Determines whether the integer lies between the specified minimum and maximum values.
        /// Automatically swaps min and max if they are in the wrong order.
        /// </summary>
        /// <param name="isInclusive">If true, includes the boundaries in the comparison. Default is true.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBetween(this int value, int min, int max, bool isInclusive = true)
        {
            if (min > max)
            {
                (min, max) = (max, min);
            }

            if (isInclusive)
            {
                return (value >= min) && (value <= max);
            }
            else
            {
                return (value > min) && (value < max);
            }
        }

        /// <summary>
        /// Converts the integer to a boolean. Returns false if the value is 0; otherwise returns true.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ToBool(this int value) => value != 0;
    }

    public static class FloatExtensions
    {
        /// <summary>
        /// Returns the absolute value of the float.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Abs(this float value) => System.MathF.Abs(value);

        /// <summary>
        /// Determines whether the float is approximately zero, within a given threshold.
        /// </summary>
        /// <param name="threshold">The tolerance used to compare against zero. Default is 1E-06.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroApprox(this float value, float threshold = 1E-06f) => value.Abs() <= threshold.Abs();

        /// <summary>
        /// Returns the square of the float.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Squared(this float value) => value * value;

        /// <summary>
        /// Clamps the float to the specified minimum and maximum range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(this float value, float min, float max) => System.Math.Clamp(value, min, max);

        /// <summary>
        /// Clamps the float between 0.0 and 1.0.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp01(this float value) => System.Math.Clamp(value, 0.0f, 1.0f);

        /// <summary>
        /// Returns the sign of the float as int: -1 for negative, 1 for positive, 0 for zero.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(this float value) => System.MathF.Sign(value);

        /// <summary>
        /// Returns the sign of the float as float: -1.0 for negative, 1.0 for positive, 0.0 for zero.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SignF(this float value) => UnityEngine.Mathf.Sign(value);

        /// <summary>
        /// Determines whether the float lies between the specified minimum and maximum values.
        /// Automatically swaps min and max if they are in the wrong order.
        /// </summary>
        /// <param name="isInclusive">If true, includes the boundaries in the comparison. Default is true.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBetween(this float value, float min, float max, bool isInclusive = true)
        {
            if (min > max)
            {
                (min, max) = (max, min);
            }

            if (isInclusive)
            {
                return (value >= min) && (value <= max);
            }
            else
            {
                return (value > min) && (value < max);
            }
        }
    }

    public static class BooleanExtensions
    {
        /// <summary>
        /// Converts the boolean to an integer. Returns 1 if true; otherwise 0.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToInt(this bool value) => value ? 1 : 0;

        /// <summary>
        /// Converts the boolean to a float. Returns 1.0 if true; otherwise 0.0.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ToFloat(this bool value) => value ? 1.0f : 0.0f;
    }

    public static class StringExtensions
    {
        /// <summary>
        /// Determines whether a string is null or has only empty/invisible characters.
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }

    public static class CollectionExtensions
    {
        /// <summary>
        /// Determines whether the specified <see cref="System.Collections.Generic.IReadOnlyList{T}"/> is null or contains no elements.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this System.Collections.Generic.IReadOnlyList<T> collection)
        {
            return collection == null || collection.Count == 0;
        }

        /// <summary>
        /// Determines whether the specified index is valid for the given <see cref="IReadOnlyList{T}"/>.
        /// Returns true if the list is not null and the index is within bounds.
        /// </summary>
        public static bool IsValidIndex<T>(this System.Collections.Generic.IReadOnlyList<T> collection, int index)
        {
            if (collection == null || index < 0)
            {
                return false;
            }

            int count = collection.Count;
            return count > 0 && index < count;
        }

        /// <summary>
        /// Attempts to retrieve a non-null element at the specified index from the list.
        /// </summary>
        /// <returns>true only if the index is valid and the element is not null.</returns>
        public static bool TryGetNotNull<T>(this System.Collections.Generic.IReadOnlyList<T> collection, int index, out T element) where T : class
        {
            if (collection.IsValidIndex(index))
            {
                element = collection[index];
                return element != null;
            }
            else
            {
                element = default(T);
                return false;
            }
        }

        /// <summary>
        /// Attempts to retrieve the element at the specified index from the list.
        /// </summary>
        /// <returns>true if the index is valid.</returns>
        public static bool TryGetValue<T>(this System.Collections.Generic.IReadOnlyList<T> collection, int index, out T element) where T : struct
        {
            if (collection.IsValidIndex(index))
            {
                element = collection[index];
                return true;
            }
            else
            {
                element = default(T);
                return false;
            }
        }
    }
}
