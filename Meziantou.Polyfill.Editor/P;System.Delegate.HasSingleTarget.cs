using System;

static partial class PolyfillExtensions
{
    extension(Delegate target)
    {
        public bool HasSingleTarget
        {
            get => target.GetInvocationList().Length == 1;
        }
    }
}
