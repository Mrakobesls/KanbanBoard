using AutoMapper;
using BoardApp.BLL.Services;
using BoardApp.Common.Models;
using BoardApp.BLL.Mappings;
using Moq;
using Xunit;
using BoardApp.DAL.Model;
using BoardApp.DAL.Repositories;
using BoardApp.DAL.UnitOfWork;

namespace BoardApp.BLL.Tests.Services
{
    public class BoardAccessServiceTests
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly IMapper _mapper;
        private readonly IBoardAccessService _boardAccessService;
        private readonly Mock<IGenericRepository<BoardAccess>> _repository;

        public BoardAccessServiceTests()
        {
            _repository = new Mock<IGenericRepository<BoardAccess>>();
            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(m => m.BoardAccessRepository).Returns(_repository.Object);

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<ServicesProfile>()).CreateMapper();
            _boardAccessService = new BoardAccessService(_uow.Object, _mapper);
        }

        [Fact]
        public void Add()
        {
            //Arrange
            var id = 4;
            var userId = 5;
            var permissionId = 6;
            var boardId = 7;
            var boardAccess = new BoardAccessDto()
            {
                Id = id,
                BoardId = boardId,
                PermissionId = permissionId,
                UserId = userId
            };
            var dalBoardAccess = new BoardAccess() { Id = id };
            _repository.Setup(x => x.Add(It.IsAny<BoardAccess>()))
                .Callback<BoardAccess>(x =>
                {
                    Assert.Equal(id, x.Id);
                    Assert.Equal(userId, x.UserId);
                    Assert.Equal(permissionId, x.PermissionId);
                    Assert.Equal(boardId, x.BoardId);
                }).Returns(dalBoardAccess);

            //Act
            var result = _boardAccessService.Add(boardAccess);

            //Assert
            Assert.Equal(id, result.Id);
            _uow.VerifyAll();
            _repository.VerifyAll();
        }

        [Fact]
        public void Delete()
        {
            //Arrange
            var id = 7;
            var dalBoardAccess = new BoardAccess() { Id = id };
            _repository.Setup(x => x.Read(id)).Returns(dalBoardAccess);
            _repository.Setup(x => x.Delete(dalBoardAccess));

            //Act 
            _boardAccessService.Delete(id);

            //Assert
            _uow.VerifyAll();
            _repository.VerifyAll();
        }

        [Fact]
        public void Update()
        {
            //Arrange
            var id = 10;
            var boardAccess = new BoardAccessDto() {Id = id};
            var dalBoardAccess = new BoardAccess() {Id = id};
            _repository.Setup(x => x.Read(id)).Returns(dalBoardAccess);
            _repository.Setup(x => x.Update(It.IsAny<BoardAccess>()))
                .Callback<BoardAccess>(x =>
                {
                    Assert.Equal(id, x.Id);
                }).Returns(dalBoardAccess);

            //Act
            var result = _boardAccessService.Update(boardAccess);

            //Assert
            Assert.Equal(id, result.Id);
            _uow.VerifyAll();
            _repository.VerifyAll();
        }
    }
}
