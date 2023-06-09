// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.Versioning
{
    /// <summary>
    /// Marks APIs that were removed in a given operating system version.
    /// </summary>
    /// <remarks>
    /// Primarily used by OS bindings to indicate APIs that are only available in
    /// earlier versions.
    /// </remarks>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Assembly |
        global::System.AttributeTargets.Class |
        global::System.AttributeTargets.Constructor |
        global::System.AttributeTargets.Enum |
        global::System.AttributeTargets.Event |
        global::System.AttributeTargets.Field |
        global::System.AttributeTargets.Interface |
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Module |
        global::System.AttributeTargets.Property |
        global::System.AttributeTargets.Struct,
        AllowMultiple = true, Inherited = false)]
    internal sealed class UnsupportedOSPlatformAttribute : Attribute // global::System.Runtime.Versioning.OSPlatformAttribute
    {
        public UnsupportedOSPlatformAttribute(string platformName)
            //: base(platformName)
        {
        }
        public UnsupportedOSPlatformAttribute(string platformName, string? message)
            //: base(platformName)
        {
            Message = message;
        }

        public string? Message { get; }
    }
}