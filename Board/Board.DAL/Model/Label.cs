using System.Collections.Generic;

namespace BoardApp.DAL.Model
{
    public class Label
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
    }
}
