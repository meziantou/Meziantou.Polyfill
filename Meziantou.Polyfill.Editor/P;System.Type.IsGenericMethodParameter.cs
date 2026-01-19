using System;

static partial class PolyfillExtensions
{
    extension(Type target)
    {
        public bool IsGenericMethodParameter
        {
            get => target.IsGenericParameter && target.DeclaringMethod is not null;
        }
    }
}
