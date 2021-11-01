using System;
using BoardApp.DAL.Model;
using BoardApp.DAL.Repositories;

namespace BoardApp.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BoardDbContext _context;

        private IGenericRepository<Board> _boardRepository;
        public IGenericRepository<Board> BoardRepository => _boardRepository ??= new GenericRepository<Board>(_context);

        private IGenericRepository<BoardAccess> _boardAccessRepository;
        public IGenericRepository<BoardAccess> BoardAccessRepository => _boardAccessRepository ??= new GenericRepository<BoardAccess>(_context);

        private IGenericRepository<Card> _cardRepository;
        public IGenericRepository<Card> CardRepository => _cardRepository ??= new GenericRepository<Card>(_context);

        private IGenericRepository<Column> _columnRepository;
        public IGenericRepository<Column> ColumnRepository => _columnRepository ??= new GenericRepository<Column>(_context);

        private IGenericRepository<Comment> _commentRepository;
        public IGenericRepository<Comment> CommentRepository => _commentRepository ??= new GenericRepository<Comment>(_context);

        private IGenericRepository<Label> _labelRepository;
        public IGenericRepository<Label> LabelRepository => _labelRepository ??= new GenericRepository<Label>(_context);

        private IGenericRepository<Permission> _permissionRepository;
        public IGenericRepository<Permission> PermissionRepository => _permissionRepository ??= new GenericRepository<Permission>(_context);

        private IGenericRepository<User> _userRepository;
        public IGenericRepository<User> UserRepository => _userRepository ??= new GenericRepository<User>(_context);


        public UnitOfWork(BoardDbContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
