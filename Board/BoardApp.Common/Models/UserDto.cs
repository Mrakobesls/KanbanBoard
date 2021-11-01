using System.Collections.Generic;

namespace BoardApp.Common.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<CardDto> Cards { get; set; }
        public IList<CommentDto> Comments { get; set; }
        public IList<BoardAccessDto> BoardAccesses { get; set; }
    }
}
