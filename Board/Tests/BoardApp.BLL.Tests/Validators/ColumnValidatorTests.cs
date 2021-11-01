using BoardApp.BLL.Validators;
using BoardApp.Common.Models;
using FluentValidation.TestHelper;
using Xunit;

namespace BoardApp.BLL.Tests.Validators
{
    public class ColumnValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_WhenTitleIsNullOrEmpty_ThenThrow(string title)
        {
            //Arrange
            var column = new ColumnDto { Title = title };
            var validator = new ColumnValidator();

            //Act & Assert
            var result = validator.TestValidate(column);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Title).WithErrorMessage($"'{nameof(ColumnDto.Title)}' must not be empty.");
        }

        [Fact]
        public void Validate_WhenModelIsRight_ThenPass()
        {
            //Arrange
            var column = new ColumnDto { Title = "new" };
            var validator = new ColumnValidator();

            //Act
            var result = validator.TestValidate(column);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
