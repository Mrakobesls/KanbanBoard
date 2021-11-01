using BoardApp.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardApp.DAL.ModelsSeed
{
    public class SeedBoard : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder.HasData(
                new Board { Id = 1, Title = "Bowling kata", Description = "Project about bowling" },
                new Board { Id = 2, Title = "Board project", Description = "Project about board" });
        }
    }
}
