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
    public class LabelServiceTests
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly IMapper _mapper;
        private readonly Mock<IValidationService> _validationService;
        private readonly Mock<IGenericRepository<Label>> _repository;

        private readonly ILabelService _labelService;

        public LabelServiceTests()
        {
            _repository = new Mock<IGenericRepository<Label>>();
            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(m => m.LabelRepository).Returns(_repository.Object);

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<ServicesProfile>()).CreateMapper();
            _validationService = new Mock<IValidationService>(MockBehavior.Strict);

            _labelService = new LabelService(_uow.Object, _mapper, _validationService.Object);
        }

        [Fact]
        public void Add()
        {
            //Arrange
            var id = 4;
            var label = new LabelDto { Id = id };
            var validationResult = new ValidationModel { IsValid = true };
            _validationService.Setup(x => x.Validate<LabelValidator, LabelDto>(label)).Returns(validationResult);
            var dalLabel = new Label() { Id = id };
            _repository.Setup(x => x.Add(It.IsAny<Label>()))
                .Callback<Label>(x =>
                {
                    Assert.Equal(id, x.Id);
                }).Returns(dalLabel);

            //Act
            var result = _labelService.Add(label);

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
            var dalLabel = new Label() { Id = id };
            _repository.Setup(x => x.Read(id)).Returns(dalLabel);
            _repository.Setup(x => x.Delete(dalLabel));

            //Act 
            _labelService.Delete(id);

            //Assert
            _uow.VerifyAll();
            _repository.VerifyAll();
            _validationService.VerifyAll();
        }
    }
}
