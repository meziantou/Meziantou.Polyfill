using System;

static partial class PolyfillExtensions
{
    extension(Math)
    {
        public static uint Clamp(uint value, uint min, uint max)
        {
            if (min > max)
            {
                throw new ArgumentException($"'{min}' cannot be greater than '{max}'");
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }
    }
}