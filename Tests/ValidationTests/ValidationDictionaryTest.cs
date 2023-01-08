using FluentValidation.TestHelper;
using SODP.Application.Validators;
using SODP.Model;
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
                Sign = null
            };

            var result = _validator.TestValidate(dictionary);

            result.ShouldHaveValidationErrorFor(b => b.Sign);
        }

        [Fact]
        public void should_not_have_error_when_sign_is_specified()
        {
            var dictionary = new AppDictionary()
            {
                Sign = "PARTS"
            };

            var result = _validator.TestValidate(dictionary);

            result.ShouldNotHaveValidationErrorFor(b => b.Sign);
        }
    }
}
