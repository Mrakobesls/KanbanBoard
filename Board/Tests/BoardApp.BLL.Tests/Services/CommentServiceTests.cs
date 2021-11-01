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
    public class CommentServiceTests
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly IMapper _mapper;
        private readonly Mock<IValidationService> _validationService;
        private readonly Mock<IGenericRepository<Comment>> _repository;

        private readonly ICommentService _commentService;

        public CommentServiceTests()
        {
            _repository = new Mock<IGenericRepository<Comment>>();
            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(m => m.CommentRepository).Returns(_repository.Object);

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<ServicesProfile>()).CreateMapper();
            _validationService = new Mock<IValidationService>(MockBehavior.Strict);

            _commentService = new CommentService(_uow.Object, _mapper, _validationService.Object);
        }

        [Fact]
        public void Add()
        {
            //Arrange
            var id = 4;
            var cardId = 6;
            var userId = 7;
            var comment = new CommentDto
            {
                Id = id,
                CardId = cardId,
                UserId = userId
            };
            var validationResult = new ValidationModel { IsValid = true };
            _validationService.Setup(x => x.Validate<CommentValidator, CommentDto>(comment)).Returns(validationResult);
            var dalComment = new Comment { Id = id };
            _repository.Setup(x => x.Add(It.IsAny<Comment>()))
                .Callback<Comment>(x =>
                {
                    Assert.Equal(id, x.Id);
                    Assert.Equal(userId, x.UserId);
                    Assert.Equal(cardId, x.CardId);
                }).Returns(dalComment);

            //Act
            var result = _commentService.Add(comment);

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
            var dalComment = new Comment { Id = id };
            _repository.Setup(x => x.Read(id)).Returns(dalComment);
            _repository.Setup(x => x.Delete(dalComment));

            //Act 
            _commentService.Delete(id);

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
            var comment = new CommentDto { Id = id };
            var validationResult = new ValidationModel { IsValid = true };
            _validationService.Setup(x => x.Validate<CommentValidator, CommentDto>(comment)).Returns(validationResult);
            var dalComment = new Comment { Id = id };
            _repository.Setup(x => x.Read(id)).Returns(dalComment);
            _repository.Setup(x => x.Update(It.IsAny<Comment>()))
                .Callback<Comment>(x =>
                {
                    Assert.Equal(id, x.Id);
                }).Returns(dalComment);

            //Act
            var result = _commentService.Update(comment);

            //Assert
            Assert.Equal(id, result.Id);
            _uow.VerifyAll();
            _repository.VerifyAll();
            _validationService.VerifyAll();
        }
    }
}
