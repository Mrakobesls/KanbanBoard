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
    public class ColumnServiceTests
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly IMapper _mapper;
        private readonly Mock<IValidationService> _validationService;
        private readonly Mock<IGenericRepository<Column>> _columnRepository;
        private readonly Mock<IGenericRepository<Board>> _boardRepository;

        private readonly IColumnService _columnService;

        public ColumnServiceTests()
        {
            _columnRepository = new Mock<IGenericRepository<Column>>();
            _boardRepository = new Mock<IGenericRepository<Board>>();
            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(m => m.ColumnRepository).Returns(_columnRepository.Object);

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<ServicesProfile>()).CreateMapper();
            _validationService = new Mock<IValidationService>();

            _columnService = new ColumnService(_uow.Object, _mapper, _validationService.Object);
        }

        [Fact]
        public void Add()
        {
            //Arrange
            var id = 4;
            var boardId = 6;
            var column = new ColumnDto
            {
                Id = id, 
                BoardId = boardId
            };
            _uow.Setup(m => m.BoardRepository).Returns(_boardRepository.Object);
            var validationResult = new ValidationModel { IsValid = true };
            _validationService.Setup(x => x.Validate<ColumnValidator, ColumnDto>(column)).Returns(validationResult);
            var dalColumn = new Column { Id = id };
            _columnRepository.Setup(x => x.Add(It.IsAny<Column>()))
                .Callback<Column>(x =>
                {
                    Assert.Equal(id, x.Id);
                    Assert.Equal(boardId, x.BoardId);
                }).Returns(dalColumn);
            _boardRepository.Setup(x => x.Read(It.IsAny<int>())).Returns(new Board() { Id = boardId});

            //Act
            var result = _columnService.Add(column);

            //Assert
            Assert.Equal(id, result.Id);
            _uow.VerifyAll();
            _columnRepository.VerifyAll();
            _boardRepository.VerifyAll();
            _validationService.VerifyAll();
        }

        [Fact]
        public void Delete()
        {
            //Arrange
            var id = 7;
            var dalColumn = new Column { Id = id };
            _columnRepository.Setup(x => x.Read(id)).Returns(dalColumn);
            _columnRepository.Setup(x => x.Delete(dalColumn));
            //Act 
            _columnService.Delete(id);

            //Assert
            _uow.VerifyAll();
            _columnRepository.VerifyAll();
            _boardRepository.VerifyAll();
            _validationService.VerifyAll();
        }

        [Fact]
        public void Update()
        {
            //Arrange
            var id = 10;
            var boardId = 6;
            var column = new ColumnDto { Id = id, BoardId = boardId };
            var validationResult = new ValidationModel { IsValid = true };
            _validationService.Setup(x => x.Validate<ColumnValidator, ColumnDto>(column)).Returns(validationResult);
            var dalColumn = new Column { Id = id, BoardId = boardId };
            _columnRepository.Setup(x => x.Read(id)).Returns(dalColumn);
            _columnRepository.Setup(x => x.Update(It.IsAny<Column>()))
                .Callback<Column>(x =>
                {
                    Assert.Equal(id, x.Id);
                }).Returns(dalColumn);

            //Act
            var result = _columnService.Update(column);

            //Assert
            Assert.Equal(id, result.Id);
            _uow.VerifyAll();
            _columnRepository.VerifyAll();
            _validationService.VerifyAll();
        }
    }
}
