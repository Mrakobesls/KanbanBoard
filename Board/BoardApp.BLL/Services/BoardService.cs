using System;
using BoardApp.Common.Models;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BoardApp.BLL.Validators;
using BoardApp.BLL.Validators.Base;
using BoardApp.DAL.Model;
using BoardApp.DAL.UnitOfWork;

namespace BoardApp.BLL.Services
{
    public class BoardService : IBoardService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public BoardService(IUnitOfWork uow, IMapper mapper, IValidationService validationService)
        {
            _uow = uow;
            _mapper = mapper;
            _validationService = validationService;
        }

        public BoardDto Add(BoardDto entity)
        {
            var validationResult = _validationService.Validate<BoardValidator, BoardDto>(entity);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Message);
            }
            var board = _mapper.Map<Board>(entity);
            var result = _uow.BoardRepository.Add(board);
            _uow.SaveChanges();

            return _mapper.Map<BoardDto>(result);
        }

        public void Delete(int id)
        {
            var board = _uow.BoardRepository.Read(id);
            _uow.BoardRepository.Delete(board);
            _uow.SaveChanges();
        }

        public BoardDto Read(int id)
        {
            var result = _uow.BoardRepository.Read(id);

            return _mapper.Map<BoardDto>(result);
        }

        public IList<BoardDto> ReadAll()
        {
            var result = _uow.BoardRepository.ReadAll().ToList();

            return _mapper.Map<List<BoardDto>>(result);
        }

        public BoardDto Update(BoardDto entity)
        {
            var validationResult = _validationService.Validate<BoardValidator, BoardDto>(entity);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Message);
            }
            var current = _uow.BoardRepository.Read(entity.Id);
            _mapper.Map(entity, current);
            var result = _uow.BoardRepository.Update(current);
            _uow.SaveChanges();

            return _mapper.Map<BoardDto>(result);
        }
    }
}
