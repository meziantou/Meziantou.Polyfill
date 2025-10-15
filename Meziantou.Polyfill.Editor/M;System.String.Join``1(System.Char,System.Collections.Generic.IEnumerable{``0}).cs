using System.Collections.Generic;

static partial class PolyfillExtensions
{
    extension(string)
    {
        public static string Join<T>(char separator, IEnumerable<T> value)
        {
            var sb = new System.Text.StringBuilder();
            using (var enumerator = value.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    sb.Append(enumerator.Current);
                    while (enumerator.MoveNext())
                    {
                        sb.Append(separator);
                        sb.Append(enumerator.Current);
                    }
                }
            }

            return sb.ToString();
        }
    }
}