using System;
using System.Runtime.CompilerServices;

static partial class PolyfillExtensions
{
    extension(global::System.ArgumentOutOfRangeException)
    {
        public static void ThrowIfNegative<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
#if NET7_0_OR_GREATER
            where T : global::System.Numerics.INumber<T>
        {
            if (T.IsNegative(value))
                ThrowArgumentOutOfRangeException(paramName, value);
#else
            where T : struct, global::System.IComparable<T>
        {
            switch (value)
            {
                case byte or ushort or uint or ulong or char:
                    return;
                case sbyte n:
                    if (n < 0)
                        ThrowArgumentOutOfRangeException(paramName, value);
                    return;
                case short n:
                    if (n < 0)
                        ThrowArgumentOutOfRangeException(paramName, value);
                    return;
                case int n:
                    if (n < 0)
                        ThrowArgumentOutOfRangeException(paramName, value);
                    return;
                case long n:
                    if (n < 0L)
                        ThrowArgumentOutOfRangeException(paramName, value);
                    return;
#if NET5_0_OR_GREATER
                case System.Half n:
                    if (n < (System.Half)0)
                        ThrowArgumentOutOfRangeException(paramName, value);
                    return;
#endif
                case float n:
                    if (n < 0F)
                        ThrowArgumentOutOfRangeException(paramName, value);
                    return;
                case double n:
                    if (n < 0D)
                        ThrowArgumentOutOfRangeException(paramName, value);
                    return;
                case decimal n:
                    if (n < 0M)
                        ThrowArgumentOutOfRangeException(paramName, value);
                    return;
                default:
                    throw new InvalidOperationException($"Invalid type '{typeof(T).AssemblyQualifiedName}' for {paramName}.");
            }
#endif

            static void ThrowArgumentOutOfRangeException(string? paramName, object value)
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"{paramName} ('{value}') must not be negative.");
            }
        }
    }
}