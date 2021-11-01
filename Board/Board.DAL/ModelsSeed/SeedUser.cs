using BoardApp.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardApp.DAL.ModelsSeed
{
    public class SeedUser : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User 
                { 
                    Id = 1, 
                    Email = "ArtemVT@gmail.com", 
                    FirstName = "Artem", 
                    LastName = "Valtert", 
                    Login = "ArtemVT", 
                    Password = "10000.52eP9KlTbWGwkSk18vxjUw==.RjOPBUeIfkBpdCQE82SlvVVz8CZU0gn9XggLARm55OI="
                },
                new User 
                { 
                    Id = 2, 
                    Email = "DimaKR", 
                    FirstName = "Dima", 
                    LastName = "Karabanovich", 
                    Login = "DimaKR",
                    Password = "10000.52eP9KlTbWGwkSk18vxjUw==.RjOPBUeIfkBpdCQE82SlvVVz8CZU0gn9XggLARm55OI="
                });
        }
    }
}
