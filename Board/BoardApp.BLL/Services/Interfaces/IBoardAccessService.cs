using System.Collections.Generic;
using BoardApp.Common;
using BoardApp.Common.Models;

namespace BoardApp.BLL.Services
{
    public interface IBoardAccessService : IGenericService<BoardAccessDto>
    {
        IList<BoardAccessDto> GetNoneReadBoardAccessesByUserIdAndBoardId(int boardId, int userId);
        public Access GetAccess(int userId, int boardId);
        bool IsUpdatePermission(int boardId, int userId);
        bool IsReadPermission(int boardId, int userId);
    }
}
