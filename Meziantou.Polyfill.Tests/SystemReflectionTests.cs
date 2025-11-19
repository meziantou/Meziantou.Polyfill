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

public class SystemReflectionTests
{
        [Fact]
        public void Type_GetConstructor()
        {
            Assert.NotNull(typeof(Exception).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                new Type[]
                {
                    typeof(SerializationInfo),
                    typeof(StreamingContext)
                })
            );
            Assert.NotNull(typeof(Random).GetConstructor(BindingFlags.Instance | BindingFlags.Public, Array.Empty<Type>()));
            Assert.Null(typeof(Random).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, new Type[] { typeof(IntPtr) }));
        }

        [Fact]
        public void Type_GetMethod()
        {
            Assert.NotNull(typeof(Exception).GetMethod("get_Message", BindingFlags.Instance | BindingFlags.Public, Array.Empty<Type>()));
            Assert.Null(typeof(Exception).GetMethod("set_Message", BindingFlags.Instance | BindingFlags.Public, Array.Empty<Type>()));
        }

        [Fact]
        public void Type_IsAssignableTo()
        {
            Assert.True(typeof(string).IsAssignableTo(typeof(object)));
            Assert.True(typeof(string).IsAssignableTo(typeof(string)));
            Assert.False(typeof(string).IsAssignableTo(typeof(int)));
        }

}