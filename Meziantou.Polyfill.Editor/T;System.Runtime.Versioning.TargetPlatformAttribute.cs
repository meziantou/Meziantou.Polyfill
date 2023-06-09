// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.Versioning
{
    /// <summary>
    /// Records the platform that the project targeted.
    /// </summary>
    [global::System.AttributeUsage(global::System.AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    internal sealed class TargetPlatformAttribute : Attribute // global::System.Runtime.Versioning.OSPlatformAttribute
    {
        public TargetPlatformAttribute(string platformName)
            //: base(platformName)
        {
        }
    }
}