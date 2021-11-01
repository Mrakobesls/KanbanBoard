using AutoMapper;
using BoardApp.BLL.Validators;
using BoardApp.BLL.Validators.Base;
using BoardApp.Common.Models;
using BoardApp.DAL.Model;
using BoardApp.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardApp.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public CommentService(IUnitOfWork uow, IMapper mapper, IValidationService validationService)
        {
            _uow = uow;
            _mapper = mapper;
            _validationService = validationService;
        }

        public CommentDto Add(CommentDto entity)
        {
            var validationResult = _validationService.Validate<CommentValidator, CommentDto>(entity);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Message);

            var comment = _mapper.Map<Comment>(entity);
            var result = _uow.CommentRepository.Add(comment);
            _uow.SaveChanges();

            return _mapper.Map<CommentDto>(result);
        }

        public void Delete(int id)
        {
            var comment = _uow.CommentRepository.Read(id);
            _uow.CommentRepository.Delete(comment);
            _uow.SaveChanges();
        }

        public CommentDto Read(int id)
        {
            var result = _uow.CommentRepository.Read(id);

            return _mapper.Map<CommentDto>(result);
        }

        public IList<CommentDto> ReadAll()
        {
            var result = _uow.CommentRepository.ReadAll().ToList();

            return _mapper.Map<List<CommentDto>>(result);
        }

        public CommentDto Update(CommentDto entity)
        {
            var validationResult = _validationService.Validate<CommentValidator, CommentDto>(entity); if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Message);

            var current = _uow.CommentRepository.Read(entity.Id);
            _mapper.Map(entity, current);
            var result = _uow.CommentRepository.Update(current);
            _uow.SaveChanges();

            return _mapper.Map<CommentDto>(result);
        }
    }
}
