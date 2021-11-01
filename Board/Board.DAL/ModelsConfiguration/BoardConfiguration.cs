using BoardApp.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardApp.DAL.ModelsConfiguration
{
    public class BoardConfiguration : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Description).HasMaxLength(200);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(30);
            builder.HasMany(c => c.Columns).WithOne(c => c.Board);
            builder.HasMany(c => c.BoardAccesses).WithOne(c => c.Board);
        }
    }
}
