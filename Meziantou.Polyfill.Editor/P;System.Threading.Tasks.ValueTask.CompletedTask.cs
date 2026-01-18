namespace System.Threading.Tasks;

static partial class PolyfillExtensions
{
    extension(ValueTask)
    {
        /// <summary>
        /// Gets a <see cref="ValueTask"/> that has already completed successfully.
        /// </summary>
        public static ValueTask CompletedTask => default;
    }
}
