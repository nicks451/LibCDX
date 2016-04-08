﻿using System;

namespace libcdx.Math
{
    public class MathUtils
    {
        public const float nanoToSec = 1 / 1000000000f;

        // ---
        public const float FLOAT_ROUNDING_ERROR = 0.000001f; // 32 bits
        public const float PI = 3.1415927f;
        public const float PI2 = PI * 2;

        public const float E = 2.7182818f;

        private const int SIN_BITS = 14; // 16KB. Adjust for accuracy.
        private const int SIN_MASK = ~(-1 << SIN_BITS);
        private const int SIN_COUNT = SIN_MASK + 1;

        private const float radFull = PI * 2;
        private const float degFull = 360;
        private const float radToIndex = SIN_COUNT / radFull;
        private const float degToIndex = SIN_COUNT / degFull;

        /** multiply by this to convert from radians to degrees */
        public const float radiansToDegrees = 180f / PI;
        public const float radDeg = radiansToDegrees;
        /** multiply by this to convert from degrees to radians */
        public const float degreesToRadians = PI / 180;
        public const float degRad = degreesToRadians;

        /** Returns the sine in radians from a lookup table. */
        public static float Sin(float radians)
        {
            return SinClass.table[(int)(radians * radToIndex) & SIN_MASK];
        }

        /** Returns the cosine in radians from a lookup table. */
        public static float Cos(float radians)
        {
            return SinClass.table[(int)((radians + PI / 2) * radToIndex) & SIN_MASK];
        }

        /** Returns the sine in radians from a lookup table. */
        public static float SinDeg(float degrees)
        {
            return SinClass.table[(int)(degrees * degToIndex) & SIN_MASK];
        }

        /** Returns the cosine in radians from a lookup table. */
        public static float CosDeg(float degrees)
        {
            return SinClass.table[(int)((degrees + 90) * degToIndex) & SIN_MASK];
        }

        // ---

        /** Returns atan2 in radians, faster but less accurate than Math.atan2. Average error of 0.00231 radians (0.1323 degrees),
         * largest error of 0.00488 radians (0.2796 degrees). */
        public static float Atan2(float y, float x)
        {
            if (x == 0f)
            {
                if (y > 0f) return PI / 2;
                if (y == 0f) return 0f;
                return -PI / 2;
            }
            float atan, z = y / x;
            if (System.Math.Abs(z) < 1f)
            {
                atan = z / (1f + 0.28f * z * z);
                if (x < 0f) return atan + (y < 0f ? -PI : PI);
                return atan;
            }
            atan = PI / 2 - z / (z * z + 0.28f);
            return y < 0f ? atan - PI : atan;
        }

        // ---

        public static Random _random = new Random();

        /** Returns a random number between 0 (inclusive) and the specified value (inclusive). */
        public static int Random(int range)
        {
            return _random.Next(range + 1);
        }

        /** Returns a random number between start (inclusive) and end (inclusive). */
        public static int Random(int start, int end)
        {
            return start + _random.Next(end - start + 1);
        }

        /** Returns a random number between 0 (inclusive) and the specified value (inclusive). */
        public static long Random(long range)
        {
            return (long)(_random.NextDouble() * range);
        }

        /** Returns a random number between start (inclusive) and end (inclusive). */
        public static long Random(long start, long end)
        {
            return start + (long)(_random.NextDouble() * (end - start));
        }

        /** Returns a random bool value. */
        public static bool RandomBoolean()
        {
            return Convert.ToBoolean(_random.Next(1));
        }

        /** Returns true if a random value between 0 and 1 is less than the specified value. */
        public static bool RandomBoolean(float chance)
        {
            return MathUtils.Random() < chance;
        }

        /** Returns random number between 0.0 (inclusive) and 1.0 (exclusive). */
        public static float Random()
        {
            return (float)_random.NextDouble();
        }

        /** Returns a random number between 0 (inclusive) and the specified value (exclusive). */
        public static float Random(float range)
        {
            return (float)_random.NextDouble() * range;
        }

