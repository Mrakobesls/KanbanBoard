using BoardApp.BLL.Validators;
using BoardApp.Common.Models;
using FluentValidation.TestHelper;
using Xunit;

namespace BoardApp.BLL.Tests.Validators
{
    public class BoardValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_WhenTitleIsNullOrEmpty_ThenThrow(string title)
        {
            //Arrange
            var board = new BoardDto {Title = title};
            var validator = new BoardValidator();

            //Act
            var result = validator.TestValidate(board);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Title).WithErrorMessage($"'{nameof(BoardDto.Title)}' must not be empty.");
        }


        [Fact]
        public void Validate_WhenModelIsRight_ThenPass()
        {
            //Arrange
            var board = new BoardDto { Title = "new" };
            var validator = new BoardValidator();

            //Act
            var result = validator.TestValidate(board);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
