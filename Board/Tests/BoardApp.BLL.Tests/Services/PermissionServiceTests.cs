using AutoMapper;
using BoardApp.BLL.Mappings;
using BoardApp.BLL.Services;
using BoardApp.BLL.Validators;
using BoardApp.BLL.Validators.Base;
using BoardApp.Common.Models;
using Moq;
using Xunit;
using BoardApp.DAL.Model;
using BoardApp.DAL.Repositories;
using BoardApp.DAL.UnitOfWork;

namespace BoardApp.BLL.Tests.Services
{
    public class PermissionServiceTests
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly IMapper _mapper;
        private readonly Mock<IValidationService> _validationService;
        private readonly Mock<IGenericRepository<Permission>> _repository;

        private readonly IPermissionService _permissionService;

        public PermissionServiceTests()
        {
            _repository = new Mock<IGenericRepository<Permission>>();
            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(m => m.PermissionRepository).Returns(_repository.Object);

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<ServicesProfile>()).CreateMapper();
            _validationService = new Mock<IValidationService>(MockBehavior.Strict);

            _permissionService = new PermissionService(_uow.Object, _mapper, _validationService.Object);
        }

        [Fact]
        public void Add()
        {
            //Arrange
            var id = 4;
            var permission = new PermissionDto { Id = id };
            var validationResult = new ValidationModel { IsValid = true };
            _validationService.Setup(x => x.Validate<PermissionValidator, PermissionDto>(permission)).Returns(validationResult);
            var dalPermission = new Permission() { Id = id };
            _repository.Setup(x => x.Add(It.IsAny<Permission>()))
                .Callback<Permission>(x =>
                {
                    Assert.Equal(id, x.Id);
                }).Returns(dalPermission);

            //Act
            var result = _permissionService.Add(permission);

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
            var dalPermission = new Permission() { Id = id };
            _repository.Setup(x => x.Read(id)).Returns(dalPermission);
            _repository.Setup(x => x.Delete(dalPermission));

            //Act 
            _permissionService.Delete(id);

            //Assert
            _uow.VerifyAll();
            _repository.VerifyAll();
            _validationService.VerifyAll();
        }
    }
}
