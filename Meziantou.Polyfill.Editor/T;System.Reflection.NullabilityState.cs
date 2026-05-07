// when T:System.Reflection.NullabilityInfo
namespace System.Reflection
{
    /// <summary>
    /// An enum that represents nullability state
    /// </summary>
    internal enum NullabilityState
    {
        /// <summary>
        /// Nullability context not enabled (oblivious)
        /// </summary>
        Unknown,
        /// <summary>
        /// Non nullable value or reference type
        /// </summary>
        NotNull,
        /// <summary>
        /// Nullable value or reference type
        /// </summary>
        Nullable
    }
}
