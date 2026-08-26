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

        // NLS folds the target characters that pair with oldValue's own weightless tail into the match, so
        // the shortest equal region falls one character short for each of them. The count only depends on
        // oldValue, so it is computed once instead of once per occurrence.
        var ignorableTail = 0;
        if (compareInfo != null && ExtendsMatchOverIgnorableTail(compareInfo, compareOptions))
        {
            while (ignorableTail < oldValue.Length &&
                   HasNoCollationWeight(compareInfo, oldValue, oldValue.Length - ignorableTail - 1, ignorableTail + 1, compareOptions))
            {
                ignorableTail++;
            }
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
                matchLength = GetMatchLength(compareInfo, target, index, oldValue, compareOptions, ignorableTail);

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

        // Equivalent of CompareInfo.IndexOf(..., out int matchLength) which is not available on all the
        // supported frameworks: find the shortest region of target starting at index that is equal to
        // oldValue, then extend it over the weightless characters NLS folds into the match.
        // Returns -1 when no such region exists.
        static int GetMatchLength(CompareInfo compareInfo, string target, int index, string oldValue, CompareOptions options, int ignorableTail)
        {
            var length = -1;
            for (var candidate = 0; index + candidate <= target.Length; candidate++)
            {
                if (compareInfo.Compare(target, index, candidate, oldValue, 0, oldValue.Length, options) == 0)
                {
                    length = candidate;
                    break;
                }
            }

            // No match, or oldValue is weightless in its entirety. Extending a zero-length match would turn
            // "there is nothing to replace" into a replacement, so leave it for the caller to stop on.
            if (length <= 0)
                return length;

            var remaining = ignorableTail;
            while (remaining > 0 &&
                   index + length < target.Length &&
                   HasNoCollationWeight(compareInfo, target, index + length, 1, options))
            {
                length++;
                remaining--;
            }

            return length;
        }

        static bool HasNoCollationWeight(CompareInfo compareInfo, string value, int index, int length, CompareOptions options)
            => compareInfo.Compare(value, index, length, "", 0, 0, options) == 0;

        // Only NLS extends a match over the weightless characters that pair with oldValue's tail; applying
        // the rule under ICU makes the result worse. NLS also compares U+00DF (LATIN SMALL LETTER SHARP S)
        // equal to "ss" at the default strength while ICU does not, which tells the two implementations
        // apart without depending on globalization internals.
        static bool ExtendsMatchOverIgnorableTail(CompareInfo compareInfo, CompareOptions options)
            => compareInfo.Compare("\u00DF", "ss", options) == 0;
    }
}
