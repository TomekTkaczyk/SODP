using DocumentFormat.OpenXml.Wordprocessing;
using FluentValidation.TestHelper;
using SODP.Application.Validators;
using SODP.Model;
using System;
using Xunit;

namespace Tests.ValidationTests
{
    public class ValidationDictionaryTest
    {
        private DictionaryValidator _validator;

        public ValidationDictionaryTest()
        {
            _validator = new DictionaryValidator();
        }

        [Fact]
        public void should_have_error_when_sign_is_null()
        {
            var dictionary = new AppDictionary()
            {
                Name = "SPECIFIED"
            };

            var result = _validator.TestValidate(dictionary);

            result.ShouldHaveValidationErrorFor(b => b.Sign);
        }

        [Fact]
        public void should_have_error_when_sign_is_empty()
        {
            var dictionary = new AppDictionary()
            {
                Sign = "",
                Name = "SPECIFIED"
            };

            var result = _validator.TestValidate(dictionary);

            result.ShouldHaveValidationErrorFor(b => b.Sign);
        }

        [Fact]
        public void should_have_error_when_name_is_null()
        {
            var dictionary = new AppDictionary()
            {
                Sign = "SPECIFIED",
            };

            var result = _validator.TestValidate(dictionary);

            result.ShouldHaveValidationErrorFor(b => b.Name);
        }

        [Fact]
        public void should_have_error_when_name_is_empty()
        {
            var dictionary = new AppDictionary()
            {
                Sign = "SPECIFIED",
                Name = "",
            };

            var result = _validator.TestValidate(dictionary);

            result.ShouldHaveValidationErrorFor(b => b.Name);
        }

        [Fact]
        public void should_not_have_error_when_sign_and_name_is_specified()
        {
            var dictionary = new AppDictionary()
            {
                Sign = "PARTS",
                Name = "CZĘŚCI PROJEKTU"
            };

            var result = _validator.TestValidate(dictionary);

            result.ShouldNotHaveValidationErrorFor(b => b.Sign);
            result.ShouldNotHaveValidationErrorFor(b => b.Name);
        }

        [Fact]
        public void should_have_error_when_sign_and_name_is_too_long()
        {
            var dictionary = new AppDictionary()
            {
                Sign = new String('a', 11),
                Name = new String('a',51)
            };

            var result = _validator.TestValidate(dictionary);

            result.ShouldHaveValidationErrorFor(b => b.Sign);
            result.ShouldHaveValidationErrorFor(b => b.Name);
        }

    }
}
