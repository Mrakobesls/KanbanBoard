using BoardApp.DAL.Model;
using System.Collections.Generic;

namespace BoardApp.WebApi.Models.ResponseModels
{
    public class CardAddUserResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
