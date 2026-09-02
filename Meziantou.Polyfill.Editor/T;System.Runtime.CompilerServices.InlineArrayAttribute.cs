// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Indicates that the instance's storage is sequentially replicated a number of times equal to <see cref="Length"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
    internal sealed class InlineArrayAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InlineArrayAttribute"/> class.
        /// </summary>
        /// <param name="length">The number of sequentially replicated instances of the struct's single field.</param>
        public InlineArrayAttribute(int length)
        {
            Length = length;
        }

        /// <summary>
        /// The number of sequentially replicated instances of the struct's single field.
        /// </summary>
        public int Length { get; }
    }
}
