// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

static partial class PolyfillExtensions
{
    public static bool EndsWith(this string target, char value, System.StringComparison comparisonType)
    {
        if (comparisonType == System.StringComparison.Ordinal)
            return target.Length > 0 && target[target.Length - 1] == value;

        return target.EndsWith(value.ToString(), comparisonType);
    }
}
