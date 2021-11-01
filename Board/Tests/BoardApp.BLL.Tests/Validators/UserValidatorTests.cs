using BoardApp.BLL.Validators;
using BoardApp.Common.Models;
using FluentValidation.TestHelper;
using Xunit;

namespace BoardApp.BLL.Tests.Validators
{
    public class UserValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_WhenFirstNameIsNullOrEmpty_ThenThrow(string name)
        {
            //Arrange
            var user = new UserDto { FirstName = name, LastName = "Tremonin", Login = "VTr109", Email = "VTr@gmail.com", Password = "VTr9087" };
            var validator = new UserValidator();

            //Act
            var result = validator.TestValidate(user);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.FirstName).WithErrorMessage($"'{nameof(UserDto.FirstName)}' must not be empty.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_WhenLastNameIsNullOrEmpty_ThenThrow(string name)
        {
            //Arrange
            var user = new UserDto { FirstName = "Vli", LastName = name, Login = "VTr109", Email = "VTr@gmail.com", Password = "VTr9087" };
            var validator = new UserValidator();

            //Act
            var result = validator.TestValidate(user);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.LastName).WithErrorMessage($"'{nameof(UserDto.LastName)}' must not be empty.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_WhenEmailIsNullOrEmpty_ThenThrow(string email)
        {
            //Arrange
            var user = new UserDto { FirstName = "Vli", LastName = "Vs", Email = email, Login = "hyt", Password = "VTr9087" };
            var validator = new UserValidator();

            //Act
            var result = validator.TestValidate(user);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage($"'{nameof(UserDto.Email)}' must not be empty.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_WhenLoginIsNullOrEmpty_ThenThrow(string login)
        {
            //Arrange
            var user = new UserDto { FirstName = "Vli", LastName = "Vs", Email = "kjhhf", Login = login, Password = "kjhf123"};
            var validator = new UserValidator();

            //Act & Assert
            var result = validator.TestValidate(user);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Login).WithErrorMessage($"'{nameof(UserDto.Login)}' must not be empty.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_WhenPasswordIsNullOrEmpty_ThenThrow(string password)
        {
            //Arrange
            var user = new UserDto { FirstName = "Vli", LastName = "Vs", Email = "kjhg", Login = "poiu", Password = password };
            var validator = new UserValidator();

            //Act
            var result = validator.TestValidate(user);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage($"'{nameof(UserDto.Password)}' must not be empty.");
        }

        [Fact]
        public void Validate_WhenFirstNameMoreThan50_ThenThrow()
        {
            //Arrange
            var name = new string('1', 51);
            var user = new UserDto { FirstName = name, LastName = "Tremonin", Login = "VTr109", Email = "VTr@gmail.com", Password = "VTr9087" };
            var validator = new UserValidator();

            //Act
            var result = validator.TestValidate(user);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.FirstName).WithErrorMessage($"The length of '{nameof(UserDto.FirstName)}' must be 50 characters or fewer. You entered 51 characters.");
        }

        [Fact]
        public void Validate_WhenLastNameMoreThan50_ThenThrow()
        {
            //Arrange
            var name = new string('1', 51);
            var user = new UserDto { FirstName = "Vli", LastName = name, Login = "VTr109", Email = "VTr@gmail.com", Password = "kjhf123" };
            var validator = new UserValidator();

            //Act
            var result = validator.TestValidate(user);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.LastName).WithErrorMessage($"The length of '{nameof(UserDto.LastName)}' must be 50 characters or fewer. You entered 51 characters.");
        }

        [Fact]
        public void Validate_WhenEmailMoreThan100_ThenThrow()
        {
            //Arrange
            var email = new string('1', 101);
            var user = new UserDto { FirstName = "jhgd", LastName = "OIUY", Email = email, Login = "VTr109", Password = "kjhf123" };
            var validator = new UserValidator();

            //Act
            var result = validator.TestValidate(user);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage($"The length of '{nameof(UserDto.Email)}' must be 100 characters or fewer. You entered 101 characters.");
        }

        [Fact]
        public void Validate_WhenLoginMoreThan100_ThenThrow()
        {
            //Arrange
            var login = new string('1', 101);
            var user = new UserDto { FirstName = "jhgd", LastName = "OIUY", Email = "kjhgf", Login = login, Password = "kjhfs"};
            var validator = new UserValidator();

            //Act
            var result = validator.TestValidate(user);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Login).WithErrorMessage($"The length of '{nameof(UserDto.Login)}' must be 100 characters or fewer. You entered 101 characters.");
        }

        [Fact]
        public void Validate_WhenPasswordMoreThan100_ThenThrow()
        {
            //Arrange
            var password = new string('1', 101);
            var user = new UserDto { FirstName = "jhgd", LastName = "OIUY", Email = "kjhgf", Login = "lkjyr", Password = password };
            var validator = new UserValidator();

            //Act
            var result = validator.TestValidate(user);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage($"The length of '{nameof(UserDto.Password)}' must be 100 characters or fewer. You entered 101 characters.");
        }

        [Fact]
        public void Validate_WhenModelIsRight_ThenPass()
        {
            //Arrange
            var user = new UserDto { FirstName = "Vli", LastName = "Tremonin", Login = "VTr109", Email = "VTr@gmail.com", Password = "VTr9087"};
            var validator = new UserValidator();

            //Act
            var result = validator.TestValidate(user);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
