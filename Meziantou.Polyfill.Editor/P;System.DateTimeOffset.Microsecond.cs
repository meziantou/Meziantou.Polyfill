static partial class PolyfillExtensions
{
    extension(System.DateTimeOffset target)
    {
        public int Microsecond
        {
            get => (int)(target.Ticks % 10_000) / 10;
        }
    }
}
