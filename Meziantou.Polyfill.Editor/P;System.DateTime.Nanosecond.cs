static partial class PolyfillExtensions
{
    extension(System.DateTime target)
    {
        public int Nanosecond
        {
            get => (int)(target.Ticks % 10) * 100;
        }
    }
}
