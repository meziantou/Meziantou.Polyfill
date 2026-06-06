static partial class PolyfillExtensions
{
    extension(System.DateTime target)
    {
        public int Microsecond
        {
            get => (int)(target.Ticks % 10_000) / 10;
        }
    }
}
