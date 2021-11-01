using BoardApp.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardApp.DAL.ModelsConfiguration
{
    public class BoardAccessConfiguration : IEntityTypeConfiguration<BoardAccess>
    {
        public void Configure(EntityTypeBuilder<BoardAccess> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasOne(c => c.User).WithMany(c => c.BoardAccesses);
            builder.HasOne(c => c.Board).WithMany(c => c.BoardAccesses);
            builder.HasOne(c => c.Permission).WithMany(c => c.BoardAccesses);
        }
    }
}
