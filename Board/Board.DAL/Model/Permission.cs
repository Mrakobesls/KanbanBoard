using System.Collections.Generic;

namespace BoardApp.DAL.Model
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BoardAccess> BoardAccesses { get; set; }
    }
}
