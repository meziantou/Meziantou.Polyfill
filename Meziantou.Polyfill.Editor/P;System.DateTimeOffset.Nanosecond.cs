static partial class PolyfillExtensions
{
    extension(System.DateTimeOffset target)
    {
        public int Nanosecond
        {
            get => (int)(target.Ticks % 10) * 100;
        }
    }
}
