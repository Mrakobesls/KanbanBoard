using BoardApp.BLL.Validators;
using BoardApp.Common.Models;
using FluentValidation.TestHelper;
using Xunit;


namespace BoardApp.BLL.Tests.Validators
{
    public class PermissionValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_WhenNameIsNullOrEmpty_ThenThrow(string name)
        {
            //Arrange
            var permission = new PermissionDto { Name = name };
            var validator = new PermissionValidator();

            //Act
            var result = validator.TestValidate(permission);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage($"'{nameof(PermissionDto.Name)}' must not be empty.");
        }

        [Fact]
        public void Validate_WhenNameMoreThan30_ThenThrow()
        {
            //Arrange
            var name = new string('1', 31);
            var permission = new PermissionDto { Name = name };
            var validator = new PermissionValidator();

            //Act
            var result = validator.TestValidate(permission);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage($"The length of '{nameof(PermissionDto.Name)}' must be 30 characters or fewer. You entered 31 characters.");
        }

        [Fact]
        public void Validate_WhenModelIsRight_ThenPass()
        {
            //Arrange
            var permission = new PermissionDto { Name = "new" };
            var validator = new PermissionValidator();

            //Act
            var result = validator.TestValidate(permission);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
