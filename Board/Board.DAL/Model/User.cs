using System.Collections.Generic;

namespace BoardApp.DAL.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<BoardAccess> BoardAccesses { get; set; }
    }
}
