partial class PolyfillExtensions
{
    extension(System.Security.Cryptography.CryptographicOperations)
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining | System.Runtime.CompilerServices.MethodImplOptions.NoOptimization)]
        public static bool FixedTimeEquals(System.ReadOnlySpan<byte> left, System.ReadOnlySpan<byte> right)
        {
            // Lengths are not secret, so an early return does not leak information about the content.
            if (left.Length != right.Length)
                return false;

            var result = 0;
            for (var i = 0; i < left.Length; i++)
            {
                result |= left[i] - right[i];
            }

            return result == 0;
        }
    }
}
