// when M:System.Guid.CreateVersion7(System.DateTimeOffset)
partial class PolyfillExtensions
{
    extension(System.Guid)
    {
        public static System.Guid CreateVersion7()
        {
            return System.Guid.CreateVersion7(System.DateTimeOffset.UtcNow);
        }
    }
}
