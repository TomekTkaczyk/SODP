using SODP.Model.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.ExtensionsTests
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void When_string_is_empty_GetUntilOrEmpty_should_by_return_empty_string(string s)
        {
            Assert.Equal(string.Empty, s.GetUntilOrEmpty("_"));
        }

        [Theory]
        [InlineData("some text")]
        [InlineData("sometext")]
        public void When_string_not_contain_underscore_GetUntilOrEmpty_should_by_return_this_text(string s)
        {
            var newString = s.GetLastUntilOrEmpty("_");

            Assert.Equal(s, newString);
        }
    }
}
