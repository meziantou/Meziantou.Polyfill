




#nullable enable
using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Meziantou.Polyfill;

internal sealed partial class Members : IEquatable<Members>
{
    
    private readonly bool _generate_M_System_Collections_Generic_KeyValuePair_2_Deconstruct__0___1__;
    
    private readonly bool _generate_M_System_Collections_Generic_Queue_1_TryDequeue__0__;
    
    private readonly bool _generate_M_System_MemoryExtensions_Contains__1_System_ReadOnlySpan___0____0_;
    
    private readonly bool _generate_M_System_MemoryExtensions_Contains__1_System_Span___0____0_;
    
    private readonly bool _generate_M_System_String_Contains_System_Char_;
    
    private readonly bool _generate_M_System_String_Contains_System_String_System_StringComparison_;
    
    private readonly bool _generate_M_System_String_CopyTo_System_Span_System_Char__;
    
    private readonly bool _generate_M_System_String_EndsWith_System_Char_;
    
    private readonly bool _generate_M_System_String_GetHashCode_System_StringComparison_;
    
    private readonly bool _generate_M_System_String_Replace_System_String_System_String_System_StringComparison_;
    
    private readonly bool _generate_M_System_String_Split_System_Char_System_Int32_System_StringSplitOptions_;
    
    private readonly bool _generate_M_System_String_Split_System_Char_System_StringSplitOptions_;
    
    private readonly bool _generate_M_System_String_StartsWith_System_Char_;
    
    private readonly bool _generate_M_System_String_TryCopyTo_System_Span_System_Char__;
    
    private readonly bool _generate_M_System_Text_StringBuilder_Append_System_ReadOnlyMemory_System_Char__;
    
    private readonly bool _generate_M_System_Text_StringBuilder_Append_System_ReadOnlySpan_System_Char__;
    
    private readonly bool _generate_M_System_Threading_CancellationTokenSource_CancelAsync;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_AllowNullAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_DisallowNullAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_DoesNotReturnAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_DoesNotReturnIfAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_DynamicDependencyAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMemberTypes;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMembersAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_MaybeNullAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_MaybeNullWhenAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_MemberNotNullAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_MemberNotNullWhenAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_NotNullAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_NotNullIfNotNullAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_NotNullWhenAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_RequiresAssemblyFilesAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_RequiresDynamicCodeAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_RequiresUnreferencedCodeAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_SetsRequiredMembersAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_StringSyntaxAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_UnconditionalSuppressMessageAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_CodeAnalysis_UnscopedRefAttribute;
    
    private readonly bool _generate_T_System_Diagnostics_StackTraceHiddenAttribute;
    
    private readonly bool _generate_T_System_Index;
    
    private readonly bool _generate_T_System_Range;
    
    private readonly bool _generate_T_System_Runtime_CompilerServices_AsyncMethodBuilderAttribute;
    
    private readonly bool _generate_T_System_Runtime_CompilerServices_CallerArgumentExpressionAttribute;
    
    private readonly bool _generate_T_System_Runtime_CompilerServices_CompilerFeatureRequiredAttribute;
    
    private readonly bool _generate_T_System_Runtime_CompilerServices_DisableRuntimeMarshallingAttribute;
    
    private readonly bool _generate_T_System_Runtime_CompilerServices_InterpolatedStringHandlerArgumentAttribute;
    
    private readonly bool _generate_T_System_Runtime_CompilerServices_InterpolatedStringHandlerAttribute;
    
    private readonly bool _generate_T_System_Runtime_CompilerServices_IsExternalInit;
    
    private readonly bool _generate_T_System_Runtime_CompilerServices_ModuleInitializerAttribute;
    
    private readonly bool _generate_T_System_Runtime_CompilerServices_RequiredMemberAttribute;
    
    private readonly bool _generate_T_System_Runtime_CompilerServices_SkipLocalsInitAttribute;
    
    private readonly bool _generate_T_System_Runtime_InteropServices_SuppressGCTransitionAttribute;
    
    private readonly bool _generate_T_System_Runtime_InteropServices_UnmanagedCallersOnlyAttribute;
    
    private readonly bool _generate_T_System_Runtime_Versioning_ObsoletedOSPlatformAttribute;
    
    private readonly bool _generate_T_System_Runtime_Versioning_RequiresPreviewFeaturesAttribute;
    
    private readonly bool _generate_T_System_Runtime_Versioning_SupportedOSPlatformAttribute;
    
    private readonly bool _generate_T_System_Runtime_Versioning_SupportedOSPlatformGuardAttribute;
    
    private readonly bool _generate_T_System_Runtime_Versioning_TargetPlatformAttribute;
    
    private readonly bool _generate_T_System_Runtime_Versioning_UnsupportedOSPlatformAttribute;
    
