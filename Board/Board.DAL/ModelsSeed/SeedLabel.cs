using BoardApp.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardApp.DAL.ModelsSeed
{
    public class SeedLabel : IEntityTypeConfiguration<Label>
    {
        public void Configure(EntityTypeBuilder<Label> builder)
        {
            builder.HasData(
                new Label { Id = 1, Name = "Important" },
                new Label { Id = 2, Name = "Dead Line" });
        }
    }
}
