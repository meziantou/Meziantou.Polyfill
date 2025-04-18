﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.Diagnostics.CodeAnalysis;

/// <summary>
///   Specifies that <see langword="null"/> is allowed as an input even if the
///   corresponding type disallows it.
/// </summary>
/// <summary>Specifies that null is allowed as an input even if the corresponding type disallows it.</summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false)]
internal sealed class AllowNullAttribute : Attribute
{
}