using System.Collections.Generic;
using System.Collections.ObjectModel;
static partial class PolyfillExtensions { public static ReadOnlySet<T> AsReadOnly<T>(this ISet<T> set) => new(set); }
