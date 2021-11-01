using BoardApp.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardApp.DAL.ModelsSeed
{
    public class SeedCard : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasData(
                new Card { Id = 1, Title = "Made MVC", ColumnId = 1 },
                new Card { Id = 2, Title = "Create DB", ColumnId = 2 });
        }
    }
}
