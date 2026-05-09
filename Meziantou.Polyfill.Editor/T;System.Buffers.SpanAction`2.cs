namespace System.Buffers
{
#if MEZIANTOU_POLYFILL_SUPPORT_ALLOWS_REF_STRUCT
    internal delegate void SpanAction<T, in TArg>(global::System.Span<T> span, TArg arg)
        where TArg : allows ref struct;
#else
    internal delegate void SpanAction<T, in TArg>(global::System.Span<T> span, TArg arg);
#endif
}
