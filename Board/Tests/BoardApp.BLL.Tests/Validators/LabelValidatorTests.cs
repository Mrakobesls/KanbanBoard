using BoardApp.BLL.Validators;
using BoardApp.Common.Models;
using FluentValidation.TestHelper;
using Xunit;

namespace BoardApp.BLL.Tests.Validators
{
    public class LabelValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_WhenNameIsNullOrEmpty_ThenThrow(string name)
        {
            //Arrange
            var label = new LabelDto {Name = name};
            var validator = new LabelValidator();

            //Act
            var result = validator.TestValidate(label);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage($"'{nameof(LabelDto.Name)}' must not be empty.");
        }

        [Fact]
        public void Validate_WhenNameMoreThan30_ThenThrow()
        {
            //Arrange
            var name = new string('1', 31);
            var label = new LabelDto { Name = name };
            var validator = new LabelValidator();

            //Act
            var result = validator.TestValidate(label);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage($"The length of '{nameof(LabelDto.Name)}' must be 30 characters or fewer. You entered 31 characters.");
        }

        [Fact]
        public void Validate_WhenModelIsRight_ThenPass()
        {
            //Arrange
            var label = new LabelDto { Name = "new"};
            var validator = new LabelValidator();

            //Act
            var result = validator.TestValidate(label);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
