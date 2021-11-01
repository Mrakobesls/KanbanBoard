using AutoMapper;
using BoardApp.BLL.Hashing;
using BoardApp.BLL.Mappings;
using BoardApp.BLL.Services;
using BoardApp.BLL.Validators;
using BoardApp.BLL.Validators.Base;
using BoardApp.Common.Models;
using BoardApp.DAL.Model;
using BoardApp.DAL.Repositories;
using BoardApp.DAL.UnitOfWork;
using Moq;
using Xunit;

namespace BoardApp.BLL.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly IMapper _mapper;
        private readonly Mock<IValidationService> _validationService;
        private readonly IPasswordCrypt _crypt;
        private readonly Mock<IGenericRepository<User>> _repository;

        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _repository = new Mock<IGenericRepository<User>>();
            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(m => m.UserRepository).Returns(_repository.Object);

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<ServicesProfile>()).CreateMapper();
            _validationService = new Mock<IValidationService>(MockBehavior.Strict);
            _crypt = new PasswordCrypt();

            _userService = new UserService(_uow.Object, _mapper, _validationService.Object, _crypt);
        }

        [Fact]
        public void Add()
        {
            //Arrange
            var id = 4;
            var user = new UserDto { Id = id, Password = "123456" };
            var validationResult = new ValidationModel { IsValid = true };
            _validationService.Setup(x => x.Validate<UserValidator, UserDto>(user)).Returns(validationResult);

            var dalUser = new User() { Id = id };
            _repository.Setup(x => x.Add(It.IsAny<User>()))
                .Callback<User>(x =>
                {
                    Assert.Equal(id, x.Id);
                }).Returns(dalUser);

            //Act
            var result = _userService.Add(user);

            //Assert
            Assert.Equal(id, result.Id);
            _uow.VerifyAll();
            _repository.VerifyAll();
            _validationService.VerifyAll();
        }

        [Fact]
        public void Delete()
        {
            //Arrange
            var id = 7;
            var dalUser = new User() { Id = id };
            _repository.Setup(x => x.Read(id)).Returns(dalUser);
            _repository.Setup(x => x.Delete(dalUser));

            //Act 
            _userService.Delete(id);

            //Assert
            _uow.VerifyAll();
            _repository.VerifyAll();
            _validationService.VerifyAll();
        }
    }
}
