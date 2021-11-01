using BoardApp.Common.Models;
using System.Collections.Generic;

namespace BoardApp.BLL.Services
{
    public interface ICardService : IGenericService<CardDto>
    {
        void AddLabel(int cardId, int labelId);
        void DeleteLabel(int cardId, int labelId);
        public CardDto AddMember(int cardId, int userId);
        public IList<UserDto> GetAllMembers(CardDto entity);
        public CardDto UpdateMembers(CardDto entity, IEnumerable<int> membersIds);
    }
}
