using FluentAssertions;
using FsCheck;
using System;
using System.Collections.Generic;
using Verve.Identity.Core.Service;
using Xunit;

namespace Verve.Identity.Core.Tests
{
    public class StringExtensionTests
    {
        [InlineData("ABcDef1234rere", "ABCDEF1234RERE")]
        [InlineData("ABcDef&1*'234", "ABCDEF1234")]
        [InlineData("@ABcDef1234", "ABCDEF1234")]
        [InlineData("ABcDef1234", "ABCDEF1234")]
        [Theory]
        public void NormalizeStringTest(string test, string expected)
        {
            var result = test.NormalizedString();
            result.Should().Be(expected);
        }

        [InlineData("ABcDef*1234**rere", "*", "ABCDEF*1234**RERE")]
        [InlineData("@AB@cDef&1*'234", "@*", "@AB@CDEF1*234")]
        [InlineData("@ABcDef1234", "@*", "@ABCDEF1234")]
        [Theory]
        public void Normalize_And_Ignore_Skip(string test, string skip, string expected)
        {
            var result = test.NormalizedString(skip);
            result.Should().Be(expected);
        }

        public static IEnumerable<Object[]> TestStrings()
        {
            var gen = Arb.Generate<string[]>();
            return gen.Sample(20, 10);
        }
    }
}