using System;

namespace BoardApp.DAL.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public int CardId { get; set; }
        public virtual Card Card { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
