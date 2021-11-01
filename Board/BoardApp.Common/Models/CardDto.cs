using System.Collections.Generic;

namespace BoardApp.Common.Models
{
    public class CardDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ColumnId { get; set; }
        public IList<LabelDto> Labels { get; set; }
        public IList<CommentDto> Comments { get; set; }
        public IList<UserDto> Users { get; set; }
    }
}
