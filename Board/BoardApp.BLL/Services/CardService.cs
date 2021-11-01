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
    public class CardService : ICardService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public CardService(IUnitOfWork uow, IMapper mapper, IValidationService validationService)
        {
            _uow = uow;
            _mapper = mapper;
            _validationService = validationService;
        }

        public CardDto Add(CardDto entity)
        {
            var validationResult = _validationService.Validate<CardValidator, CardDto>(entity);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Message);
            }
            var card = _mapper.Map<Card>(entity);
            var result = _uow.CardRepository.Add(card);
            _uow.SaveChanges();

            return _mapper.Map<CardDto>(result);
        }

        public CardDto AddMember(int cardId, int userId)
        {
            var user = _uow.UserRepository.Read(userId);
            var card = _uow.CardRepository.Read(cardId);

            card.Users.Add(user);

            var cardDto = _mapper.Map<CardDto>(card);
            var validationResult = _validationService.Validate<CardValidator, CardDto>(cardDto);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Message);
            }

            _uow.SaveChanges();

            return cardDto;
        }

        public void Delete(int id)
        {
            var card = _uow.CardRepository.Read(id);
            if (card is null)
            {
                throw new ArgumentNullException(nameof(Card), "Card not found.");
            }
            _uow.CardRepository.Delete(card);
            _uow.SaveChanges();
        }

        public IList<UserDto> GetAllMembers(CardDto entity)
        {
            return entity.Users.ToList();
        }

        public CardDto Read(int id)
        {
            var result = _uow.CardRepository.Read(id);
            if (result is null)
            {
                throw new ArgumentNullException(nameof(Card), "Card not found.");
            }

            return _mapper.Map<CardDto>(result);
        }

        public IList<CardDto> ReadAll()
        {
            var result = _uow.CardRepository.ReadAll().ToList();

            return _mapper.Map<List<CardDto>>(result);
        }

        public CardDto Update(CardDto entity)
        {
            var validationResult = _validationService.Validate<CardValidator, CardDto>(entity);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Message);
            }
            var current = _uow.CardRepository.Read(entity.Id);
            if (current is null)
            {
                throw new ArgumentNullException(nameof(Card), "Card not found.");
            }
            _mapper.Map(entity, current);
            var result = _uow.CardRepository.Update(current);
            _uow.SaveChanges();

            return _mapper.Map<CardDto>(result);
        }

        public void AddLabel(int cardId, int labelId)
        {
            var card = _uow.CardRepository.Read(cardId);
            if (card is null)
            {
                throw new ArgumentNullException(nameof(Card), "Card not found.");
            }

            if (card.Labels.Any(x => x.Id == labelId))
            {
                throw new ArgumentException("This label was already added to card");
            }

            var label = _uow.LabelRepository.Read(labelId);
            if (label is null)
            {
                throw new ArgumentNullException(nameof(Label), "Label not found.");
            }
            
            card.Labels.Add(label);
            _uow.SaveChanges();
        }

        public void DeleteLabel(int cardId, int labelId)
        {
            var card = _uow.CardRepository.Read(cardId);
            if (card is null)
            {
                throw new ArgumentNullException(nameof(Card), "Card not found.");
            }

            if (card.Labels.All(x => x.Id != labelId))
            {
                throw new ArgumentException("This label was not yet added to card");
            }

            var label = _uow.LabelRepository.Read(labelId);
            if (label is null)
            {
                throw new ArgumentNullException(nameof(Label), "Label not found.");
            }

            card.Labels.Remove(label);
            _uow.SaveChanges();
        }

        public CardDto UpdateMembers(CardDto entity, IEnumerable<int> membersIds)
        {
            var card = _uow.CardRepository.Read(entity.Id);
            var users = new List<User>();

            foreach (var id in membersIds)
            {
                var user = _uow.UserRepository.Read(id);
                users.Add(user);
            }

            card.Users = users;

            _uow.CardRepository.Update(card);
            _uow.SaveChanges();

            return _mapper.Map<CardDto>(card);
        }
    }
}
