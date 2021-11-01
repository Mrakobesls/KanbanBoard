using BoardApp.BLL.Validators;
using BoardApp.Common.Models;
using FluentValidation.TestHelper;
using Xunit;

namespace BoardApp.BLL.Tests.Validators
{
    public class CardValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_WhenTitleIsNullOrEmpty_ThenThrow(string title)
        {
            //Arrange
            var card = new CardDto {Title = title};
            var validator = new CardValidator();

            //Act
            var result = validator.TestValidate(card);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Title).WithErrorMessage($"'{nameof(CardDto.Title)}' must not be empty.");
        }

        [Fact]
        public void Validate_WhenModelIsRight_ThenPass()
        {
            //Arrange
            var card = new CardDto { Title = "new" };
            var validator = new CardValidator();

            //Act
            var result = validator.TestValidate(card);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
