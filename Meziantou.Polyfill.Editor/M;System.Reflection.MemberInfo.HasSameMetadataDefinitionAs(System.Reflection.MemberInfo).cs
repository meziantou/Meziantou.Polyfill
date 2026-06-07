#pragma warning disable CA1510

using System;
using System.Reflection;

static partial class PolyfillExtensions
{
    public static bool HasSameMetadataDefinitionAs(this MemberInfo member, MemberInfo other)
    {
        if (member is null)
            throw new ArgumentNullException(nameof(member));
        if (other is null)
            throw new ArgumentNullException(nameof(other));

        return member.MemberType == other.MemberType && member.Module == other.Module && member.MetadataToken == other.MetadataToken;
    }
}
