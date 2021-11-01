using BoardApp.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardApp.DAL.ModelsSeed
{
    public class SeedBoardAccess : IEntityTypeConfiguration<BoardAccess>
    {
        public void Configure(EntityTypeBuilder<BoardAccess> builder)
        {
            builder.HasData(
                new BoardAccess { Id = 1, BoardId = 1, UserId = 1, PermissionId = 1 },
                new BoardAccess { Id = 2, BoardId = 2, UserId = 2, PermissionId = 2 });
        }
    }
}
