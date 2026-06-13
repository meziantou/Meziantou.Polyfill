partial class PolyfillExtensions
{
    extension(System.Security.Cryptography.CryptographicOperations)
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining | System.Runtime.CompilerServices.MethodImplOptions.NoOptimization)]
        public static bool FixedTimeEquals(System.ReadOnlySpan<byte> source, byte value)
        {
            var result = 0;
            for (var i = 0; i < source.Length; i++)
            {
                result |= source[i] - value;
            }

            return result == 0;
        }
    }
}
