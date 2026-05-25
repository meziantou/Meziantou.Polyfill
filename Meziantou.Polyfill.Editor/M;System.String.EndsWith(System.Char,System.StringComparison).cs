// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

static partial class PolyfillExtensions
{
    public static bool EndsWith(this string target, char value, System.StringComparison comparisonType)
    {
        return target.EndsWith(value.ToString(), comparisonType);
    }
}
