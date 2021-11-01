using BoardApp.Common.Models;
using AutoMapper;
using BoardApp.BLL.Mappings;
using BoardApp.BLL.Services;
using BoardApp.BLL.Validators;
using BoardApp.BLL.Validators.Base;
using Moq;
using Xunit;
using BoardApp.DAL.Model;
using BoardApp.DAL.Repositories;
using BoardApp.DAL.UnitOfWork;

namespace BoardApp.BLL.Tests.Services
{
    public class CardServiceTests
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly IMapper _mapper;
        private readonly Mock<IValidationService> _validationService;
        private readonly Mock<IGenericRepository<Card>> _repository;

        private readonly ICardService _cardService;

        public CardServiceTests()
        {
            _repository = new Mock<IGenericRepository<Card>>();
            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(m => m.CardRepository).Returns(_repository.Object);

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<ServicesProfile>()).CreateMapper();
            _validationService = new Mock<IValidationService>(MockBehavior.Strict);

            _cardService = new CardService(_uow.Object, _mapper, _validationService.Object);
        }

        [Fact]
        public void Add()
        {
            //Arrange
            var id = 4;
            var columnId = 6;
            var card = new CardDto
            {
                Id = id,
                ColumnId = columnId
            };
            var validationResult = new ValidationModel { IsValid = true };
            _validationService.Setup(x => x.Validate<CardValidator, CardDto>(card)).Returns(validationResult);
            var dalCard = new Card { Id = id };
            _repository.Setup(x => x.Add(It.IsAny<Card>()))
                .Callback<Card>(x =>
                {
                    Assert.Equal(id, x.Id);
                    Assert.Equal(columnId, x.ColumnId);
                }).Returns(dalCard);

            //Act
            var result = _cardService.Add(card);

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
            var dalCard = new Card() { Id = id };
            _repository.Setup(x => x.Read(id)).Returns(dalCard);
            _repository.Setup(x => x.Delete(dalCard));

            //Act 
            _cardService.Delete(id);

            //Assert
            _uow.VerifyAll();
            _repository.VerifyAll();
            _validationService.VerifyAll();
        }

        [Fact]
        public void Update()
        {
            //Arrange
            var id = 10;
            var card = new CardDto { Id = id };
            var validationResult = new ValidationModel { IsValid = true };
            _validationService.Setup(x => x.Validate<CardValidator, CardDto>(card)).Returns(validationResult);
            var dalCard = new Card() { Id = id };
            _repository.Setup(x => x.Read(id)).Returns(dalCard);
            _repository.Setup(x => x.Update(It.IsAny<Card>()))
                .Callback<Card>(x =>
                {
                    Assert.Equal(id, x.Id);
                }).Returns(dalCard);

            //Act
            var result = _cardService.Update(card);

            //Assert
            Assert.Equal(id, result.Id);
            _uow.VerifyAll();
            _repository.VerifyAll();
            _validationService.VerifyAll();
        }
    }
}
