// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

#if !NET6_0_OR_GREATER
/// <summary>Specifies that null is allowed as an input even if the corresponding type disallows it.</summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false)]
#if SYSTEM_PRIVATE_CORELIB
public
#else
internal
#endif
    sealed class AllowNullAttribute : Attribute
{
}
#endif