    private readonly bool _generate_T_System_Runtime_Versioning_UnsupportedOSPlatformGuardAttribute;
    
    
    public Members(Compilation compilation, PolyfillOptions options)
    {
    
        
        _generate_M_System_Collections_Generic_KeyValuePair_2_Deconstruct__0___1__ = IncludeMember(compilation, options, "M:System.Collections.Generic.KeyValuePair`2.Deconstruct(`0@,`1@)");
    
        
        _generate_M_System_Collections_Generic_Queue_1_TryDequeue__0__ = IncludeMember(compilation, options, "M:System.Collections.Generic.Queue`1.TryDequeue(`0@)");
    
        
        _generate_M_System_MemoryExtensions_Contains__1_System_ReadOnlySpan___0____0_ = IncludeMember(compilation, options, "M:System.MemoryExtensions.Contains``1(System.ReadOnlySpan{``0},``0)");
    
        
        _generate_M_System_MemoryExtensions_Contains__1_System_Span___0____0_ = IncludeMember(compilation, options, "M:System.MemoryExtensions.Contains``1(System.Span{``0},``0)");
    
        
        _generate_M_System_String_Contains_System_Char_ = IncludeMember(compilation, options, "M:System.String.Contains(System.Char)");
    
        
        _generate_M_System_String_Contains_System_String_System_StringComparison_ = IncludeMember(compilation, options, "M:System.String.Contains(System.String,System.StringComparison)");
    
        
        _generate_M_System_String_CopyTo_System_Span_System_Char__ = IncludeMember(compilation, options, "M:System.String.CopyTo(System.Span{System.Char})");
    
        
        _generate_M_System_String_EndsWith_System_Char_ = IncludeMember(compilation, options, "M:System.String.EndsWith(System.Char)");
    
        
        _generate_M_System_String_GetHashCode_System_StringComparison_ = IncludeMember(compilation, options, "M:System.String.GetHashCode(System.StringComparison)");
    
        
        _generate_M_System_String_Replace_System_String_System_String_System_StringComparison_ = IncludeMember(compilation, options, "M:System.String.Replace(System.String,System.String,System.StringComparison)");
    
        
        _generate_M_System_String_Split_System_Char_System_Int32_System_StringSplitOptions_ = IncludeMember(compilation, options, "M:System.String.Split(System.Char,System.Int32,System.StringSplitOptions)");
    
        
        _generate_M_System_String_Split_System_Char_System_StringSplitOptions_ = IncludeMember(compilation, options, "M:System.String.Split(System.Char,System.StringSplitOptions)");
    
        
        _generate_M_System_String_StartsWith_System_Char_ = IncludeMember(compilation, options, "M:System.String.StartsWith(System.Char)");
    
        
        _generate_M_System_String_TryCopyTo_System_Span_System_Char__ = IncludeMember(compilation, options, "M:System.String.TryCopyTo(System.Span{System.Char})");
    
        
        _generate_M_System_Text_StringBuilder_Append_System_ReadOnlyMemory_System_Char__ = IncludeMember(compilation, options, "M:System.Text.StringBuilder.Append(System.ReadOnlyMemory{System.Char})");
    
        
        _generate_M_System_Text_StringBuilder_Append_System_ReadOnlySpan_System_Char__ = IncludeMember(compilation, options, "M:System.Text.StringBuilder.Append(System.ReadOnlySpan{System.Char})");
    
        
        _generate_M_System_Threading_CancellationTokenSource_CancelAsync = IncludeMember(compilation, options, "M:System.Threading.CancellationTokenSource.CancelAsync");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_AllowNullAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.AllowNullAttribute");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_DisallowNullAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.DisallowNullAttribute");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_DoesNotReturnAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_DoesNotReturnIfAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.DoesNotReturnIfAttribute");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_DynamicDependencyAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMemberTypes = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMembersAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.DynamicallyAccessedMembersAttribute");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_MaybeNullAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.MaybeNullAttribute");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_MaybeNullWhenAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.MaybeNullWhenAttribute");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_MemberNotNullAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.MemberNotNullAttribute");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_MemberNotNullWhenAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.MemberNotNullWhenAttribute");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_NotNullAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.NotNullAttribute");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_NotNullIfNotNullAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.NotNullIfNotNullAttribute");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_NotNullWhenAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.NotNullWhenAttribute");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_RequiresAssemblyFilesAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.RequiresAssemblyFilesAttribute");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_RequiresDynamicCodeAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.RequiresDynamicCodeAttribute");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_RequiresUnreferencedCodeAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.RequiresUnreferencedCodeAttribute");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_SetsRequiredMembersAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_StringSyntaxAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.StringSyntaxAttribute");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_UnconditionalSuppressMessageAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessageAttribute");
    
        
        _generate_T_System_Diagnostics_CodeAnalysis_UnscopedRefAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.UnscopedRefAttribute");
    
        
        _generate_T_System_Diagnostics_StackTraceHiddenAttribute = IncludeMember(compilation, options, "T:System.Diagnostics.StackTraceHiddenAttribute");
    
        
        _generate_T_System_Index = IncludeMember(compilation, options, "T:System.Index");
    
        
        _generate_T_System_Range = IncludeMember(compilation, options, "T:System.Range");
    
        
        _generate_T_System_Runtime_CompilerServices_AsyncMethodBuilderAttribute = IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.AsyncMethodBuilderAttribute");
    
        
        _generate_T_System_Runtime_CompilerServices_CallerArgumentExpressionAttribute = IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.CallerArgumentExpressionAttribute");
    
        
        _generate_T_System_Runtime_CompilerServices_CompilerFeatureRequiredAttribute = IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.CompilerFeatureRequiredAttribute");
    
        
        _generate_T_System_Runtime_CompilerServices_DisableRuntimeMarshallingAttribute = IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.DisableRuntimeMarshallingAttribute");
    
        
        _generate_T_System_Runtime_CompilerServices_InterpolatedStringHandlerArgumentAttribute = IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.InterpolatedStringHandlerArgumentAttribute");
    
        
        _generate_T_System_Runtime_CompilerServices_InterpolatedStringHandlerAttribute = IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.InterpolatedStringHandlerAttribute");
    
        
        _generate_T_System_Runtime_CompilerServices_IsExternalInit = IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.IsExternalInit");
    
        
        _generate_T_System_Runtime_CompilerServices_ModuleInitializerAttribute = IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.ModuleInitializerAttribute");
    
        
        _generate_T_System_Runtime_CompilerServices_RequiredMemberAttribute = IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.RequiredMemberAttribute");
    
        
        _generate_T_System_Runtime_CompilerServices_SkipLocalsInitAttribute = IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.SkipLocalsInitAttribute");
    
        
        _generate_T_System_Runtime_InteropServices_SuppressGCTransitionAttribute = IncludeMember(compilation, options, "T:System.Runtime.InteropServices.SuppressGCTransitionAttribute");
    
        
        _generate_T_System_Runtime_InteropServices_UnmanagedCallersOnlyAttribute = IncludeMember(compilation, options, "T:System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute");
    
        
        _generate_T_System_Runtime_Versioning_ObsoletedOSPlatformAttribute = IncludeMember(compilation, options, "T:System.Runtime.Versioning.ObsoletedOSPlatformAttribute");
    
        
        _generate_T_System_Runtime_Versioning_RequiresPreviewFeaturesAttribute = IncludeMember(compilation, options, "T:System.Runtime.Versioning.RequiresPreviewFeaturesAttribute");
    
        
        _generate_T_System_Runtime_Versioning_SupportedOSPlatformAttribute = IncludeMember(compilation, options, "T:System.Runtime.Versioning.SupportedOSPlatformAttribute");
    
        
        _generate_T_System_Runtime_Versioning_SupportedOSPlatformGuardAttribute = IncludeMember(compilation, options, "T:System.Runtime.Versioning.SupportedOSPlatformGuardAttribute");
    
        
        _generate_T_System_Runtime_Versioning_TargetPlatformAttribute = IncludeMember(compilation, options, "T:System.Runtime.Versioning.TargetPlatformAttribute");
    
        
        _generate_T_System_Runtime_Versioning_UnsupportedOSPlatformAttribute = IncludeMember(compilation, options, "T:System.Runtime.Versioning.UnsupportedOSPlatformAttribute");
    
        
        _generate_T_System_Runtime_Versioning_UnsupportedOSPlatformGuardAttribute = IncludeMember(compilation, options, "T:System.Runtime.Versioning.UnsupportedOSPlatformGuardAttribute");
    
    }


    public override int GetHashCode()
    {    
        var hash = 0;
        
        hash = hash * 23 + _generate_M_System_Collections_Generic_KeyValuePair_2_Deconstruct__0___1__.GetHashCode();
        
        hash = hash * 23 + _generate_M_System_Collections_Generic_Queue_1_TryDequeue__0__.GetHashCode();
        
        hash = hash * 23 + _generate_M_System_MemoryExtensions_Contains__1_System_ReadOnlySpan___0____0_.GetHashCode();
        
        hash = hash * 23 + _generate_M_System_MemoryExtensions_Contains__1_System_Span___0____0_.GetHashCode();
        
        hash = hash * 23 + _generate_M_System_String_Contains_System_Char_.GetHashCode();
        
        hash = hash * 23 + _generate_M_System_String_Contains_System_String_System_StringComparison_.GetHashCode();
        
        hash = hash * 23 + _generate_M_System_String_CopyTo_System_Span_System_Char__.GetHashCode();
        
        hash = hash * 23 + _generate_M_System_String_EndsWith_System_Char_.GetHashCode();
        
        hash = hash * 23 + _generate_M_System_String_GetHashCode_System_StringComparison_.GetHashCode();
        
        hash = hash * 23 + _generate_M_System_String_Replace_System_String_System_String_System_StringComparison_.GetHashCode();
        
        hash = hash * 23 + _generate_M_System_String_Split_System_Char_System_Int32_System_StringSplitOptions_.GetHashCode();
        
        hash = hash * 23 + _generate_M_System_String_Split_System_Char_System_StringSplitOptions_.GetHashCode();
        
        hash = hash * 23 + _generate_M_System_String_StartsWith_System_Char_.GetHashCode();
        
        hash = hash * 23 + _generate_M_System_String_TryCopyTo_System_Span_System_Char__.GetHashCode();
        
        hash = hash * 23 + _generate_M_System_Text_StringBuilder_Append_System_ReadOnlyMemory_System_Char__.GetHashCode();
        
        hash = hash * 23 + _generate_M_System_Text_StringBuilder_Append_System_ReadOnlySpan_System_Char__.GetHashCode();
        
        hash = hash * 23 + _generate_M_System_Threading_CancellationTokenSource_CancelAsync.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_AllowNullAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_DisallowNullAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_DoesNotReturnAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_DoesNotReturnIfAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_DynamicDependencyAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMemberTypes.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMembersAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_MaybeNullAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_MaybeNullWhenAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_MemberNotNullAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_MemberNotNullWhenAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_NotNullAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_NotNullIfNotNullAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_NotNullWhenAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_RequiresAssemblyFilesAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_RequiresDynamicCodeAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_RequiresUnreferencedCodeAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_SetsRequiredMembersAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_StringSyntaxAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_UnconditionalSuppressMessageAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_CodeAnalysis_UnscopedRefAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Diagnostics_StackTraceHiddenAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Index.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Range.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Runtime_CompilerServices_AsyncMethodBuilderAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Runtime_CompilerServices_CallerArgumentExpressionAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Runtime_CompilerServices_CompilerFeatureRequiredAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Runtime_CompilerServices_DisableRuntimeMarshallingAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Runtime_CompilerServices_InterpolatedStringHandlerArgumentAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Runtime_CompilerServices_InterpolatedStringHandlerAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Runtime_CompilerServices_IsExternalInit.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Runtime_CompilerServices_ModuleInitializerAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Runtime_CompilerServices_RequiredMemberAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Runtime_CompilerServices_SkipLocalsInitAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Runtime_InteropServices_SuppressGCTransitionAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Runtime_InteropServices_UnmanagedCallersOnlyAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Runtime_Versioning_ObsoletedOSPlatformAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Runtime_Versioning_RequiresPreviewFeaturesAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Runtime_Versioning_SupportedOSPlatformAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Runtime_Versioning_SupportedOSPlatformGuardAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Runtime_Versioning_TargetPlatformAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Runtime_Versioning_UnsupportedOSPlatformAttribute.GetHashCode();
        
        hash = hash * 23 + _generate_T_System_Runtime_Versioning_UnsupportedOSPlatformGuardAttribute.GetHashCode();
        
        return hash;
    }

    public override bool Equals(object? obj) => obj is Members other && Equals(other);

    public bool Equals(Members? other)
    {
        return other != null
        
        && _generate_M_System_Collections_Generic_KeyValuePair_2_Deconstruct__0___1__ == other._generate_M_System_Collections_Generic_KeyValuePair_2_Deconstruct__0___1__
        
        && _generate_M_System_Collections_Generic_Queue_1_TryDequeue__0__ == other._generate_M_System_Collections_Generic_Queue_1_TryDequeue__0__
        
        && _generate_M_System_MemoryExtensions_Contains__1_System_ReadOnlySpan___0____0_ == other._generate_M_System_MemoryExtensions_Contains__1_System_ReadOnlySpan___0____0_
        
        && _generate_M_System_MemoryExtensions_Contains__1_System_Span___0____0_ == other._generate_M_System_MemoryExtensions_Contains__1_System_Span___0____0_
        
        && _generate_M_System_String_Contains_System_Char_ == other._generate_M_System_String_Contains_System_Char_
        
        && _generate_M_System_String_Contains_System_String_System_StringComparison_ == other._generate_M_System_String_Contains_System_String_System_StringComparison_
        
        && _generate_M_System_String_CopyTo_System_Span_System_Char__ == other._generate_M_System_String_CopyTo_System_Span_System_Char__
        
        && _generate_M_System_String_EndsWith_System_Char_ == other._generate_M_System_String_EndsWith_System_Char_
        
        && _generate_M_System_String_GetHashCode_System_StringComparison_ == other._generate_M_System_String_GetHashCode_System_StringComparison_
        
        && _generate_M_System_String_Replace_System_String_System_String_System_StringComparison_ == other._generate_M_System_String_Replace_System_String_System_String_System_StringComparison_
        
        && _generate_M_System_String_Split_System_Char_System_Int32_System_StringSplitOptions_ == other._generate_M_System_String_Split_System_Char_System_Int32_System_StringSplitOptions_
        
        && _generate_M_System_String_Split_System_Char_System_StringSplitOptions_ == other._generate_M_System_String_Split_System_Char_System_StringSplitOptions_
        
        && _generate_M_System_String_StartsWith_System_Char_ == other._generate_M_System_String_StartsWith_System_Char_
        
        && _generate_M_System_String_TryCopyTo_System_Span_System_Char__ == other._generate_M_System_String_TryCopyTo_System_Span_System_Char__
        
        && _generate_M_System_Text_StringBuilder_Append_System_ReadOnlyMemory_System_Char__ == other._generate_M_System_Text_StringBuilder_Append_System_ReadOnlyMemory_System_Char__
        
        && _generate_M_System_Text_StringBuilder_Append_System_ReadOnlySpan_System_Char__ == other._generate_M_System_Text_StringBuilder_Append_System_ReadOnlySpan_System_Char__
        
        && _generate_M_System_Threading_CancellationTokenSource_CancelAsync == other._generate_M_System_Threading_CancellationTokenSource_CancelAsync
        
        && _generate_T_System_Diagnostics_CodeAnalysis_AllowNullAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_AllowNullAttribute
        
        && _generate_T_System_Diagnostics_CodeAnalysis_DisallowNullAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_DisallowNullAttribute
        
        && _generate_T_System_Diagnostics_CodeAnalysis_DoesNotReturnAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_DoesNotReturnAttribute
        
        && _generate_T_System_Diagnostics_CodeAnalysis_DoesNotReturnIfAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_DoesNotReturnIfAttribute
        
        && _generate_T_System_Diagnostics_CodeAnalysis_DynamicDependencyAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_DynamicDependencyAttribute
        
        && _generate_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMemberTypes == other._generate_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMemberTypes
        
        && _generate_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMembersAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMembersAttribute
        
        && _generate_T_System_Diagnostics_CodeAnalysis_MaybeNullAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_MaybeNullAttribute
        
        && _generate_T_System_Diagnostics_CodeAnalysis_MaybeNullWhenAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_MaybeNullWhenAttribute
        
        && _generate_T_System_Diagnostics_CodeAnalysis_MemberNotNullAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_MemberNotNullAttribute
        
        && _generate_T_System_Diagnostics_CodeAnalysis_MemberNotNullWhenAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_MemberNotNullWhenAttribute
        
        && _generate_T_System_Diagnostics_CodeAnalysis_NotNullAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_NotNullAttribute
        
        && _generate_T_System_Diagnostics_CodeAnalysis_NotNullIfNotNullAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_NotNullIfNotNullAttribute
        
        && _generate_T_System_Diagnostics_CodeAnalysis_NotNullWhenAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_NotNullWhenAttribute
        
        && _generate_T_System_Diagnostics_CodeAnalysis_RequiresAssemblyFilesAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_RequiresAssemblyFilesAttribute
        
        && _generate_T_System_Diagnostics_CodeAnalysis_RequiresDynamicCodeAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_RequiresDynamicCodeAttribute
        
        && _generate_T_System_Diagnostics_CodeAnalysis_RequiresUnreferencedCodeAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_RequiresUnreferencedCodeAttribute
        
        && _generate_T_System_Diagnostics_CodeAnalysis_SetsRequiredMembersAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_SetsRequiredMembersAttribute
        
        && _generate_T_System_Diagnostics_CodeAnalysis_StringSyntaxAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_StringSyntaxAttribute
        
        && _generate_T_System_Diagnostics_CodeAnalysis_UnconditionalSuppressMessageAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_UnconditionalSuppressMessageAttribute
        
        && _generate_T_System_Diagnostics_CodeAnalysis_UnscopedRefAttribute == other._generate_T_System_Diagnostics_CodeAnalysis_UnscopedRefAttribute
        
        && _generate_T_System_Diagnostics_StackTraceHiddenAttribute == other._generate_T_System_Diagnostics_StackTraceHiddenAttribute
        
        && _generate_T_System_Index == other._generate_T_System_Index
        
        && _generate_T_System_Range == other._generate_T_System_Range
        
        && _generate_T_System_Runtime_CompilerServices_AsyncMethodBuilderAttribute == other._generate_T_System_Runtime_CompilerServices_AsyncMethodBuilderAttribute
        
        && _generate_T_System_Runtime_CompilerServices_CallerArgumentExpressionAttribute == other._generate_T_System_Runtime_CompilerServices_CallerArgumentExpressionAttribute
        
        && _generate_T_System_Runtime_CompilerServices_CompilerFeatureRequiredAttribute == other._generate_T_System_Runtime_CompilerServices_CompilerFeatureRequiredAttribute
        
        && _generate_T_System_Runtime_CompilerServices_DisableRuntimeMarshallingAttribute == other._generate_T_System_Runtime_CompilerServices_DisableRuntimeMarshallingAttribute
        
        && _generate_T_System_Runtime_CompilerServices_InterpolatedStringHandlerArgumentAttribute == other._generate_T_System_Runtime_CompilerServices_InterpolatedStringHandlerArgumentAttribute
        
        && _generate_T_System_Runtime_CompilerServices_InterpolatedStringHandlerAttribute == other._generate_T_System_Runtime_CompilerServices_InterpolatedStringHandlerAttribute
        
        && _generate_T_System_Runtime_CompilerServices_IsExternalInit == other._generate_T_System_Runtime_CompilerServices_IsExternalInit
        
        && _generate_T_System_Runtime_CompilerServices_ModuleInitializerAttribute == other._generate_T_System_Runtime_CompilerServices_ModuleInitializerAttribute
        
        && _generate_T_System_Runtime_CompilerServices_RequiredMemberAttribute == other._generate_T_System_Runtime_CompilerServices_RequiredMemberAttribute
        
        && _generate_T_System_Runtime_CompilerServices_SkipLocalsInitAttribute == other._generate_T_System_Runtime_CompilerServices_SkipLocalsInitAttribute
        
        && _generate_T_System_Runtime_InteropServices_SuppressGCTransitionAttribute == other._generate_T_System_Runtime_InteropServices_SuppressGCTransitionAttribute
        
        && _generate_T_System_Runtime_InteropServices_UnmanagedCallersOnlyAttribute == other._generate_T_System_Runtime_InteropServices_UnmanagedCallersOnlyAttribute
        
        && _generate_T_System_Runtime_Versioning_ObsoletedOSPlatformAttribute == other._generate_T_System_Runtime_Versioning_ObsoletedOSPlatformAttribute
        
        && _generate_T_System_Runtime_Versioning_RequiresPreviewFeaturesAttribute == other._generate_T_System_Runtime_Versioning_RequiresPreviewFeaturesAttribute
        
        && _generate_T_System_Runtime_Versioning_SupportedOSPlatformAttribute == other._generate_T_System_Runtime_Versioning_SupportedOSPlatformAttribute
        
        && _generate_T_System_Runtime_Versioning_SupportedOSPlatformGuardAttribute == other._generate_T_System_Runtime_Versioning_SupportedOSPlatformGuardAttribute
        
        && _generate_T_System_Runtime_Versioning_TargetPlatformAttribute == other._generate_T_System_Runtime_Versioning_TargetPlatformAttribute
        
        && _generate_T_System_Runtime_Versioning_UnsupportedOSPlatformAttribute == other._generate_T_System_Runtime_Versioning_UnsupportedOSPlatformAttribute
        
        && _generate_T_System_Runtime_Versioning_UnsupportedOSPlatformGuardAttribute == other._generate_T_System_Runtime_Versioning_UnsupportedOSPlatformGuardAttribute
        ;
    }

    public void AddSources(SourceProductionContext context)
    {
        
        if (_generate_M_System_Collections_Generic_KeyValuePair_2_Deconstruct__0___1__)
        {
            context.AddSource("M_System.Collections.Generic.KeyValuePair`2.Deconstruct(`0_,`1_).g.cs", PolyfillContents.Source_M_System_Collections_Generic_KeyValuePair_2_Deconstruct__0___1__);
        }        
        
        if (_generate_M_System_Collections_Generic_Queue_1_TryDequeue__0__)
        {
            context.AddSource("M_System.Collections.Generic.Queue`1.TryDequeue(`0_).g.cs", PolyfillContents.Source_M_System_Collections_Generic_Queue_1_TryDequeue__0__);
        }        
        
        if (_generate_M_System_MemoryExtensions_Contains__1_System_ReadOnlySpan___0____0_)
        {
            context.AddSource("M_System.MemoryExtensions.Contains``1(System.ReadOnlySpan{``0},``0).g.cs", PolyfillContents.Source_M_System_MemoryExtensions_Contains__1_System_ReadOnlySpan___0____0_);
        }        
        
        if (_generate_M_System_MemoryExtensions_Contains__1_System_Span___0____0_)
        {
            context.AddSource("M_System.MemoryExtensions.Contains``1(System.Span{``0},``0).g.cs", PolyfillContents.Source_M_System_MemoryExtensions_Contains__1_System_Span___0____0_);
        }        
        
        if (_generate_M_System_String_Contains_System_Char_)
        {
            context.AddSource("M_System.String.Contains(System.Char).g.cs", PolyfillContents.Source_M_System_String_Contains_System_Char_);
        }        
        
        if (_generate_M_System_String_Contains_System_String_System_StringComparison_)
        {
            context.AddSource("M_System.String.Contains(System.String,System.StringComparison).g.cs", PolyfillContents.Source_M_System_String_Contains_System_String_System_StringComparison_);
        }        
        
        if (_generate_M_System_String_CopyTo_System_Span_System_Char__)
        {
            context.AddSource("M_System.String.CopyTo(System.Span{System.Char}).g.cs", PolyfillContents.Source_M_System_String_CopyTo_System_Span_System_Char__);
        }        
        
        if (_generate_M_System_String_EndsWith_System_Char_)
        {
            context.AddSource("M_System.String.EndsWith(System.Char).g.cs", PolyfillContents.Source_M_System_String_EndsWith_System_Char_);
        }        
        
        if (_generate_M_System_String_GetHashCode_System_StringComparison_)
        {
            context.AddSource("M_System.String.GetHashCode(System.StringComparison).g.cs", PolyfillContents.Source_M_System_String_GetHashCode_System_StringComparison_);
        }        
        
        if (_generate_M_System_String_Replace_System_String_System_String_System_StringComparison_)
        {
            context.AddSource("M_System.String.Replace(System.String,System.String,System.StringComparison).g.cs", PolyfillContents.Source_M_System_String_Replace_System_String_System_String_System_StringComparison_);
        }        
        
        if (_generate_M_System_String_Split_System_Char_System_Int32_System_StringSplitOptions_)
        {
            context.AddSource("M_System.String.Split(System.Char,System.Int32,System.StringSplitOptions).g.cs", PolyfillContents.Source_M_System_String_Split_System_Char_System_Int32_System_StringSplitOptions_);
        }        
        
        if (_generate_M_System_String_Split_System_Char_System_StringSplitOptions_)
        {
            context.AddSource("M_System.String.Split(System.Char,System.StringSplitOptions).g.cs", PolyfillContents.Source_M_System_String_Split_System_Char_System_StringSplitOptions_);
        }        
        
        if (_generate_M_System_String_StartsWith_System_Char_)
        {
            context.AddSource("M_System.String.StartsWith(System.Char).g.cs", PolyfillContents.Source_M_System_String_StartsWith_System_Char_);
        }        
        
        if (_generate_M_System_String_TryCopyTo_System_Span_System_Char__)
        {
            context.AddSource("M_System.String.TryCopyTo(System.Span{System.Char}).g.cs", PolyfillContents.Source_M_System_String_TryCopyTo_System_Span_System_Char__);
        }        
        
        if (_generate_M_System_Text_StringBuilder_Append_System_ReadOnlyMemory_System_Char__)
        {
            context.AddSource("M_System.Text.StringBuilder.Append(System.ReadOnlyMemory{System.Char}).g.cs", PolyfillContents.Source_M_System_Text_StringBuilder_Append_System_ReadOnlyMemory_System_Char__);
        }        
        
        if (_generate_M_System_Text_StringBuilder_Append_System_ReadOnlySpan_System_Char__)
        {
            context.AddSource("M_System.Text.StringBuilder.Append(System.ReadOnlySpan{System.Char}).g.cs", PolyfillContents.Source_M_System_Text_StringBuilder_Append_System_ReadOnlySpan_System_Char__);
        }        
        
        if (_generate_M_System_Threading_CancellationTokenSource_CancelAsync)
        {
            context.AddSource("M_System.Threading.CancellationTokenSource.CancelAsync.g.cs", PolyfillContents.Source_M_System_Threading_CancellationTokenSource_CancelAsync);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_AllowNullAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.AllowNullAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_AllowNullAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_DisallowNullAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.DisallowNullAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_DisallowNullAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_DoesNotReturnAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_DoesNotReturnAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_DoesNotReturnIfAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.DoesNotReturnIfAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_DoesNotReturnIfAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_DynamicDependencyAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_DynamicDependencyAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMemberTypes)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMemberTypes);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMembersAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.DynamicallyAccessedMembersAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMembersAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_MaybeNullAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.MaybeNullAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_MaybeNullAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_MaybeNullWhenAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.MaybeNullWhenAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_MaybeNullWhenAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_MemberNotNullAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.MemberNotNullAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_MemberNotNullAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_MemberNotNullWhenAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.MemberNotNullWhenAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_MemberNotNullWhenAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_NotNullAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.NotNullAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_NotNullAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_NotNullIfNotNullAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.NotNullIfNotNullAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_NotNullIfNotNullAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_NotNullWhenAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.NotNullWhenAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_NotNullWhenAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_RequiresAssemblyFilesAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.RequiresAssemblyFilesAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_RequiresAssemblyFilesAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_RequiresDynamicCodeAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.RequiresDynamicCodeAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_RequiresDynamicCodeAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_RequiresUnreferencedCodeAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.RequiresUnreferencedCodeAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_RequiresUnreferencedCodeAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_SetsRequiredMembersAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_SetsRequiredMembersAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_StringSyntaxAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.StringSyntaxAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_StringSyntaxAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_UnconditionalSuppressMessageAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessageAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_UnconditionalSuppressMessageAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_CodeAnalysis_UnscopedRefAttribute)
        {
            context.AddSource("T_System.Diagnostics.CodeAnalysis.UnscopedRefAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_UnscopedRefAttribute);
        }        
        
        if (_generate_T_System_Diagnostics_StackTraceHiddenAttribute)
        {
            context.AddSource("T_System.Diagnostics.StackTraceHiddenAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_StackTraceHiddenAttribute);
        }        
        
        if (_generate_T_System_Index)
        {
            context.AddSource("T_System.Index.g.cs", PolyfillContents.Source_T_System_Index);
        }        
        
        if (_generate_T_System_Range)
        {
            context.AddSource("T_System.Range.g.cs", PolyfillContents.Source_T_System_Range);
        }        
        
        if (_generate_T_System_Runtime_CompilerServices_AsyncMethodBuilderAttribute)
        {
            context.AddSource("T_System.Runtime.CompilerServices.AsyncMethodBuilderAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_AsyncMethodBuilderAttribute);
        }        
        
        if (_generate_T_System_Runtime_CompilerServices_CallerArgumentExpressionAttribute)
        {
            context.AddSource("T_System.Runtime.CompilerServices.CallerArgumentExpressionAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_CallerArgumentExpressionAttribute);
        }        
        
        if (_generate_T_System_Runtime_CompilerServices_CompilerFeatureRequiredAttribute)
        {
            context.AddSource("T_System.Runtime.CompilerServices.CompilerFeatureRequiredAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_CompilerFeatureRequiredAttribute);
        }        
        
        if (_generate_T_System_Runtime_CompilerServices_DisableRuntimeMarshallingAttribute)
        {
            context.AddSource("T_System.Runtime.CompilerServices.DisableRuntimeMarshallingAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_DisableRuntimeMarshallingAttribute);
        }        
        
        if (_generate_T_System_Runtime_CompilerServices_InterpolatedStringHandlerArgumentAttribute)
        {
            context.AddSource("T_System.Runtime.CompilerServices.InterpolatedStringHandlerArgumentAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_InterpolatedStringHandlerArgumentAttribute);
        }        
        
        if (_generate_T_System_Runtime_CompilerServices_InterpolatedStringHandlerAttribute)
        {
            context.AddSource("T_System.Runtime.CompilerServices.InterpolatedStringHandlerAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_InterpolatedStringHandlerAttribute);
        }        
        
        if (_generate_T_System_Runtime_CompilerServices_IsExternalInit)
        {
            context.AddSource("T_System.Runtime.CompilerServices.IsExternalInit.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_IsExternalInit);
        }        
        
        if (_generate_T_System_Runtime_CompilerServices_ModuleInitializerAttribute)
        {
            context.AddSource("T_System.Runtime.CompilerServices.ModuleInitializerAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_ModuleInitializerAttribute);
        }        
        
        if (_generate_T_System_Runtime_CompilerServices_RequiredMemberAttribute)
        {
            context.AddSource("T_System.Runtime.CompilerServices.RequiredMemberAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_RequiredMemberAttribute);
        }        
        
        if (_generate_T_System_Runtime_CompilerServices_SkipLocalsInitAttribute)
        {
            context.AddSource("T_System.Runtime.CompilerServices.SkipLocalsInitAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_SkipLocalsInitAttribute);
        }        
        
        if (_generate_T_System_Runtime_InteropServices_SuppressGCTransitionAttribute)
        {
            context.AddSource("T_System.Runtime.InteropServices.SuppressGCTransitionAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_InteropServices_SuppressGCTransitionAttribute);
        }        
        
        if (_generate_T_System_Runtime_InteropServices_UnmanagedCallersOnlyAttribute)
        {
            context.AddSource("T_System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_InteropServices_UnmanagedCallersOnlyAttribute);
        }        
        
        if (_generate_T_System_Runtime_Versioning_ObsoletedOSPlatformAttribute)
        {
            context.AddSource("T_System.Runtime.Versioning.ObsoletedOSPlatformAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_Versioning_ObsoletedOSPlatformAttribute);
        }        
        
        if (_generate_T_System_Runtime_Versioning_RequiresPreviewFeaturesAttribute)
        {
            context.AddSource("T_System.Runtime.Versioning.RequiresPreviewFeaturesAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_Versioning_RequiresPreviewFeaturesAttribute);
        }        
        
        if (_generate_T_System_Runtime_Versioning_SupportedOSPlatformAttribute)
        {
            context.AddSource("T_System.Runtime.Versioning.SupportedOSPlatformAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_Versioning_SupportedOSPlatformAttribute);
        }        
        
        if (_generate_T_System_Runtime_Versioning_SupportedOSPlatformGuardAttribute)
        {
            context.AddSource("T_System.Runtime.Versioning.SupportedOSPlatformGuardAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_Versioning_SupportedOSPlatformGuardAttribute);
        }        
        
        if (_generate_T_System_Runtime_Versioning_TargetPlatformAttribute)
        {
            context.AddSource("T_System.Runtime.Versioning.TargetPlatformAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_Versioning_TargetPlatformAttribute);
        }        
        
        if (_generate_T_System_Runtime_Versioning_UnsupportedOSPlatformAttribute)
        {
            context.AddSource("T_System.Runtime.Versioning.UnsupportedOSPlatformAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_Versioning_UnsupportedOSPlatformAttribute);
        }        
        
        if (_generate_T_System_Runtime_Versioning_UnsupportedOSPlatformGuardAttribute)
        {
            context.AddSource("T_System.Runtime.Versioning.UnsupportedOSPlatformGuardAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_Versioning_UnsupportedOSPlatformGuardAttribute);
        }        
        
    }
}

file static class PolyfillContents
{
    
    public static SourceText Source_M_System_Collections_Generic_KeyValuePair_2_Deconstruct__0___1__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> target, out TKey key, out TValue value)
    {
        key = target.Key;
        value = target.Value;
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_M_System_Collections_Generic_Queue_1_TryDequeue__0__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

static partial class PolyfillExtensions
{
    public static bool TryDequeue<T>(this Queue<T> target, [MaybeNullWhen(false)] out T result)
    {
        if (target.Count == 0)
        {
            result = default;
            return false;
        }

        result = target.Dequeue();
        return true;
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_M_System_MemoryExtensions_Contains__1_System_ReadOnlySpan___0____0_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

using System;

static partial class PolyfillExtensions
{
    public static bool Contains<T>(this ReadOnlySpan<T> span, T value) where T : IEquatable<T>?
    {
        if (default(T) != null || value is not null)
        {
            foreach (var item in span)
            {
                if (value!.Equals(item))
                    return true;
            }
        }
        else
        {
            foreach (var item in span)
            {
                if (item is null)
                    return true;
            }
        }

        return false;
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_M_System_MemoryExtensions_Contains__1_System_Span___0____0_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

using System;

static partial class PolyfillExtensions
{
    public static bool Contains<T>(this Span<T> span, T value) where T : IEquatable<T>?
    {
        if (default(T) != null || value is not null)
        {
            foreach (var item in span)
            {
                if (value!.Equals(item))
                    return true;
            }
        }
        else
        {
            foreach (var item in span)
            {
                if (item is null)
                    return true;
            }
        }

        return false;
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_M_System_String_Contains_System_Char_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

static partial class PolyfillExtensions
{
	public static bool Contains(this string target, char value)
	{
		return target.IndexOf(value) != -1;
	}
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_M_System_String_Contains_System_String_System_StringComparison_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

static partial class PolyfillExtensions
{
	public static bool Contains(this string target, string value, System.StringComparison comparisonType)
	{
		return target.IndexOf(value, comparisonType) != -1;
	}
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_M_System_String_CopyTo_System_Span_System_Char__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

using System;

static partial class PolyfillExtensions
{
    public static void CopyTo(this string target, Span<char> destination)
    {
        target.AsSpan().CopyTo(destination);
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_M_System_String_EndsWith_System_Char_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

static partial class PolyfillExtensions
{
    public static bool EndsWith(this string target, char value)
    {
        return target.Length > 0 && target[target.Length - 1] == value;
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_M_System_String_GetHashCode_System_StringComparison_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

using System;

static partial class PolyfillExtensions
{
    public static int GetHashCode(this string target, StringComparison comparisonType)
    {
        return Helpers.FromComparison(comparisonType).GetHashCode(target);
    }
}

file class Helpers
{
    public static StringComparer FromComparison(StringComparison comparisonType) =>
        comparisonType switch
        {
            StringComparison.CurrentCulture => StringComparer.CurrentCulture,
            StringComparison.CurrentCultureIgnoreCase => StringComparer.CurrentCultureIgnoreCase,
            StringComparison.InvariantCulture => StringComparer.InvariantCulture,
            StringComparison.InvariantCultureIgnoreCase => StringComparer.InvariantCultureIgnoreCase,
            StringComparison.Ordinal => StringComparer.Ordinal,
            StringComparison.OrdinalIgnoreCase => StringComparer.OrdinalIgnoreCase,
            _ => throw new ArgumentException("The string comparison type passed in is currently not supported.", nameof(comparisonType)),
        };
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_M_System_String_Replace_System_String_System_String_System_StringComparison_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

using System;
using System.Text;

static partial class PolyfillExtensions
{
    public static string Replace(this string target, string oldValue, string? newValue, StringComparison comparisonType)
    {
        if (oldValue == null)
            throw new ArgumentNullException(nameof(oldValue));

        if (oldValue == "")
            throw new ArgumentException("The value cannot be an empty string.", nameof(oldValue));

        var sb = new StringBuilder();

        var previousIndex = 0;
        while (target.IndexOf(oldValue, previousIndex, comparisonType) is var index and not -1)
        {
            sb.Append(target, previousIndex, index - previousIndex);
            sb.Append(newValue);
            previousIndex = index + oldValue.Length;
        }

        sb.Append(target, previousIndex, target.Length - previousIndex);
        return sb.ToString();
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_M_System_String_Split_System_Char_System_Int32_System_StringSplitOptions_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

using System;

static partial class PolyfillExtensions
{
    public static string[] Split(this string target, char separator, int count, StringSplitOptions options = StringSplitOptions.None)
    {
        return target.Split(new char[] { separator }, count, options);
    }
}

"""""""""", Encoding.UTF8);
    
    public static SourceText Source_M_System_String_Split_System_Char_System_StringSplitOptions_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

using System;

static partial class PolyfillExtensions
{
    public static string[] Split(this string target, char separator, StringSplitOptions options = StringSplitOptions.None)
    {
        return target.Split(new char[] { separator }, options);
    }
}

"""""""""", Encoding.UTF8);
    
    public static SourceText Source_M_System_String_StartsWith_System_Char_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

static partial class PolyfillExtensions
{
    public static bool StartsWith(this string target, char value)
    {
        return target.Length > 0 && target[0] == value;
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_M_System_String_TryCopyTo_System_Span_System_Char__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

using System;

static partial class PolyfillExtensions
{
    public static bool TryCopyTo(this string target, Span<char> destination)
    {
        return target.AsSpan().TryCopyTo(destination);
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_M_System_Text_StringBuilder_Append_System_ReadOnlyMemory_System_Char__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

using System;
using System.Text;

static partial class PolyfillExtensions
{
    public static StringBuilder Append(this StringBuilder target, ReadOnlyMemory<char> value)
    {
        if (value.IsEmpty)
            return target;

        return target.Append(value.ToArray());
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_M_System_Text_StringBuilder_Append_System_ReadOnlySpan_System_Char__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

using System;
using System.Text;

static partial class PolyfillExtensions
{
    public static StringBuilder Append(this StringBuilder target, ReadOnlySpan<char> value)
    {
        if (value.IsEmpty)
            return target;

        return target.Append(value.ToArray());
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_M_System_Threading_CancellationTokenSource_CancelAsync { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static Task CancelAsync(this CancellationTokenSource target)
    {
        target.Cancel();
        return Task.CompletedTask;
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_AllowNullAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

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
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_DisallowNullAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Specifies that null is disallowed as an input even if the corresponding type allows it.</summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false)]
internal sealed class DisallowNullAttribute : Attribute
{ }
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_DoesNotReturnAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Applied to a method that will never return under any circumstance.</summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
internal sealed class DoesNotReturnAttribute : Attribute
{
}

"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_DoesNotReturnIfAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Specifies that the method will not return if the associated Boolean parameter is passed the specified value.</summary>
[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
internal sealed class DoesNotReturnIfAttribute : Attribute
{
    /// <summary>Initializes the attribute with the specified parameter value.</summary>
    /// <param name="parameterValue">
    /// The condition parameter value. Code after the method will be considered unreachable by diagnostics if the argument to
    /// the associated parameter matches this value.
    /// </param>
    public DoesNotReturnIfAttribute(bool parameterValue) => ParameterValue = parameterValue;

    /// <summary>Gets the condition parameter value.</summary>
    public bool ParameterValue { get; }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_DynamicDependencyAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// States a dependency that one member has on another.
    /// </summary>
    /// <remarks>
    /// This can be used to inform tooling of a dependency that is otherwise not evident purely from
    /// metadata and IL, for example a member relied on via reflection.
    /// </remarks>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Constructor |
        global::System.AttributeTargets.Field |
        global::System.AttributeTargets.Method,
        AllowMultiple = true, Inherited = false)]
    internal sealed class DynamicDependencyAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute"/> class
        /// with the specified signature of a member on the same type as the consumer.
        /// </summary>
        /// <param name="memberSignature">The signature of the member depended on.</param>
        public DynamicDependencyAttribute(string memberSignature)
        {
            MemberSignature = memberSignature;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute"/> class
        /// with the specified signature of a member on a <see cref="global::System.Type"/>.
        /// </summary>
        /// <param name="memberSignature">The signature of the member depended on.</param>
        /// <param name="type">The <see cref="global::System.Type"/> containing <paramref name="memberSignature"/>.</param>
        public DynamicDependencyAttribute(string memberSignature, global::System.Type type)
        {
            MemberSignature = memberSignature;
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicDependencyAttribute"/> class
        /// with the specified signature of a member on a type in an assembly.
        /// </summary>
        /// <param name="memberSignature">The signature of the member depended on.</param>
        /// <param name="typeName">The full name of the type containing the specified member.</param>
        /// <param name="assemblyName">The assembly name of the type containing the specified member.</param>
        public DynamicDependencyAttribute(string memberSignature, string typeName, string assemblyName)
        {
            MemberSignature = memberSignature;
            TypeName = typeName;
            AssemblyName = assemblyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute"/> class
        /// with the specified types of members on a <see cref="global::System.Type"/>.
        /// </summary>
        /// <param name="memberTypes">The types of members depended on.</param>
        /// <param name="type">The <see cref="global::System.Type"/> containing the specified members.</param>
        public DynamicDependencyAttribute(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes memberTypes, global::System.Type type)
        {
            MemberTypes = memberTypes;
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute"/> class
        /// with the specified types of members on a type in an assembly.
        /// </summary>
        /// <param name="memberTypes">The types of members depended on.</param>
        /// <param name="typeName">The full name of the type containing the specified members.</param>
        /// <param name="assemblyName">The assembly name of the type containing the specified members.</param>
        public DynamicDependencyAttribute(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes memberTypes, string typeName, string assemblyName)
        {
            MemberTypes = memberTypes;
            TypeName = typeName;
            AssemblyName = assemblyName;
        }

        /// <summary>
        /// Gets the signature of the member depended on.
        /// </summary>
        /// <remarks>
        /// Either <see cref="MemberSignature"/> must be a valid string or <see cref="MemberTypes"/>
        /// must not equal <see cref="global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.None"/>, but not both.
        /// </remarks>
        public string? MemberSignature { get; }

        /// <summary>
        /// Gets the <see cref="global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes"/> which specifies the type
        /// of members depended on.
        /// </summary>
        /// <remarks>
        /// Either <see cref="MemberSignature"/> must be a valid string or <see cref="MemberTypes"/>
        /// must not equal <see cref="global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.None"/>, but not both.
        /// </remarks>
        public global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes MemberTypes { get; }

        /// <summary>
        /// Gets the <see cref="global::System.Type"/> containing the specified member.
        /// </summary>
        /// <remarks>
        /// If neither <see cref="Type"/> nor <see cref="TypeName"/> are specified,
        /// the type of the consumer is assumed.
        /// </remarks>
        public global::System.Type? Type { get; }

        /// <summary>
        /// Gets the full name of the type containing the specified member.
        /// </summary>
        /// <remarks>
        /// If neither <see cref="Type"/> nor <see cref="TypeName"/> are specified,
        /// the type of the consumer is assumed.
        /// </remarks>
        public string? TypeName { get; }

        /// <summary>
        /// Gets the assembly name of the specified type.
        /// </summary>
        /// <remarks>
        /// <see cref="AssemblyName"/> is only valid when <see cref="TypeName"/> is specified.
        /// </remarks>
        public string? AssemblyName { get; }

        /// <summary>
        /// Gets or sets the condition in which the dependency is applicable, e.g. "DEBUG".
        /// </summary>
        public string? Condition { get; set; }
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMemberTypes { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Specifies the types of members that are dynamically accessed.
    ///
    /// This enumeration has a <see cref="global::System.FlagsAttribute"/> attribute that allows a
    /// bitwise combination of its member values.
    /// </summary>
    [global::System.Flags]
    internal enum DynamicallyAccessedMemberTypes
    {
        /// <summary>
        /// Specifies no members.
        /// </summary>
        None = 0,

        /// <summary>
        /// Specifies the default, parameterless public constructor.
        /// </summary>
        PublicParameterlessConstructor = 0x0001,

        /// <summary>
        /// Specifies all public constructors.
        /// </summary>
        PublicConstructors = 0x0002 | global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.PublicParameterlessConstructor,

        /// <summary>
        /// Specifies all non-public constructors.
        /// </summary>
        NonPublicConstructors = 0x0004,

        /// <summary>
        /// Specifies all public methods.
        /// </summary>
        PublicMethods = 0x0008,

        /// <summary>
        /// Specifies all non-public methods.
        /// </summary>
        NonPublicMethods = 0x0010,

        /// <summary>
        /// Specifies all public fields.
        /// </summary>
        PublicFields = 0x0020,

        /// <summary>
        /// Specifies all non-public fields.
        /// </summary>
        NonPublicFields = 0x0040,

        /// <summary>
        /// Specifies all public nested types.
        /// </summary>
        PublicNestedTypes = 0x0080,

        /// <summary>
        /// Specifies all non-public nested types.
        /// </summary>
        NonPublicNestedTypes = 0x0100,

        /// <summary>
        /// Specifies all public properties.
        /// </summary>
        PublicProperties = 0x0200,

        /// <summary>
        /// Specifies all non-public properties.
        /// </summary>
        NonPublicProperties = 0x0400,

        /// <summary>
        /// Specifies all public events.
        /// </summary>
        PublicEvents = 0x0800,

        /// <summary>
        /// Specifies all non-public events.
        /// </summary>
        NonPublicEvents = 0x1000,

        /// <summary>
        /// Specifies all interfaces implemented by the type.
        /// </summary>
        Interfaces = 0x2000,

        /// <summary>
        /// Specifies all members.
        /// </summary>
        All = ~global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.None
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMembersAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Indicates that certain members on a specified <see cref="global::System.Type"/> are accessed dynamically,
    /// for example through <see cref="global::System.Reflection"/>.
    /// </summary>
    /// <remarks>
    /// This allows tools to understand which members are being accessed during the execution
    /// of a program.
    ///
    /// This attribute is valid on members whose type is <see cref="global::System.Type"/> or <see cref="string"/>.
    ///
    /// When this attribute is applied to a location of type <see cref="string"/>, the assumption is
    /// that the string represents a fully qualified type name.
    ///
    /// When this attribute is applied to a class, interface, or struct, the members specified
    /// can be accessed dynamically on <see cref="global::System.Type"/> instances returned from calling
    /// <see cref="object.GetType"/> on instances of that class, interface, or struct.
    ///
    /// If the attribute is applied to a method it's treated as a special case and it implies
    /// the attribute should be applied to the "this" parameter of the method. As such the attribute
    /// should only be used on instance methods of types assignable to System.Type (or string, but no methods
    /// will use it there).
    /// </remarks>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Field |
        global::System.AttributeTargets.ReturnValue |
        global::System.AttributeTargets.GenericParameter |
        global::System.AttributeTargets.Parameter |
        global::System.AttributeTargets.Property |
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Class |
        global::System.AttributeTargets.Interface |
        global::System.AttributeTargets.Struct,
        Inherited = false)]
    internal sealed class DynamicallyAccessedMembersAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMembersAttribute"/> class
        /// with the specified member types.
        /// </summary>
        /// <param name="memberTypes">The types of members dynamically accessed.</param>
        public DynamicallyAccessedMembersAttribute(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes memberTypes)
        {
            MemberTypes = memberTypes;
        }

        /// <summary>
        /// Gets the <see cref="global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes"/> which specifies the type
        /// of members dynamically accessed.
        /// </summary>
        public global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes MemberTypes { get; }
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_MaybeNullAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Specifies that an output may be null even if the corresponding type disallows it.</summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false)]
internal sealed class MaybeNullAttribute : Attribute
{ 
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_MaybeNullWhenAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Specifies that when a method returns <see cref="ReturnValue"/>, the parameter may be null even if the corresponding type disallows it.</summary>
[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
internal sealed class MaybeNullWhenAttribute : Attribute
{
    /// <summary>Initializes the attribute with the specified return value condition.</summary>
    /// <param name="returnValue">
    /// The return value condition. If the method returns this value, the associated parameter may be null.
    /// </param>
    public MaybeNullWhenAttribute(bool returnValue) => ReturnValue = returnValue;

    /// <summary>Gets the return value condition.</summary>
    public bool ReturnValue { get; }
}

"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_MemberNotNullAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Specifies that the method or property will ensure that the listed field and property members have not-null values.</summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
internal sealed class MemberNotNullAttribute : Attribute
{
    /// <summary>Initializes the attribute with a field or property member.</summary>
    /// <param name="member">
    /// The field or property member that is promised to be not-null.
    /// </param>
    public MemberNotNullAttribute(string member) => Members = new[] { member };

    /// <summary>Initializes the attribute with the list of field and property members.</summary>
    /// <param name="members">
    /// The list of field and property members that are promised to be not-null.
    /// </param>
    public MemberNotNullAttribute(params string[] members) => Members = members;

    /// <summary>Gets field or property member names.</summary>
    public string[] Members { get; }
}

"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_MemberNotNullWhenAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Specifies that the method or property will ensure that the listed field and property members have not-null values when returning with the specified return value condition.</summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
internal sealed class MemberNotNullWhenAttribute : Attribute
{
    /// <summary>Initializes the attribute with the specified return value condition and a field or property member.</summary>
    /// <param name="returnValue">
    /// The return value condition. If the method returns this value, the associated parameter will not be null.
    /// </param>
    /// <param name="member">
    /// The field or property member that is promised to be not-null.
    /// </param>
    public MemberNotNullWhenAttribute(bool returnValue, string member)
    {
        ReturnValue = returnValue;
        Members = new[] { member };
    }

    /// <summary>Initializes the attribute with the specified return value condition and list of field and property members.</summary>
    /// <param name="returnValue">
    /// The return value condition. If the method returns this value, the associated parameter will not be null.
    /// </param>
    /// <param name="members">
    /// The list of field and property members that are promised to be not-null.
    /// </param>
    public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
    {
        ReturnValue = returnValue;
        Members = members;
    }

    /// <summary>Gets the return value condition.</summary>
    public bool ReturnValue { get; }

    /// <summary>Gets field or property member names.</summary>
    public string[] Members { get; }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_NotNullAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Specifies that an output will not be null even if the corresponding type allows it. Specifies that an input argument was not null when the call returns.</summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false)]
internal sealed class NotNullAttribute : Attribute
{
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_NotNullIfNotNullAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Specifies that the output will be non-null if the named parameter is non-null.</summary>
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
internal sealed class NotNullIfNotNullAttribute : Attribute
{
    /// <summary>Initializes the attribute with the associated parameter name.</summary>
    /// <param name="parameterName">
    /// The associated parameter name.  The output will be non-null if the argument to the parameter specified is non-null.
    /// </param>
    public NotNullIfNotNullAttribute(string parameterName) => ParameterName = parameterName;

    /// <summary>Gets the associated parameter name.</summary>
    public string ParameterName { get; }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_NotNullWhenAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Specifies that when a method returns <see cref="ReturnValue"/>, the parameter will not be null even if the corresponding type allows it.</summary>
[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
internal sealed class NotNullWhenAttribute : Attribute
{
    /// <summary>Initializes the attribute with the specified return value condition.</summary>
    /// <param name="returnValue">
    /// The return value condition. If the method returns this value, the associated parameter will not be null.
    /// </param>
    public NotNullWhenAttribute(bool returnValue) => ReturnValue = returnValue;

    /// <summary>Gets the return value condition.</summary>
    public bool ReturnValue { get; }
}

"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_RequiresAssemblyFilesAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Indicates that the specified member requires assembly files to be on disk.
    /// </summary>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Constructor |
        global::System.AttributeTargets.Event |
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Property,
        Inherited = false, AllowMultiple = false)]
    internal sealed class RequiresAssemblyFilesAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.RequiresAssemblyFilesAttribute"/> class.
        /// </summary>
        public RequiresAssemblyFilesAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.RequiresAssemblyFilesAttribute"/> class.
        /// </summary>
        /// <param name="message">
        /// A message that contains information about the need for assembly files to be on disk.
        /// </param>
        public RequiresAssemblyFilesAttribute(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Gets an optional message that contains information about the need for
        /// assembly files to be on disk.
        /// </summary>
        public string? Message { get; }

        /// <summary>
        /// Gets or sets an optional URL that contains more information about the member,
        /// why it requires assembly files to be on disk, and what options a consumer has
        /// to deal with it.
        /// </summary>
        public string? Url { get; set; }
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_RequiresDynamicCodeAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Indicates that the specified method requires the ability to generate new code at runtime,
    /// for example through <see cref="global::System.Reflection"/>.
    /// </summary>
    /// <remarks>
    /// This allows tools to understand which methods are unsafe to call when compiling ahead of time.
    /// </remarks>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Constructor |
        global::System.AttributeTargets.Class,
        Inherited = false)]
    internal sealed class RequiresDynamicCodeAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Diagnostics.CodeAnalysis.RequiresDynamicCodeAttribute"/> class
        /// with the specified message.
        /// </summary>
        /// <param name="message">
        /// A message that contains information about the usage of dynamic code.
        /// </param>
        public RequiresDynamicCodeAttribute(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Gets a message that contains information about the usage of dynamic code.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets or sets an optional URL that contains more information about the method,
        /// why it requires dynamic code, and what options a consumer has to deal with it.
        /// </summary>
        public string? Url { get; set; }
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_RequiresUnreferencedCodeAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Indicates that the specified method requires dynamic access to code that is not referenced
    /// statically, for example through <see cref="global::System.Reflection"/>.
    /// </summary>
    /// <remarks>
    /// This allows tools to understand which methods are unsafe to call when removing unreferenced
    /// code from an application.
    /// </remarks>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Constructor |
        global::System.AttributeTargets.Class, Inherited = false)]
    internal sealed class RequiresUnreferencedCodeAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Diagnostics.CodeAnalysis.RequiresUnreferencedCodeAttribute"/> class
        /// with the specified message.
        /// </summary>
        /// <param name="message">
        /// A message that contains information about the usage of unreferenced code.
        /// </param>
        public RequiresUnreferencedCodeAttribute(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Gets a message that contains information about the usage of unreferenced code.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets or sets an optional URL that contains more information about the method,
        /// why it requires unreferenced code, and what options a consumer has to deal with it.
        /// </summary>
        public string? Url { get; set; }
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_SetsRequiredMembersAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Specifies that this constructor sets all required members for the current type, and callers
    /// do not need to set any required members themselves.
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
    internal sealed class SetsRequiredMembersAttribute : Attribute
    { }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_StringSyntaxAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>Specifies the syntax used in a string.</summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    internal sealed class StringSyntaxAttribute : Attribute
    {
        /// <summary>Initializes the <see cref="StringSyntaxAttribute"/> with the identifier of the syntax used.</summary>
        /// <param name="syntax">The syntax identifier.</param>
        public StringSyntaxAttribute(string syntax)
        {
            Syntax = syntax;
            Arguments = Array.Empty<object?>();
        }

        /// <summary>Initializes the <see cref="StringSyntaxAttribute"/> with the identifier of the syntax used.</summary>
        /// <param name="syntax">The syntax identifier.</param>
        /// <param name="arguments">Optional arguments associated with the specific syntax employed.</param>
        public StringSyntaxAttribute(string syntax, params object?[] arguments)
        {
            Syntax = syntax;
            Arguments = arguments;
        }

        /// <summary>Gets the identifier of the syntax used.</summary>
        public string Syntax { get; }

        /// <summary>Optional arguments associated with the specific syntax employed.</summary>
        public object?[] Arguments { get; }

        /// <summary>The syntax identifier for strings containing composite formats for string formatting.</summary>
        public const string CompositeFormat = nameof(CompositeFormat);

        /// <summary>The syntax identifier for strings containing date format specifiers.</summary>
        public const string DateOnlyFormat = nameof(DateOnlyFormat);

        /// <summary>The syntax identifier for strings containing date and time format specifiers.</summary>
        public const string DateTimeFormat = nameof(DateTimeFormat);

        /// <summary>The syntax identifier for strings containing <see cref="Enum"/> format specifiers.</summary>
        public const string EnumFormat = nameof(EnumFormat);

        /// <summary>The syntax identifier for strings containing <see cref="Guid"/> format specifiers.</summary>
        public const string GuidFormat = nameof(GuidFormat);

        /// <summary>The syntax identifier for strings containing JavaScript Object Notation (JSON).</summary>
        public const string Json = nameof(Json);

        /// <summary>The syntax identifier for strings containing numeric format specifiers.</summary>
        public const string NumericFormat = nameof(NumericFormat);

        /// <summary>The syntax identifier for strings containing regular expressions.</summary>
        public const string Regex = nameof(Regex);

        /// <summary>The syntax identifier for strings containing time format specifiers.</summary>
        public const string TimeOnlyFormat = nameof(TimeOnlyFormat);

        /// <summary>The syntax identifier for strings containing <see cref="TimeSpan"/> format specifiers.</summary>
        public const string TimeSpanFormat = nameof(TimeSpanFormat);

        /// <summary>The syntax identifier for strings containing URIs.</summary>
        public const string Uri = nameof(Uri);

        /// <summary>The syntax identifier for strings containing XML.</summary>
        public const string Xml = nameof(Xml);
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_UnconditionalSuppressMessageAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Suppresses reporting of a specific rule violation, allowing multiple suppressions on a
    /// single code artifact.
    /// </summary>
    /// <remarks>
    /// <see cref="global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessageAttribute"/> is different than
    /// <see cref="global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute"/> in that it doesn't have a
    /// <see cref="global::System.Diagnostics.ConditionalAttribute"/>. So it is always preserved in the compiled assembly.
    /// </remarks>
    [global::System.AttributeUsage(global::System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    internal sealed class UnconditionalSuppressMessageAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessageAttribute"/>
        /// class, specifying the category of the tool and the identifier for an analysis rule.
        /// </summary>
        /// <param name="category">The category for the attribute.</param>
        /// <param name="checkId">The identifier of the analysis rule the attribute applies to.</param>
        public UnconditionalSuppressMessageAttribute(string category, string checkId)
        {
            Category = category;
            CheckId = checkId;
        }

        /// <summary>
        /// Gets the category identifying the classification of the attribute.
        /// </summary>
        /// <remarks>
        /// The <see cref="Category"/> property describes the tool or tool analysis category
        /// for which a message suppression attribute applies.
        /// </remarks>
        public string Category { get; }

        /// <summary>
        /// Gets the identifier of the analysis tool rule to be suppressed.
        /// </summary>
        /// <remarks>
        /// Concatenated together, the <see cref="Category"/> and <see cref="CheckId"/>
        /// properties form a unique check identifier.
        /// </remarks>
        public string CheckId { get; }

        /// <summary>
        /// Gets or sets the scope of the code that is relevant for the attribute.
        /// </summary>
        /// <remarks>
        /// The Scope property is an optional argument that specifies the metadata scope for which
        /// the attribute is relevant.
        /// </remarks>
        public string? Scope { get; set; }

        /// <summary>
        /// Gets or sets a fully qualified path that represents the target of the attribute.
        /// </summary>
        /// <remarks>
        /// The <see cref="Target"/> property is an optional argument identifying the analysis target
        /// of the attribute. An example value is "System.IO.Stream.ctor():System.Void".
        /// Because it is fully qualified, it can be long, particularly for targets such as parameters.
        /// The analysis tool user interface should be capable of automatically formatting the parameter.
        /// </remarks>
        public string? Target { get; set; }

        /// <summary>
        /// Gets or sets an optional argument expanding on exclusion criteria.
        /// </summary>
        /// <remarks>
        /// The <see cref="MessageId "/> property is an optional argument that specifies additional
        /// exclusion where the literal metadata target is not sufficiently precise. For example,
        /// the <see cref="global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessageAttribute"/> cannot be applied within a method,
        /// and it may be desirable to suppress a violation against a statement in the method that will
        /// give a rule violation, but not against all statements in the method.
        /// </remarks>
        public string? MessageId { get; set; }

        /// <summary>
        /// Gets or sets the justification for suppressing the code analysis message.
        /// </summary>
        public string? Justification { get; set; }
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_CodeAnalysis_UnscopedRefAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Used to indicate a byref escapes and is not scoped.
    /// </summary>
    /// <remarks>
    /// <para>
    /// There are several cases where the C# compiler treats a <see langword="ref"/> as implicitly
    /// <see langword="scoped"/> - where the compiler does not allow the <see langword="ref"/> to escape the method.
    /// </para>
    /// <para>
    /// For example:
    /// <list type="number">
    ///     <item><see langword="this"/> for <see langword="struct"/> instance methods.</item>
    ///     <item><see langword="ref"/> parameters that refer to <see langword="ref"/> <see langword="struct"/> types.</item>
    ///     <item><see langword="out"/> parameters.</item>
    /// </list>
    /// </para>
    /// <para>
    /// This attribute is used in those instances where the <see langword="ref"/> should be allowed to escape.
    /// </para>
    /// <para>
    /// Applying this attribute, in any form, has impact on consumers of the applicable API. It is necessary for
    /// API authors to understand the lifetime implications of applying this attribute and how it may impact their users.
    /// </para>
    /// </remarks>
    [AttributeUsageAttribute(
        AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter,
        AllowMultiple = false,
        Inherited = false)]
    internal sealed class UnscopedRefAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnscopedRefAttribute"/> class.
        /// </summary>
        public UnscopedRefAttribute() { }
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Diagnostics_StackTraceHiddenAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics
{
    /// <summary>
    /// Types and Methods attributed with StackTraceHidden will be omitted from the stack trace text shown in StackTrace.ToString()
    /// and Exception.StackTrace
    /// </summary>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Class |
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Constructor |
        global::System.AttributeTargets.Struct,
        Inherited = false)]
    internal sealed class StackTraceHiddenAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Diagnostics.StackTraceHiddenAttribute"/> class.
        /// </summary>
        public StackTraceHiddenAttribute() { }
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Index { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System
{
    /// <summary>Represent a type can be used to index a collection either from the start or the end.</summary>
    /// <remarks>
    /// Index is used by the C# compiler to support the new index syntax
    /// <code>
    /// int[] someArray = new int[5] { 1, 2, 3, 4, 5 } ;
    /// int lastElement = someArray[^1]; // lastElement = 5
    /// </code>
    /// </remarks>
    internal readonly struct Index : global::System.IEquatable<global::System.Index>
    {
        private readonly int _value;

        /// <summary>Construct an Index using a value and indicating if the index is from the start or from the end.</summary>
        /// <param name="value">The index value. it has to be zero or positive number.</param>
        /// <param name="fromEnd">Indicating if the index is from the start or from the end.</param>
        /// <remarks>
        /// If the Index constructed from the end, index value 1 means pointing at the last element and index value 0 means pointing at beyond last element.
        /// </remarks>
        [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Index(int value, bool fromEnd = false)
        {
            if (value < 0)
            {
                global::System.Index.ThrowHelper.ThrowValueArgumentOutOfRange_NeedNonNegNumException();
            }

            if (fromEnd)
                _value = ~value;
            else
                _value = value;
        }

        // The following private constructors mainly created for perf reason to avoid the checks
        private Index(int value)
        {
            _value = value;
        }

        /// <summary>Create an Index pointing at first element.</summary>
        public static global::System.Index Start => new global::System.Index(0);

        /// <summary>Create an Index pointing at beyond last element.</summary>
        public static global::System.Index End => new global::System.Index(~0);

        /// <summary>Create an Index from the start at the position indicated by the value.</summary>
        /// <param name="value">The index value from the start.</param>
        [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static global::System.Index FromStart(int value)
        {
            if (value < 0)
            {
                global::System.Index.ThrowHelper.ThrowValueArgumentOutOfRange_NeedNonNegNumException();
            }

            return new global::System.Index(value);
        }

        /// <summary>Create an Index from the end at the position indicated by the value.</summary>
        /// <param name="value">The index value from the end.</param>
        [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static global::System.Index FromEnd(int value)
        {
            if (value < 0)
            {
                global::System.Index.ThrowHelper.ThrowValueArgumentOutOfRange_NeedNonNegNumException();
            }

            return new global::System.Index(~value);
        }

        /// <summary>Returns the index value.</summary>
        public int Value
        {
            get
            {
                if (_value < 0)
                    return ~_value;
                else
                    return _value;
            }
        }

        /// <summary>Indicates whether the index is from the start or the end.</summary>
        public bool IsFromEnd => _value < 0;

        /// <summary>Calculate the offset from the start using the giving collection length.</summary>
        /// <param name="length">The length of the collection that the Index will be used with. length has to be a positive value</param>
        /// <remarks>
        /// For performance reason, we don't validate the input length parameter and the returned offset value against negative values.
        /// we don't validate either the returned offset is greater than the input length.
        /// It is expected Index will be used with collections which always have non negative length/count. If the returned offset is negative and
        /// then used to index a collection will get out of range exception which will be same affect as the validation.
        /// </remarks>
        [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public int GetOffset(int length)
        {
            int offset = _value;
            if (IsFromEnd)
            {
                // offset = length - (~value)
                // offset = length + (~(~value) + 1)
                // offset = length + value + 1

                offset += length + 1;
            }
            return offset;
        }

        /// <summary>Indicates whether the current Index object is equal to another object of the same type.</summary>
        /// <param name="value">An object to compare with this object</param>
        public override bool Equals([global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? value) => value is global::System.Index && _value == ((global::System.Index)value)._value;

        /// <summary>Indicates whether the current Index object is equal to another Index object.</summary>
        /// <param name="other">An object to compare with this object</param>
        public bool Equals(global::System.Index other) => _value == other._value;

        /// <summary>Returns the hash code for this instance.</summary>
        public override int GetHashCode() => _value;

        /// <summary>Converts integer number to an Index.</summary>
        public static implicit operator global::System.Index(int value) => FromStart(value);

        /// <summary>Converts the value of the current Index object to its equivalent string representation.</summary>
        public override string ToString()
        {
            if (IsFromEnd)
                return ToStringFromEnd();

            return ((uint)Value).ToString();
        }

        private string ToStringFromEnd()
        {
            return '^' + Value.ToString();
        }

        private static class ThrowHelper
        {
            [global::System.Diagnostics.CodeAnalysis.DoesNotReturn]
            public static void ThrowValueArgumentOutOfRange_NeedNonNegNumException()
            {
                throw new global::System.ArgumentOutOfRangeException("value", "Non-negative number required.");
            }
        }
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Range { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System
{
    /// <summary>Represent a range has start and end indexes.</summary>
    /// <remarks>
    /// Range is used by the C# compiler to support the range syntax.
    /// <code>
    /// int[] someArray = new int[5] { 1, 2, 3, 4, 5 };
    /// int[] subArray1 = someArray[0..2]; // { 1, 2 }
    /// int[] subArray2 = someArray[1..^0]; // { 2, 3, 4, 5 }
    /// </code>
    /// </remarks>
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal readonly struct Range : global::System.IEquatable<global::System.Range>
    {
        /// <summary>Represent the inclusive start index of the Range.</summary>
        public global::System.Index Start { get; }

        /// <summary>Represent the exclusive end index of the Range.</summary>
        public global::System.Index End { get; }

        /// <summary>Construct a Range object using the start and end indexes.</summary>
        /// <param name="start">Represent the inclusive start index of the range.</param>
        /// <param name="end">Represent the exclusive end index of the range.</param>
        public Range(global::System.Index start, global::System.Index end)
        {
            Start = start;
            End = end;
        }

        /// <summary>Indicates whether the current Range object is equal to another object of the same type.</summary>
        /// <param name="value">An object to compare with this object</param>
        public override bool Equals([global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? value) =>
            value is global::System.Range r &&
            r.Start.Equals(Start) &&
            r.End.Equals(End);

        /// <summary>Indicates whether the current Range object is equal to another Range object.</summary>
        /// <param name="other">An object to compare with this object</param>
        public bool Equals(global::System.Range other) => other.Start.Equals(Start) && other.End.Equals(End);

        /// <summary>Returns the hash code for this instance.</summary>
        public override int GetHashCode()
        {
            return global::System.Range.HashHelpers.Combine(Start.GetHashCode(), End.GetHashCode());
        }

        /// <summary>Converts the value of the current Range object to its equivalent string representation.</summary>
        public override string ToString()
        {
            return Start.ToString() + ".." + End.ToString();
        }

        /// <summary>Create a Range object starting from start index to the end of the collection.</summary>
        public static global::System.Range StartAt(global::System.Index start) => new global::System.Range(start, global::System.Index.End);

        /// <summary>Create a Range object starting from first element in the collection to the end Index.</summary>
        public static global::System.Range EndAt(global::System.Index end) => new global::System.Range(global::System.Index.Start, end);

        /// <summary>Create a Range object starting from first element to the end.</summary>
        public static global::System.Range All => new global::System.Range(global::System.Index.Start, global::System.Index.End);

        /// <summary>Calculate the start offset and length of range object using a collection length.</summary>
        /// <param name="length">The length of the collection that the range will be used with. length has to be a positive value.</param>
        /// <remarks>
        /// For performance reason, we don't validate the input length parameter against negative values.
        /// It is expected Range will be used with collections which always have non negative length/count.
        /// We validate the range is inside the length scope though.
        /// </remarks>
        [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public (int Offset, int Length) GetOffsetAndLength(int length)
        {
            int start;
            global::System.Index startIndex = Start;
            if (startIndex.IsFromEnd)
                start = length - startIndex.Value;
            else
                start = startIndex.Value;

            int end;
            global::System.Index endIndex = End;
            if (endIndex.IsFromEnd)
                end = length - endIndex.Value;
            else
                end = endIndex.Value;

            if ((uint)end > (uint)length || (uint)start > (uint)end)
            {
                global::System.Range.ThrowHelper.ThrowArgumentOutOfRangeException();
            }

            return (start, end - start);
        }

        private static class HashHelpers
        {
            public static int Combine(int h1, int h2)
            {
                uint rol5 = ((uint)h1 << 5) | ((uint)h1 >> 27);
                return ((int)rol5 + h1) ^ h2;
            }
        }

        private static class ThrowHelper
        {
            [global::System.Diagnostics.CodeAnalysis.DoesNotReturn]
            public static void ThrowArgumentOutOfRangeException()
            {
                throw new global::System.ArgumentOutOfRangeException("length");
            }
        }
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Runtime_CompilerServices_AsyncMethodBuilderAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Indicates the type of the async method builder that should be used by a language compiler to
    /// build the attributed async method or to build the attributed type when used as the return type
    /// of an async method.
    /// </summary>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Class |
        global::System.AttributeTargets.Struct |
        global::System.AttributeTargets.Interface |
        global::System.AttributeTargets.Delegate |
        global::System.AttributeTargets.Enum |
        global::System.AttributeTargets.Method,
        Inherited = false, AllowMultiple = false)]
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal sealed class AsyncMethodBuilderAttribute : global::System.Attribute
    {
        /// <summary>Initializes the <see cref="global::System.Runtime.CompilerServices.AsyncMethodBuilderAttribute"/>.</summary>
        /// <param name="builderType">The <see cref="global::System.Type"/> of the associated builder.</param>
        public AsyncMethodBuilderAttribute(global::System.Type builderType) => BuilderType = builderType;

        /// <summary>Gets the <see cref="global::System.Type"/> of the associated builder.</summary>
        public global::System.Type BuilderType { get; }
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Runtime_CompilerServices_CallerArgumentExpressionAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// An attribute that allows parameters to receive the expression of other parameters.
    /// </summary>
    [global::System.AttributeUsage(global::System.AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    internal sealed class CallerArgumentExpressionAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Runtime.CompilerServices.CallerArgumentExpressionAttribute"/> class.
        /// </summary>
        /// <param name="parameterName">The condition parameter value.</param>
        public CallerArgumentExpressionAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }

        /// <summary>
        /// Gets the parameter name the expression is retrieved from.
        /// </summary>
        public string ParameterName { get; }
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Runtime_CompilerServices_CompilerFeatureRequiredAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Indicates that compiler support for a particular feature is required for the location where this attribute is applied.
    /// </summary>
    [global::System.AttributeUsage(global::System.AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    internal sealed class CompilerFeatureRequiredAttribute : global::System.Attribute
    {
        /// <summary>
        /// Creates a new instance of the <see cref="global::System.Runtime.CompilerServices.CompilerFeatureRequiredAttribute"/> type.
        /// </summary>
        /// <param name="featureName">The name of the feature to indicate.</param>
        public CompilerFeatureRequiredAttribute(string featureName)
        {
            FeatureName = featureName;
        }

        /// <summary>
        /// The name of the compiler feature.
        /// </summary>
        public string FeatureName { get; }

        /// <summary>
        /// If true, the compiler can choose to allow access to the location where this attribute is applied if it does not understand <see cref="FeatureName"/>.
        /// </summary>
        public bool IsOptional { get; set; }

        /// <summary>
        /// The <see cref="FeatureName"/> used for the ref structs C# feature.
        /// </summary>
        public const string RefStructs = nameof(RefStructs);

        /// <summary>
        /// The <see cref="FeatureName"/> used for the required members C# feature.
        /// </summary>
        public const string RequiredMembers = nameof(RequiredMembers);
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Runtime_CompilerServices_DisableRuntimeMarshallingAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Disables the built-in runtime managed/unmanaged marshalling subsystem for
    /// P/Invokes, Delegate types, and unmanaged function pointer invocations.
    /// </summary>
    /// <remarks>
    /// The built-in marshalling subsystem has some behaviors that cannot be changed due to
    /// backward-compatibility requirements. This attribute allows disabling the built-in
    /// subsystem and instead uses the following rules for P/Invokes, Delegates,
    /// and unmanaged function pointer invocations:
    ///
    /// - All value types that do not contain reference type fields recursively (<c>unmanaged</c> in C#) are blittable
    /// - Value types that recursively have any fields that have <c>[StructLayout(LayoutKind.Auto)]</c> are disallowed from interop.
    /// - All reference types are disallowed from usage in interop scenarios.
    /// - SetLastError support in P/Invokes is disabled.
    /// - varargs support is disabled.
    /// - LCIDConversionAttribute support is disabled.
    /// </remarks>
    [global::System.AttributeUsage(global::System.AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    internal sealed class DisableRuntimeMarshallingAttribute : global::System.Attribute
    {
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Runtime_CompilerServices_InterpolatedStringHandlerArgumentAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Indicates which arguments to a method involving an interpolated string handler should be passed to that handler.
    /// </summary>
    [global::System.AttributeUsage(global::System.AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    internal sealed class InterpolatedStringHandlerArgumentAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Runtime.CompilerServices.InterpolatedStringHandlerArgumentAttribute"/> class.
        /// </summary>
        /// <param name="argument">The name of the argument that should be passed to the handler.</param>
        /// <remarks><see langword="null"/> may be used as the name of the receiver in an instance method.</remarks>
        public InterpolatedStringHandlerArgumentAttribute(string argument)
        {
            Arguments = new string[] { argument };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Runtime.CompilerServices.InterpolatedStringHandlerArgumentAttribute"/> class.
        /// </summary>
        /// <param name="arguments">The names of the arguments that should be passed to the handler.</param>
        /// <remarks><see langword="null"/> may be used as the name of the receiver in an instance method.</remarks>
        public InterpolatedStringHandlerArgumentAttribute(params string[] arguments)
        {
            Arguments = arguments;
        }

        /// <summary>
        /// Gets the names of the arguments that should be passed to the handler.
        /// </summary>
        /// <remarks><see langword="null"/> may be used as the name of the receiver in an instance method.</remarks>
        public string[] Arguments { get; }
    }
}

"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Runtime_CompilerServices_InterpolatedStringHandlerAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Indicates the attributed type is to be used as an interpolated string handler.
    /// </summary>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Class |
        global::System.AttributeTargets.Struct,
        AllowMultiple = false, Inherited = false)]
    internal sealed class InterpolatedStringHandlerAttribute : global::System.Attribute
    {
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Runtime_CompilerServices_IsExternalInit { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Reserved to be used by the compiler for tracking metadata.
    /// This class should not be used by developers in source code.
    /// </summary>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    internal static class IsExternalInit
    {
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Runtime_CompilerServices_ModuleInitializerAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Used to indicate to the compiler that a method should be called
    /// in its containing module's initializer.
    /// </summary>
    /// <remarks>
    /// When one or more valid methods
    /// with this attribute are found in a compilation, the compiler will
    /// emit a module initializer which calls each of the attributed methods.
    ///
    /// Certain requirements are imposed on any method targeted with this attribute:
    /// - The method must be `static`.
    /// - The method must be an ordinary member method, as opposed to a property accessor, constructor, local function, etc.
    /// - The method must be parameterless.
    /// - The method must return `void`.
    /// - The method must not be generic or be contained in a generic type.
    /// - The method's effective accessibility must be `internal` or `public`.
    ///
    /// The specification for module initializers in the .NET runtime can be found here:
    /// https://github.com/dotnet/runtime/blob/main/docs/design/specs/Ecma-335-Augments.md#module-initializer
    /// </remarks>
    [global::System.AttributeUsage(global::System.AttributeTargets.Method, Inherited = false)]
    internal sealed class ModuleInitializerAttribute : global::System.Attribute
    {
        public ModuleInitializerAttribute()
        {
        }
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Runtime_CompilerServices_RequiredMemberAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Specifies that a type has required members or that a member is required.
    /// </summary>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Class |
        global::System.AttributeTargets.Struct |
        global::System.AttributeTargets.Field |
        global::System.AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = false)]
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal sealed class RequiredMemberAttribute : global::System.Attribute
    {
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Runtime_CompilerServices_SkipLocalsInitAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Used to indicate to the compiler that the <c>.locals init</c> flag should not be set in method headers.
    /// </summary>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Module |
        global::System.AttributeTargets.Class |
        global::System.AttributeTargets.Struct |
        global::System.AttributeTargets.Interface |
        global::System.AttributeTargets.Constructor |
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Property |
        global::System.AttributeTargets.Event,
        Inherited = false)]
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal sealed class SkipLocalsInitAttribute : global::System.Attribute
    {
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Runtime_InteropServices_SuppressGCTransitionAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.InteropServices
{
    /// <summary>
    /// An attribute used to indicate a GC transition should be skipped when making an unmanaged function call.
    /// </summary>
    /// <example>
    /// Example of a valid use case. The Win32 `GetTickCount()` function is a small performance related function
    /// that reads some global memory and returns the value. In this case, the GC transition overhead is significantly
    /// more than the memory read.
    /// <code>
    /// using System;
    /// using System.Runtime.InteropServices;
    /// class Program
    /// {
    ///     [DllImport("Kernel32")]
    ///     [SuppressGCTransition]
    ///     static extern int GetTickCount();
    ///     static void Main()
    ///     {
    ///         Console.WriteLine($"{GetTickCount()}");
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <remarks>
    /// This attribute is ignored if applied to a method without the <see cref="global::System.Runtime.InteropServices.DllImportAttribute"/>.
    ///
    /// Forgoing this transition can yield benefits when the cost of the transition is more than the execution time
    /// of the unmanaged function. However, avoiding this transition removes some of the guarantees the runtime
    /// provides through a normal P/Invoke. When exiting the managed runtime to enter an unmanaged function the
    /// GC must transition from Cooperative mode into Preemptive mode. Full details on these modes can be found at
    /// https://github.com/dotnet/runtime/blob/main/docs/coding-guidelines/clr-code-guide.md#2.1.8.
    /// Suppressing the GC transition is an advanced scenario and should not be done without fully understanding
    /// potential consequences.
    ///
    /// One of these consequences is an impact to Mixed-mode debugging (https://docs.microsoft.com/visualstudio/debugger/how-to-debug-in-mixed-mode).
    /// During Mixed-mode debugging, it is not possible to step into or set breakpoints in a P/Invoke that
    /// has been marked with this attribute. A workaround is to switch to native debugging and set a breakpoint in the native function.
    /// In general, usage of this attribute is not recommended if debugging the P/Invoke is important, for example
    /// stepping through the native code or diagnosing an exception thrown from the native code.
    ///
    /// The runtime may load the native library for method marked with this attribute in advance before the method is called for the first time.
    /// Usage of this attribute is not recommended for platform neutral libraries with conditional platform specific code.
    ///
    /// The P/Invoke method that this attribute is applied to must have all of the following properties:
    ///   * Native function always executes for a trivial amount of time (less than 1 microsecond).
    ///   * Native function does not perform a blocking syscall (e.g. any type of I/O).
    ///   * Native function does not call back into the runtime (e.g. Reverse P/Invoke).
    ///   * Native function does not throw exceptions.
    ///   * Native function does not manipulate locks or other concurrency primitives.
    ///
    /// Consequences of invalid uses of this attribute:
    ///   * GC starvation.
    ///   * Immediate runtime termination.
    ///   * Data corruption.
    /// </remarks>
    [global::System.AttributeUsage(global::System.AttributeTargets.Method, Inherited = false)]
    internal sealed class SuppressGCTransitionAttribute : global::System.Attribute
    {
        public SuppressGCTransitionAttribute()
        {
        }
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Runtime_InteropServices_UnmanagedCallersOnlyAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.InteropServices
{
    /// <summary>
    /// Any method marked with <see cref="global::System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute" /> can be directly called from
    /// native code. The function token can be loaded to a local variable using the <see href="https://docs.microsoft.com/dotnet/csharp/language-reference/operators/pointer-related-operators#address-of-operator-">address-of</see> operator
    /// in C# and passed as a callback to a native method.
    /// </summary>
    /// <remarks>
    /// Methods marked with this attribute have the following restrictions:
    ///   * Method must be marked "static".
    ///   * Must not be called from managed code.
    ///   * Must only have <see href="https://docs.microsoft.com/dotnet/framework/interop/blittable-and-non-blittable-types">blittable</see> arguments.
    /// </remarks>
    [global::System.AttributeUsage(global::System.AttributeTargets.Method, Inherited = false)]
    internal sealed class UnmanagedCallersOnlyAttribute : global::System.Attribute
    {
        public UnmanagedCallersOnlyAttribute()
        {
        }

        /// <summary>
        /// Optional. If omitted, the runtime will use the default platform calling convention.
        /// </summary>
        /// <remarks>
        /// Supplied types must be from the official "System.Runtime.CompilerServices" namespace and
        /// be of the form "CallConvXXX".
        /// </remarks>
        public global::System.Type[]? CallConvs;

        /// <summary>
        /// Optional. If omitted, no named export is emitted during compilation.
        /// </summary>
        public string? EntryPoint;
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Runtime_Versioning_ObsoletedOSPlatformAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.Versioning
{
    /// <summary>
    /// Marks APIs that were obsoleted in a given operating system version.
    /// </summary>
    /// <remarks>
    /// Primarily used by OS bindings to indicate APIs that should not be used anymore.
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
    internal sealed class ObsoletedOSPlatformAttribute : Attribute // global::System.Runtime.Versioning.OSPlatformAttribute
    {
        public ObsoletedOSPlatformAttribute(string platformName)
            //: base(platformName)
        {
        }

        public ObsoletedOSPlatformAttribute(string platformName, string? message)
            //: base(platformName)
        {
            Message = message;
        }

        public string? Message { get; }

        public string? Url { get; set; }
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Runtime_Versioning_RequiresPreviewFeaturesAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.Versioning
{
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Assembly |
        global::System.AttributeTargets.Module |
        global::System.AttributeTargets.Class |
        global::System.AttributeTargets.Interface |
        global::System.AttributeTargets.Delegate |
        global::System.AttributeTargets.Struct |
        global::System.AttributeTargets.Enum |
        global::System.AttributeTargets.Constructor |
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Property |
        global::System.AttributeTargets.Field |
        AttributeTargets.Event, Inherited = false)]
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal sealed class RequiresPreviewFeaturesAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Runtime.Versioning.RequiresPreviewFeaturesAttribute"/> class.
        /// </summary>
        public RequiresPreviewFeaturesAttribute() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Runtime.Versioning.RequiresPreviewFeaturesAttribute"/> class with the specified message.
        /// </summary>
        /// <param name="message">An optional message associated with this attribute instance.</param>
        public RequiresPreviewFeaturesAttribute(string? message)
        {
            Message = message;
        }

        /// <summary>
        /// Returns the optional message associated with this attribute instance.
        /// </summary>
        public string? Message { get; }

        /// <summary>
        /// Returns the optional URL associated with this attribute instance.
        /// </summary>
        public string? Url { get; set; }
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Runtime_Versioning_SupportedOSPlatformAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.Versioning
{
    /// <summary>
    /// Records the operating system (and minimum version) that supports an API. Multiple attributes can be
    /// applied to indicate support on multiple operating systems.
    /// </summary>
    /// <remarks>
    /// Callers can apply a <see cref="global::System.Runtime.Versioning.SupportedOSPlatformAttribute " />
    /// or use guards to prevent calls to APIs on unsupported operating systems.
    ///
    /// A given platform should only be specified once.
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
    internal sealed class SupportedOSPlatformAttribute : Attribute // global::System.Runtime.Versioning.OSPlatformAttribute
    {
        public SupportedOSPlatformAttribute(string platformName)
            //: base(platformName)
        {
        }
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Runtime_Versioning_SupportedOSPlatformGuardAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.Versioning
{
    /// <summary>
    /// Annotates a custom guard field, property or method with a supported platform name and optional version.
    /// Multiple attributes can be applied to indicate guard for multiple supported platforms.
    /// </summary>
    /// <remarks>
    /// Callers can apply a <see cref="global::System.Runtime.Versioning.SupportedOSPlatformGuardAttribute " /> to a field, property or method
    /// and use that field, property or method in a conditional or assert statements in order to safely call platform specific APIs.
    ///
    /// The type of the field or property should be boolean, the method return type should be boolean in order to be used as platform guard.
    /// </remarks>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Field |
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Property,
        AllowMultiple = true, Inherited = false)]
    internal sealed class SupportedOSPlatformGuardAttribute : Attribute // global::System.Runtime.Versioning.OSPlatformAttribute
    {
        public SupportedOSPlatformGuardAttribute(string platformName)
            //: base(platformName)
        {
        }
    }
}
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Runtime_Versioning_TargetPlatformAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

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
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Runtime_Versioning_UnsupportedOSPlatformAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

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
"""""""""", Encoding.UTF8);
    
    public static SourceText Source_T_System_Runtime_Versioning_UnsupportedOSPlatformGuardAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.Versioning
{
    /// <summary>
    /// Annotates the custom guard field, property or method with an unsupported platform name and optional version.
    /// Multiple attributes can be applied to indicate guard for multiple unsupported platforms.
    /// </summary>
    /// <remarks>
    /// Callers can apply a <see cref="global::System.Runtime.Versioning.UnsupportedOSPlatformGuardAttribute " /> to a field, property or method
    /// and use that  field, property or method in a conditional or assert statements as a guard to safely call APIs unsupported on those platforms.
    ///
    /// The type of the field or property should be boolean, the method return type should be boolean in order to be used as platform guard.
    /// </remarks>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Field |
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Property,
        AllowMultiple = true, Inherited = false)]
    internal sealed class UnsupportedOSPlatformGuardAttribute : Attribute // global::System.Runtime.Versioning.OSPlatformAttribute
    {
        public UnsupportedOSPlatformGuardAttribute(string platformName)
            //: base(platformName)
        {
        }
    }
}
"""""""""", Encoding.UTF8);
    

}