        /** Returns a random number between start (inclusive) and end (exclusive). */
        public static float Random(float start, float end)
        {
            return start + (float)_random.NextDouble() * (end - start);
        }

        /** Returns -1 or 1, randomly. */
        public static int RandomSign()
        {
            return 1 | (_random.Next() >> 31);
        }

        /** Returns a triangularly distributed random number between -1.0 (exclusive) and 1.0 (exclusive), where values around zero are
         * more likely.
         * <p>
         * This is an optimized version of {@link #randomTriangular(float, float, float) randomTriangular(-1, 1, 0)} */
        public static float RandomTriangular()
        {
            return (float)_random.NextDouble() - (float)_random.NextDouble();
        }

        /** Returns a triangularly distributed random number between {@code -max} (exclusive) and {@code max} (exclusive), where values
         * around zero are more likely.
         * <p>
         * This is an optimized version of {@link #randomTriangular(float, float, float) randomTriangular(-max, max, 0)}
         * @param max the upper limit */
        public static float RandomTriangular(float max)
        {
            return (float)(_random.NextDouble() - _random.NextDouble()) * max;
        }

        /** Returns a triangularly distributed random number between {@code min} (inclusive) and {@code max} (exclusive), where the
         * {@code mode} argument defaults to the midpoint between the bounds, giving a symmetric distribution.
         * <p>
         * This method is equivalent of {@link #randomTriangular(float, float, float) randomTriangular(min, max, (min + max) * .5f)}
         * @param min the lower limit
         * @param max the upper limit */
        public static float RandomTriangular(float min, float max)
        {
            return RandomTriangular(min, max, (min + max) * 0.5f);
        }

        /** Returns a triangularly distributed random number between {@code min} (inclusive) and {@code max} (exclusive), where values
         * around {@code mode} are more likely.
         * @param min the lower limit
         * @param max the upper limit
         * @param mode the point around which the values are more likely */
        public static float RandomTriangular(float min, float max, float mode)
        {
            float u = (float)_random.NextDouble();
            float d = max - min;
            if (u <= (mode - min) / d) return min + (float)System.Math.Sqrt(u * d * (mode - min));
            return max - (float)System.Math.Sqrt((1 - u) * d * (max - mode));
        }

        // ---

        /** Returns the next power of two. Returns the specified value if the value is already a power of two. */
        public static int NextPowerOfTwo(int value)
        {
            if (value == 0) return 1;
            value--;
            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;
            return value + 1;
        }

        public static bool IsPowerOfTwo(int value)
        {
            return value != 0 && (value & value - 1) == 0;
        }

        // ---

