using System.Collections.Generic;

namespace BoardApp.WebApi.Models
{
    public class CardModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<UserModel> Users { get; set; }
    }
}
