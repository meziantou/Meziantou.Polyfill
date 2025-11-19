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

public class SystemCollectionsObjectModelTests
{
        [Fact]
        public void IList_AsReadOnly()
        {
            var list = new List<int>() { 1, 2 };
            ReadOnlyCollection<int> result = list.AsReadOnly();
            Assert.Equal(list, result);
        }

        [Fact]
        public void IDictionary_AsReadOnly()
        {
            IDictionary<int, string> dict = new Dictionary<int, string>
            {
                { 1, "a" },
                { 2, "b" },
            };
    
            ReadOnlyDictionary<int, string> result = dict.AsReadOnly();
            Assert.Equal(dict, result);
        }

        [Fact]
        public void ReadOnlyDictionary_Empty()
        {
            var empty = ReadOnlyDictionary<int, string>.Empty;
            Assert.NotNull(empty);
            Assert.Empty(empty);
            
            // Verify it's the same instance each time
            Assert.Same(empty, ReadOnlyDictionary<int, string>.Empty);
            
            // Verify different type parameters get different instances
            var emptyInt = ReadOnlyDictionary<string, int>.Empty;
            Assert.NotNull(emptyInt);
            Assert.Empty(emptyInt);
        }

        [Fact]
        public void ReadOnlySet_Empty()
        {
            var empty = ReadOnlySet<int>.Empty;
            Assert.NotNull(empty);
            Assert.Empty(empty);
            
            // Verify it's the same instance each time
            Assert.Same(empty, ReadOnlySet<int>.Empty);
            
            // Verify different type parameters get different instances
            var emptyString = ReadOnlySet<string>.Empty;
            Assert.NotNull(emptyString);
            Assert.Empty(emptyString);
        }

        [Fact]
        public void ReadOnlyCollection_Empty()
        {
            var empty = ReadOnlyCollection<int>.Empty;
            Assert.NotNull(empty);
            Assert.Empty(empty);
            
            // Verify it's the same instance each time
            Assert.Same(empty, ReadOnlyCollection<int>.Empty);
            
            // Verify different type parameters get different instances
            var emptyString = ReadOnlyCollection<string>.Empty;
            Assert.NotNull(emptyString);
            Assert.Empty(emptyString);
        }

        [Fact]
        public void ReadOnlyDictionary_GetValueOrDefaultTests()
        {
            IReadOnlyDictionary<int, int> dictionary = new Dictionary<int, int>
            {
                [1] = 10
            };
    
            Assert.Equal(10, dictionary.GetValueOrDefault(1));
            Assert.Equal(10, dictionary.GetValueOrDefault(1, -1));
    
            // key not present
            Assert.Equal(0, dictionary.GetValueOrDefault(100));
            Assert.Equal(-1, dictionary.GetValueOrDefault(100, -1));
        }

}