        public static short Clamp(short value, short min, short max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public static long Clamp(long value, long min, long max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public static float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public static double Clamp(double value, double min, double max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        // ---

        /** Linearly interpolates between fromValue to toValue on progress position. */
        public static float Lerp(float fromValue, float toValue, float progress)
        {
            return fromValue + (toValue - fromValue) * progress;
        }

        /** Linearly interpolates between two angles in radians. Takes into account that angles wrap at two pi and always takes the
         * direction with the smallest delta angle.
         * 
         * @param fromRadians start angle in radians
         * @param toRadians target angle in radians
         * @param progress interpolation value in the range [0, 1]
         * @return the interpolated angle in the range [0, PI2[ */
        public static float LerpAngle(float fromRadians, float toRadians, float progress)
        {
            float delta = ((toRadians - fromRadians + PI2 + PI) % PI2) - PI;
            return (fromRadians + delta * progress + PI2) % PI2;
        }

        /** Linearly interpolates between two angles in degrees. Takes into account that angles wrap at 360 degrees and always takes
         * the direction with the smallest delta angle.
         * 
         * @param fromDegrees start angle in degrees
         * @param toDegrees target angle in degrees
         * @param progress interpolation value in the range [0, 1]
         * @return the interpolated angle in the range [0, 360[ */
        public static float LerpAngleDeg(float fromDegrees, float toDegrees, float progress)
        {
            float delta = ((toDegrees - fromDegrees + 360 + 180) % 360) - 180;
            return (fromDegrees + delta * progress + 360) % 360;
        }

        // ---

        private const int BIG_ENOUGH_INT = 16 * 1024;
        private const double BIG_ENOUGH_FLOOR = BIG_ENOUGH_INT;
        private const double CEIL = 0.9999999;
        private const double BIG_ENOUGH_CEIL = 16384.999999999996;
        private const double BIG_ENOUGH_ROUND = BIG_ENOUGH_INT + 0.5f;

        /** Returns the largest integer less than or equal to the specified float. This method will only properly floor floats from
         * -(2^14) to (Float.MAX_VALUE - 2^14). */
        public static int Floor(float value)
        {
            return (int)(value + BIG_ENOUGH_FLOOR) - BIG_ENOUGH_INT;
        }

        /** Returns the largest integer less than or equal to the specified float. This method will only properly floor floats that are
         * positive. Note this method simply casts the float to int. */
        public static int FloorPositive(float value)
        {
            return (int)value;
        }

        /** Returns the smallest integer greater than or equal to the specified float. This method will only properly ceil floats from
         * -(2^14) to (Float.MAX_VALUE - 2^14). */
        public static int Ceil(float value)
        {
            return (int)(value + BIG_ENOUGH_CEIL) - BIG_ENOUGH_INT;
        }

        /** Returns the smallest integer greater than or equal to the specified float. This method will only properly ceil floats that
         * are positive. */
        public static int CeilPositive(float value)
        {
            return (int)(value + CEIL);
        }

        /** Returns the closest integer to the specified float. This method will only properly round floats from -(2^14) to
         * (Float.MAX_VALUE - 2^14). */
        public static int Round(float value)
        {
            return (int)(value + BIG_ENOUGH_ROUND) - BIG_ENOUGH_INT;
        }

        /** Returns the closest integer to the specified float. This method will only properly round floats that are positive. */
        public static int RoundPositive(float value)
        {
            return (int)(value + 0.5f);
        }

        /** Returns true if the value is zero (using the default tolerance as upper bound) */
        public static bool IsZero(float value)
        {
            return System.Math.Abs(value) <= FLOAT_ROUNDING_ERROR;
        }

        /** Returns true if the value is zero.
         * @param tolerance represent an upper bound below which the value is considered zero. */
        public static bool IsZero(float value, float tolerance)
        {
            return System.Math.Abs(value) <= tolerance;
        }

        /** Returns true if a is nearly equal to b. The function uses the default floating error tolerance.
         * @param a the first value.
         * @param b the second value. */
        public static bool IsEqual(float a, float b)
        {
            return System.Math.Abs(a - b) <= FLOAT_ROUNDING_ERROR;
        }

        /** Returns true if a is nearly equal to b.
         * @param a the first value.
         * @param b the second value.
         * @param tolerance represent an upper bound below which the two values are considered equal. */
        public static bool IsEqual(float a, float b, float tolerance)
        {
            return System.Math.Abs(a - b) <= tolerance;
        }

        /** @return the logarithm of value with base a */
        public static float Log(float a, float value)
        {
            return (float)(System.Math.Log(value) / System.Math.Log(a));
        }

        /** @return the logarithm of value with base 2 */
        public static float Log2(float value)
        {
            return Log(2, value);
        }

        internal class SinClass
        {
            public static readonly float[] table = new float[SIN_COUNT];

            public SinClass()
            {
                for (int i = 0; i < SIN_COUNT; i++)
                {
                    table[i] = (float)System.Math.Sin((i + 0.5f) / SIN_COUNT * radFull);
                }
                for (int i = 0; i < 360; i += 90)
                {
                    table[(int)(i * degToIndex) & SIN_MASK] = (float)System.Math.Sin(i * degreesToRadians);
                }
            }
        }
    }
}