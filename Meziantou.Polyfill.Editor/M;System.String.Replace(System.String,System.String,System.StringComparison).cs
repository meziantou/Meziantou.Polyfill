using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions
{
    public static string Replace(this string target, string oldValue, string? newValue, StringComparison comparisonType)
    {
        if (oldValue == null)
            throw new ArgumentNullException(nameof(oldValue));

        if (oldValue == "")
            throw new ArgumentException("The value cannot be an empty string.", nameof(oldValue));

        // Ordinal comparisons always match exactly oldValue.Length characters. Culture-sensitive comparisons
        // can match a region of the target whose length differs from oldValue.Length, so the length of the
        // match must be computed for each occurrence.
        CompareInfo? compareInfo = null;
        var compareOptions = CompareOptions.None;
        switch (comparisonType)
        {
            case StringComparison.CurrentCulture:
                compareInfo = CultureInfo.CurrentCulture.CompareInfo;
                break;

            case StringComparison.CurrentCultureIgnoreCase:
                compareInfo = CultureInfo.CurrentCulture.CompareInfo;
                compareOptions = CompareOptions.IgnoreCase;
                break;

            case StringComparison.InvariantCulture:
                compareInfo = CultureInfo.InvariantCulture.CompareInfo;
                break;

            case StringComparison.InvariantCultureIgnoreCase:
                compareInfo = CultureInfo.InvariantCulture.CompareInfo;
                compareOptions = CompareOptions.IgnoreCase;
                break;
        }

        var sb = new StringBuilder();

        var previousIndex = 0;
        while (target.IndexOf(oldValue, previousIndex, comparisonType) is var index and not -1)
        {
            int matchLength;
            if (compareInfo == null)
            {
                matchLength = oldValue.Length;
            }
            else
            {
                matchLength = GetMatchLength(compareInfo, target, index, oldValue, compareOptions);

                // oldValue has no collation weight (or no match length could be determined):
                // behave as if there is nothing left to replace
                if (matchLength <= 0)
                    break;
            }

            sb.Append(target, previousIndex, index - previousIndex);
            sb.Append(newValue);
            previousIndex = index + matchLength;
        }

        sb.Append(target, previousIndex, target.Length - previousIndex);
        return sb.ToString();

        // Equivalent of CompareInfo.IndexOf(..., out int matchLength) which is not available on all
        // the supported frameworks: find the shortest region of target starting at index that is
        // equal to oldValue. Returns -1 when no such region exists.
        static int GetMatchLength(CompareInfo compareInfo, string target, int index, string oldValue, CompareOptions options)
        {
            for (var length = 0; index + length <= target.Length; length++)
            {
                if (compareInfo.Compare(target, index, length, oldValue, 0, oldValue.Length, options) == 0)
                    return length;
            }

            return -1;
        }
    }
}
