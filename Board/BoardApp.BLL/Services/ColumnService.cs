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
    public class ColumnService : IColumnService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public ColumnService(IUnitOfWork uow, IMapper mapper, IValidationService validationService)
        {
            _uow = uow;
            _mapper = mapper;
            _validationService = validationService;
        }

        public ColumnDto Add(ColumnDto entity)
        {
            var validationResult = _validationService.Validate<ColumnValidator, ColumnDto>(entity);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Message);
            }

            if (_uow.BoardRepository.Read(entity.BoardId) is null)
            {
                throw new ArgumentException("Incorrect boardId");
            }

            var column = _mapper.Map<Column>(entity);
            var result = _uow.ColumnRepository.Add(column);
            
            _uow.SaveChanges();

            return _mapper.Map<ColumnDto>(result);
        }

        public void Delete(int id)
        {
            var column = _uow.ColumnRepository.Read(id);
            if (column is null)
            {
                throw new ArgumentException("Incorrect columnId");
            }
            _uow.ColumnRepository.Delete(column);
            _uow.SaveChanges();
        }

        public ColumnDto Read(int id)
        {
            var result = _uow.ColumnRepository.Read(id);

            return _mapper.Map<ColumnDto>(result);
        }

        public IList<ColumnDto> ReadAll()
        {
            var result = _uow.ColumnRepository.ReadAll().ToList();

            return _mapper.Map<List<ColumnDto>>(result);
        }

        public ColumnDto Update(ColumnDto entity)
        {
            var validationResult = _validationService.Validate<ColumnValidator, ColumnDto>(entity);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Message);
            }
            if (_uow.ColumnRepository.Read(entity.Id) is null)
            {
                throw new ArgumentException("Incorrect columnId");
            }
            var current = _uow.ColumnRepository.Read(entity.Id);
            _mapper.Map(entity, current);
            var result = _uow.ColumnRepository.Update(current);
            _uow.SaveChanges();

            return _mapper.Map<ColumnDto>(result);
        }
    }
}
