using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    extension(string)
    {
        public static string Join<T>(char separator, IEnumerable<T> values)
        {
            if (values is null)
                throw new ArgumentNullException(nameof(values));

            var sb = new System.Text.StringBuilder();
            using (var enumerator = values.GetEnumerator())
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