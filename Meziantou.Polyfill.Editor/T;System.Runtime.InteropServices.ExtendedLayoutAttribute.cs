// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.InteropServices
{
    [global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct, Inherited = false)]
    internal sealed class ExtendedLayoutAttribute : global::System.Attribute
    {
        public ExtendedLayoutAttribute(global::System.Runtime.InteropServices.ExtendedLayoutKind layoutKind)
        {
            LayoutKind = layoutKind;
        }

        public global::System.Runtime.InteropServices.ExtendedLayoutKind LayoutKind { get; }
    }
}
