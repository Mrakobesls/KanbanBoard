using BoardApp.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardApp.DAL.ModelsSeed
{
    public class SeedColumn : IEntityTypeConfiguration<Column>
    {
        public void Configure(EntityTypeBuilder<Column> builder)
        {
            builder.HasData(
                new Column { Id = 1, Title = "New", BoardId = 1 },
                new Column { Id = 2, Title = "In Progress", BoardId = 2 });
        }
    }
}
