using System;

namespace BoardApp.Common.Models
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int CardId { get; set; }
        public int UserId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
