// XML-DOC: M:System.IO.Path.GetRelativePath(System.String,System.String)
#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
using System;
using System.IO;
using System.Text;

partial class PolyfillExtensions
{
    extension(Path)
    {
        public static string GetRelativePath(string relativeTo, string path)
        {
            if (relativeTo == null)
                throw new ArgumentNullException(nameof(relativeTo));
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (relativeTo.Length == 0)
                throw new ArgumentException("Path cannot be the empty string.", nameof(relativeTo));
            if (path.Length == 0)
                throw new ArgumentException("Path cannot be the empty string.", nameof(path));

            relativeTo = Path.GetFullPath(relativeTo);
            path = Path.GetFullPath(path);

            var comparison = Path.DirectorySeparatorChar == '/'
                ? StringComparison.Ordinal
                : StringComparison.OrdinalIgnoreCase;

            return PathGetRelativePathHelper.GetRelativePath(relativeTo, path, comparison);
        }
    }
}

file static class PathGetRelativePathHelper
{
    public static string GetRelativePath(string relativeTo, string path, StringComparison comparison)
    {
        bool ignoreCase = comparison == StringComparison.OrdinalIgnoreCase;

        if (!AreRootsEqual(relativeTo, path, comparison))
            return path;

        int commonLength = GetCommonPathLength(relativeTo, path, ignoreCase);
        if (commonLength == 0)
            return path;

        int relativeToLength = relativeTo.Length;
        if (relativeToLength > 0 && IsDirectorySeparator(relativeTo[relativeToLength - 1]))
            relativeToLength--;

        int pathLength = path.Length;
        if (pathLength > 0 && IsDirectorySeparator(path[pathLength - 1]))
            pathLength--;

        // If paths are the same (ignoring trailing separators), return "."
        bool pathSameAsRelativeTo = pathLength == relativeToLength && commonLength >= relativeToLength;
        if (pathSameAsRelativeTo)
            return ".";

        // Clamp commonLength to relativeToLength to handle when relativeTo ends with a separator
        if (commonLength > relativeToLength)
            commonLength = relativeToLength;

        if (relativeToLength == commonLength)
        {
            // No parent segments: target is a child of the base path
            int next = commonLength;
            if (next < path.Length && IsDirectorySeparator(path[next]))
                next++;
            return path.Substring(next);
        }

        var sb = new StringBuilder();
        sb.Append("..");
        for (int i = commonLength + 1; i < relativeToLength; i++)
        {
            if (IsDirectorySeparator(relativeTo[i]))
            {
                sb.Append(Path.DirectorySeparatorChar);
                sb.Append("..");
            }
        }

        if (commonLength < pathLength)
        {
            sb.Append(Path.DirectorySeparatorChar);
            int start = commonLength;
            if (start < path.Length && IsDirectorySeparator(path[start]))
                start++;
            sb.Append(path, start, path.Length - start);
        }

        return sb.ToString();
    }

    private static bool AreRootsEqual(string first, string second, StringComparison comparison)
    {
        var firstRoot = Path.GetPathRoot(first) ?? string.Empty;
        var secondRoot = Path.GetPathRoot(second) ?? string.Empty;
        return firstRoot.Length == secondRoot.Length &&
               string.Compare(first, 0, second, 0, firstRoot.Length, comparison) == 0;
    }

    private static int GetCommonPathLength(string first, string second, bool ignoreCase)
    {
        int commonChars = 0;
        int minLength = Math.Min(first.Length, second.Length);
        for (int i = 0; i < minLength; i++)
        {
            char c1 = ignoreCase ? char.ToUpperInvariant(first[i]) : first[i];
            char c2 = ignoreCase ? char.ToUpperInvariant(second[i]) : second[i];
            if (c1 != c2)
                break;
            commonChars++;
        }

        if (commonChars == 0)
            return 0;

        if (commonChars == first.Length && (commonChars == second.Length || IsDirectorySeparator(second[commonChars])))
            return commonChars;
        if (commonChars == second.Length && commonChars < first.Length && IsDirectorySeparator(first[commonChars]))
            return commonChars;

        while (commonChars > 0 && !IsDirectorySeparator(first[commonChars - 1]))
            commonChars--;

        return commonChars;
    }

    private static bool IsDirectorySeparator(char c)
        => c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
}
#endif
