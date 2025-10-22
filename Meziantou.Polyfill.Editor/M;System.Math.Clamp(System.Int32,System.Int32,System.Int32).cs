using System;

static partial class PolyfillExtensions
{
    extension(Math)
    {
        public static int Clamp(int value, int min, int max)
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