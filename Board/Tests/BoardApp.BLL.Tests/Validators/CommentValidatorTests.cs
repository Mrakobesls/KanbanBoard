using System;
using BoardApp.BLL.Validators;
using BoardApp.Common.Models;
using FluentValidation.TestHelper;
using Xunit;

namespace BoardApp.BLL.Tests.Validators
{
    public class CommentValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_WhenTextIsNullOrEmpty_ThenThrow(string text)
        {
            //Arrange
            var comment = new CommentDto { Text = text, DateTime = DateTime.Now };
            var validator = new CommentValidator();

            //Act
            var result = validator.TestValidate(comment);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Text).WithErrorMessage($"'{nameof(CommentDto.Text)}' must not be empty.");
        }

        [Fact]
        public void Validate_WhenDateTimeMinValue_ThenThrow()
        {
            //Arrange
            var comment = new CommentDto { Text = "new" };
            var validator = new CommentValidator();

            //Act
            var result = validator.TestValidate(comment);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.DateTime).WithErrorMessage($"'{nameof(CommentDto.DateTime)}' must be greater than '{DateTime.MinValue}'.");
        }

        [Fact]
        public void Validate_WhenModelIsRight_ThenPass()
        {
            //Arrange
            var comment = new CommentDto { Text = "new", DateTime = DateTime.Now };
            var validator = new CommentValidator();

            //Act
            var result = validator.TestValidate(comment);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
