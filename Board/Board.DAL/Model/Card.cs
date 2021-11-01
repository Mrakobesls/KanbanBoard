using System.Collections.Generic;

namespace BoardApp.DAL.Model
{
    public class Card
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ColumnId { get; set; }
        public virtual Column Column { get; set; }
        public virtual ICollection<Label> Labels { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
