using BoardApp.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardApp.DAL.ModelsConfiguration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Text).IsRequired().HasMaxLength(300);
            builder.Property(c => c.DateTime).IsRequired();
            builder.HasOne(c => c.User).WithMany(c => c.Comments).HasForeignKey(c => c.UserId);
            builder.HasOne(c => c.Card).WithMany(c => c.Comments).HasForeignKey(c => c.CardId);
        }
    }
}
