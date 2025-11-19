using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public class AttributesTests
{
        [Fact]
        public void AttributesAreAvailable()
        {
            _ = new AllowNullAttribute();
            _ = new DisallowNullAttribute();
            _ = new DoesNotReturnAttribute();
            _ = new DoesNotReturnIfAttribute(true);
            _ = new DynamicallyAccessedMembersAttribute(DynamicallyAccessedMemberTypes.All);
            _ = new DynamicDependencyAttribute("");
            _ = new MaybeNullAttribute();
            _ = new MaybeNullWhenAttribute(true);
            _ = new MemberNotNullAttribute("");
            _ = new MemberNotNullWhenAttribute(false);
            _ = new NotNullAttribute();
            _ = new NotNullIfNotNullAttribute("");
            _ = new NotNullWhenAttribute(false);
            _ = new RequiresAssemblyFilesAttribute();
            _ = new RequiresDynamicCodeAttribute("");
            _ = new RequiresUnreferencedCodeAttribute("");
            _ = new SetsRequiredMembersAttribute();
            _ = new StringSyntaxAttribute("");
            _ = new UnconditionalSuppressMessageAttribute("", "");
            _ = new UnscopedRefAttribute();
            _ = new StackTraceHiddenAttribute();
            _ = new AsyncMethodBuilderAttribute(typeof(AttributesTests));
            _ = new CallerArgumentExpressionAttribute("");
            _ = new CompilerFeatureRequiredAttribute("");
            _ = new DisableRuntimeMarshallingAttribute();
            _ = new InterpolatedStringHandlerArgumentAttribute("");
            _ = new InterpolatedStringHandlerAttribute();
            _ = new ModuleInitializerAttribute();
            _ = new RequiredMemberAttribute();
            _ = new SkipLocalsInitAttribute();
            _ = new SuppressGCTransitionAttribute();
            _ = new UnmanagedCallersOnlyAttribute();
            _ = new ObsoletedOSPlatformAttribute("");
            _ = new RequiresPreviewFeaturesAttribute();
            _ = new SupportedOSPlatformAttribute("");
            _ = new SupportedOSPlatformGuardAttribute("");
            _ = new TargetPlatformAttribute("");
            _ = new UnsupportedOSPlatformAttribute("");
            _ = new UnsupportedOSPlatformGuardAttribute("");
            _ = new CollectionBuilderAttribute(typeof(string), "");
            _ = new ExperimentalAttribute("test");
            _ = new OverloadResolutionPriorityAttribute(1);
    
            _ = typeof(IsExternalInit);
        }

}