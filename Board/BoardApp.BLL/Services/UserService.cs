using AutoMapper;
using BoardApp.BLL.Hashing;
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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;
        private readonly IPasswordCrypt _crypt;

        public UserService(IUnitOfWork userRepository, IMapper mapper, IValidationService validationService, IPasswordCrypt crypt)
        {
            _uow = userRepository;
            _mapper = mapper;
            _validationService = validationService;
            _crypt = crypt;
        }

        public UserDto Add(UserDto entity)
        {
            var validationResult = _validationService.Validate<UserValidator, UserDto>(entity);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Message);
            }

            var existingUser = _uow.UserRepository
                .ReadAll()
                .FirstOrDefault(u => u.Email == entity.Email || u.Login == entity.Login);

            if (existingUser is not null)
            {
                if (existingUser.Email == entity.Email)
                {
                    throw new ArgumentException("There's already exists a user with this email");
                }
                if (existingUser.Login == entity.Login)
                {
                    throw new ArgumentException("There's already exists a user with this login");
                }
            }

            _validationService.Validate<UserValidator, UserDto>(entity);
            entity.Password = _crypt.HashPassword(entity.Password);
            var user = _mapper.Map<User>(entity);
            var result = _uow.UserRepository.Add(user);
            _uow.SaveChanges();

            return _mapper.Map<UserDto>(result);
        }

        public void Delete(int id)
        {
            var permission = _uow.UserRepository.Read(id);
            _uow.UserRepository.Delete(permission);
            _uow.SaveChanges();
        }

        public UserDto Authenticate(string loginOrEmail, string password)
        {
            var user = _uow.UserRepository.ReadAll().FirstOrDefault(u => u.Login == loginOrEmail || u.Email == loginOrEmail);
            if (user is null)
                return null;

            return _crypt.Verify(password, user.Password) ? _mapper.Map<UserDto>(user) : null;
        }

        public UserDto Read(int id)
        {
            var result = _uow.UserRepository.Read(id);

            return _mapper.Map<UserDto>(result);
        }

        public IList<UserDto> ReadAll()
        {
            var result = _uow.UserRepository.ReadAll().ToList();

            return _mapper.Map<List<UserDto>>(result);
        }

        public UserDto Update(UserDto entity)
        {
            var validationResult = _validationService.Validate<UserValidator, UserDto>(entity);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Message);
            }

            var current = _uow.UserRepository.Read(entity.Id);
            _mapper.Map(entity, current);
            var result = _uow.UserRepository.Update(current);
            _uow.SaveChanges();

            return _mapper.Map<UserDto>(result);
        }
    }
}
