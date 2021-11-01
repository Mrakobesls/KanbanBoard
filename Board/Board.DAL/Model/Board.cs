using System.Collections.Generic;

namespace BoardApp.DAL.Model
{
    public class Board
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<BoardAccess> BoardAccesses { get; set; }
        public virtual ICollection<Column> Columns { get; set; }
    }
}
