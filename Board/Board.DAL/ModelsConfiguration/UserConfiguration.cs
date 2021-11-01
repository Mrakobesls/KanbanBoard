using BoardApp.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardApp.DAL.ModelsConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasIndex(c => new { c.FirstName, c.LastName });
            builder.Property(c => c.Email).IsRequired().HasMaxLength(100);
            builder.HasAlternateKey(c => c.Email);
            builder.Property(c => c.Login).IsRequired().HasMaxLength(100);
            builder.HasAlternateKey(c => c.Login);
            builder.Property(c => c.Password).IsRequired().HasMaxLength(100);
            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.LastName).IsRequired().HasMaxLength(50);
            builder.HasMany(c => c.BoardAccesses).WithOne(c => c.User);
            builder.HasMany(c => c.Cards).WithMany(c => c.Users);
            builder.HasMany(c => c.Comments).WithOne(c => c.User);
        }
    }
}
