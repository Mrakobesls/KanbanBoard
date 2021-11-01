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
    public class BoardServiceTests
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly IMapper _mapper;
        private readonly Mock<IValidationService> _validationService;
        private readonly Mock<IGenericRepository<Board>> _repository;

        private readonly IBoardService _boardService;

        public BoardServiceTests()
        {
            _repository = new Mock<IGenericRepository<Board>>();
            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(m => m.BoardRepository).Returns(_repository.Object);

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<ServicesProfile>()).CreateMapper();
            _validationService = new Mock<IValidationService>(MockBehavior.Strict);

            _boardService = new BoardService(_uow.Object, _mapper, _validationService.Object);
        }

        [Fact]
        public void Add()
        {
            //Arrange
            var id = 4;
            var board = new BoardDto { Id = id };
            var validationResult = new ValidationModel { IsValid = true };
            _validationService.Setup(x => x.Validate<BoardValidator, BoardDto>(board)).Returns(validationResult);
            var dalBoard = new Board() { Id = id };
            _repository.Setup(x => x.Add(It.IsAny<Board>()))
                .Callback<Board>(x =>
                {
                    Assert.Equal(id, x.Id);
                }).Returns(dalBoard);

            //Act
            var result = _boardService.Add(board);

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
            var dalBoard = new Board() { Id = id };
            _repository.Setup(x => x.Read(id)).Returns(dalBoard);
            _repository.Setup(x => x.Delete(dalBoard));

            //Act 
            _boardService.Delete(id);

            //Assert
            _uow.VerifyAll();
            _repository.VerifyAll();
            _validationService.VerifyAll();
        }
    }
}
