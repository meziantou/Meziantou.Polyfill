static partial class PolyfillExtensions
{
    extension(string target)
    {
        public bool EndsWith(char value)
        {
            return target.Length > 0 && target[target.Length - 1] == value;
        }
    }
}