using AutoMapper;
using BoardApp.Common;
using BoardApp.Common.Models;
using BoardApp.DAL.Model;
using BoardApp.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace BoardApp.BLL.Services
{
    public class BoardAccessService : IBoardAccessService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BoardAccessService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public BoardAccessDto Add(BoardAccessDto entity)
        {
            var boardAccess = _mapper.Map<BoardAccess>(entity);
            var result = _uow.BoardAccessRepository.Add(boardAccess);
            _uow.SaveChanges();

            return _mapper.Map<BoardAccessDto>(result);
        }

        public void Delete(int id)
        {
            var boardAccess = _uow.BoardAccessRepository.Read(id);
            _uow.BoardAccessRepository.Delete(boardAccess);
            _uow.SaveChanges();
        }

        public Access GetAccess(int userId, int boardId)
        {
            Access access = 0;

            var accesses = _uow.BoardAccessRepository.ReadAll()
                .Where(ba => ba.UserId == userId && ba.BoardId == boardId).ToList();

            foreach(var a in accesses)
            {
                access |= (Access)a.PermissionId;
            }

            return access;
        }

        public BoardAccessDto Read(int id)
        {
            var result = _uow.BoardAccessRepository.Read(id);

            return _mapper.Map<BoardAccessDto>(result);
        }

        public IList<BoardAccessDto> ReadAll()
        {
            var result = _uow.BoardAccessRepository.ReadAll().ToList();

            return _mapper.Map<List<BoardAccessDto>>(result);
        }

        public BoardAccessDto Update(BoardAccessDto entity)
        {
            var current = _uow.BoardAccessRepository.Read(entity.Id);
            _mapper.Map(entity, current);
            var result = _uow.BoardAccessRepository.Update(current);
            _uow.SaveChanges();

            return _mapper.Map<BoardAccessDto>(result);
        }
        
        public IList<BoardAccessDto> GetNoneReadBoardAccessesByUserIdAndBoardId(int boardId, int userId)
        {
            //not read permission
            var result =  _uow.BoardAccessRepository.ReadAll()
                .Where(x => x.BoardId == boardId && x.UserId == userId && x.PermissionId != (int)Access.Read).ToList();
            return _mapper.Map<List<BoardAccessDto>>(result);
        }

        public bool IsUpdatePermission(int boardId, int userId)
        {
            var a = _uow.BoardAccessRepository.ReadAll()
                .Where(x => x.BoardId == boardId && x.UserId == userId && x.PermissionId != (int)Access.Read).ToList();
            return a.Count != 0;
        }
        public bool IsReadPermission(int boardId, int userId)
        {
            var a = _uow.BoardAccessRepository.ReadAll()
                .Where(x => x.BoardId == boardId && x.UserId == userId).ToList();
            return a.Count != 0;
        }
    }
}
