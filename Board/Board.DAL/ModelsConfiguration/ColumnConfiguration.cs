using BoardApp.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardApp.DAL.ModelsConfiguration
{
    public class ColumnConfiguration : IEntityTypeConfiguration<Column>
    {
        public void Configure(EntityTypeBuilder<Column> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Title).IsRequired().HasMaxLength(30);
            builder.HasOne(c => c.Board).WithMany(c => c.Columns).HasForeignKey(c => c.BoardId);
            builder.HasMany(c => c.Cards).WithOne(c => c.Column);
        }
    }
}
