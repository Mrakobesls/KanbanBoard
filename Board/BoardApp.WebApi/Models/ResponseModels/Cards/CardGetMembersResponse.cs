using System.Collections.Generic;

namespace BoardApp.WebApi.Models.ResponseModels
{
    public class CardGetMembersResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<UserModel> Users { get; set; }
    }
}
