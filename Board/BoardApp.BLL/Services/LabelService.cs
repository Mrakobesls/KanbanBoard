using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BoardApp.BLL.Validators;
using BoardApp.BLL.Validators.Base;
using BoardApp.Common.Models;
using BoardApp.DAL.Model;
using BoardApp.DAL.UnitOfWork;

namespace BoardApp.BLL.Services
{
    public class LabelService : ILabelService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public LabelService(IUnitOfWork uow, IMapper mapper, IValidationService validationService)
        {
            _uow = uow;
            _mapper = mapper;
            _validationService = validationService;
        }

        public LabelDto Add(LabelDto entity)
        {
            var validationResult = _validationService.Validate<LabelValidator, LabelDto>(entity);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Message);
            }
            var label = _mapper.Map<Label>(entity);
            var result = _uow.LabelRepository.Add(label);
            _uow.SaveChanges();

            return _mapper.Map<LabelDto>(result);
        }

        public void Delete(int id)
        {
            var label = _uow.LabelRepository.Read(id);
            if (label is null)
            {
                throw new ArgumentNullException(nameof(Label), "Label not found.");
            }
            _uow.LabelRepository.Delete(label);
            _uow.SaveChanges();
        }

        public LabelDto Read(int id)
        {
            var result = _uow.PermissionRepository.Read(id);
            if (result is null)
            {
                throw new ArgumentNullException(nameof(Label), "Label not found.");
            }

            return _mapper.Map<LabelDto>(result);
        }

        public IList<LabelDto> ReadAll()
        {
            var result = _uow.LabelRepository.ReadAll().ToList();

            return _mapper.Map<List<LabelDto>>(result);
        }

        public LabelDto Update(LabelDto entity)
        {
            var validationResult = _validationService.Validate<LabelValidator, LabelDto>(entity);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Message);
            }
            var current = _uow.LabelRepository.Read(entity.Id);
            if (current is null)
            {
                throw new ArgumentNullException(nameof(Label), "Label not found.");
            }
            _mapper.Map(entity, current);
            var result = _uow.LabelRepository.Update(current);
            _uow.SaveChanges();

            return _mapper.Map<LabelDto>(result);
        }
    }
}
