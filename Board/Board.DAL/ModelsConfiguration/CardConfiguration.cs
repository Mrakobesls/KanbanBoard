using BoardApp.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardApp.DAL.ModelsConfiguration
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Description).HasMaxLength(200);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(30);
            builder.HasOne(c => c.Column).WithMany(c => c.Cards).HasForeignKey(c => c.ColumnId);
            builder.HasMany(c => c.Labels).WithMany(c => c.Cards);
            builder.HasMany(c => c.Users).WithMany(c => c.Cards);
            builder.HasMany(c => c.Comments).WithOne(c => c.Card);
        }
    }
}
