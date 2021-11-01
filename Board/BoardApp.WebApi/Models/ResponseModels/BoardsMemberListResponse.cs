using BoardApp.Common.Models;
using System.Collections.Generic;

namespace BoardApp.WebApi.Models.ResponseModels
{
    public class BoardsMemberListResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
