static partial class PolyfillExtensions
{
    extension(string)
    {
        public static string Join(char separator, params object?[] value)
        {
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                if (i > 0)
                {
                    sb.Append(separator);
                }

                sb.Append(value[i]);
            }

            return sb.ToString();
        }
    }
}