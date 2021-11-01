using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using BoardApp.Common;


namespace BoardApp.DAL.ModelsSeed
{
    public class SeedPermission : IEntityTypeConfiguration<Model.Permission>
    {
        public void Configure(EntityTypeBuilder<Model.Permission> builder)
        {
            foreach (var item in Enum.GetValues(typeof(Access)))
            {
                builder.HasData(new Model.Permission { Id = (int)item, Name = item.ToString() });
            }
        }
    }
}
