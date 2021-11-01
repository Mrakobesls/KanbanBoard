using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BoardApp.BLL.Validators;
using BoardApp.BLL.Validators.Base;
using BoardApp.Common.Models;
using BoardApp.DAL.Repositories;
using BoardApp.DAL.Model;
using BoardApp.DAL.UnitOfWork;

namespace BoardApp.BLL.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public PermissionService(IUnitOfWork uow, IMapper mapper, IValidationService validationService)
        {
            _uow = uow;
            _mapper = mapper;
            _validationService = validationService;
        }

        public PermissionDto Add(PermissionDto entity)
        {
            var validationResult = _validationService.Validate<PermissionValidator, PermissionDto>(entity);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Message);
            }
            var permission = _mapper.Map<Permission>(entity);
            var result = _uow.PermissionRepository.Add(permission);
            _uow.SaveChanges();

            return _mapper.Map<PermissionDto>(result);
        }

        public void Delete(int id)
        {
            var permission = _uow.PermissionRepository.Read(id);
            _uow.PermissionRepository.Delete(permission);
            _uow.SaveChanges();
        }

        public PermissionDto Read(int id)
        {
            var result = _uow.PermissionRepository.Read(id);

            return _mapper.Map<PermissionDto>(result);
        }

        public IList<PermissionDto> ReadAll()
        {
            var result = _uow.PermissionRepository.ReadAll().ToList();

            return _mapper.Map<List<PermissionDto>>(result);
        }

        public PermissionDto Update(PermissionDto entity)
        {
            var validationResult = _validationService.Validate<PermissionValidator, PermissionDto>(entity);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Message);
            }
            var current = _uow.PermissionRepository.Read(entity.Id);
            _mapper.Map(entity, current);
            var result = _uow.PermissionRepository.Update(current);
            _uow.SaveChanges();

            return _mapper.Map<PermissionDto>(result);
        }
    }
}
