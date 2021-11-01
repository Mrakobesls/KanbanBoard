using BoardApp.DAL.ModelsConfiguration;
using BoardApp.DAL.Model;
using BoardApp.DAL.ModelsSeed;
using Microsoft.EntityFrameworkCore;

namespace BoardApp.DAL
{
    public class BoardDbContext : DbContext
    {
        public BoardDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }
        
        public DbSet<Board> Boards { get; set; }
        public DbSet<BoardAccess> BoardAccesses { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BoardConfiguration());
            modelBuilder.ApplyConfiguration(new CardConfiguration());
            modelBuilder.ApplyConfiguration(new ColumnConfiguration());
            modelBuilder.ApplyConfiguration(new BoardAccessConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new LabelConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new SeedBoard());
            modelBuilder.ApplyConfiguration(new SeedCard());
            modelBuilder.ApplyConfiguration(new SeedColumn());
            modelBuilder.ApplyConfiguration(new SeedBoardAccess());
            modelBuilder.ApplyConfiguration(new SeedComment());
            modelBuilder.ApplyConfiguration(new SeedLabel());
            modelBuilder.ApplyConfiguration(new SeedPermission());
            modelBuilder.ApplyConfiguration(new SeedUser());
        }
    }
}
