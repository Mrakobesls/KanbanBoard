using System;
using BoardApp.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardApp.DAL.ModelsSeed
{
    public class SeedComment : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasData(
                new Comment { Id = 1, Text = "I can do it", UserId = 1, CardId = 1, DateTime = DateTime.Now },
                new Comment { Id = 2, Text = "It have to make Dima Karabanovich", UserId = 2, CardId = 2, DateTime = DateTime.Now });
        }
    }
}
