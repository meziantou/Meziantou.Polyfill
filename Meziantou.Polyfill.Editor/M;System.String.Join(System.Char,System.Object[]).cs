using System;

static partial class PolyfillExtensions
{
    extension(string)
    {
        public static string Join(char separator, params object?[] values)
        {
            if (values is null)
                throw new ArgumentNullException(nameof(values));

            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < values.Length; i++)
            {
                if (i > 0)
                {
                    sb.Append(separator);
                }

                sb.Append(values[i]);
            }

            return sb.ToString();
        }
    }
}