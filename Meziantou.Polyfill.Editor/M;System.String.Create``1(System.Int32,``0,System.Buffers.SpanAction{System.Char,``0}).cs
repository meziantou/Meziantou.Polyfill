using System;
using System.Buffers;

static partial class PolyfillExtensions
{
    extension(string)
    {
        public static string Create<TState>(int length, TState state, SpanAction<char, TState> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (length == 0)
            {
                return string.Empty;
            }

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            Span<char> span = length <= 256 ? stackalloc char[length] : new char[length];
            action(span, state);
            return span.ToString();
        }
    }
}
