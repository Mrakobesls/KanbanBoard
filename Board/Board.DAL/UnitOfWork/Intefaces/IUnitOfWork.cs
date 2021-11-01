using BoardApp.DAL.Model;
using BoardApp.DAL.Repositories;

namespace BoardApp.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IGenericRepository<Board> BoardRepository { get; }
        public IGenericRepository<BoardAccess> BoardAccessRepository { get; }
        public IGenericRepository<Card> CardRepository { get; }
        public IGenericRepository<Column> ColumnRepository { get; }
        public IGenericRepository<Comment> CommentRepository { get; }
        public IGenericRepository<Label> LabelRepository { get; }
        public IGenericRepository<Permission> PermissionRepository { get; }
        public IGenericRepository<User> UserRepository { get; }

        void SaveChanges();
    }
}
