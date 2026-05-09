namespace System.Buffers
{
    internal delegate void SpanAction<T, in TArg>(global::System.Span<T> span, TArg arg);
}
