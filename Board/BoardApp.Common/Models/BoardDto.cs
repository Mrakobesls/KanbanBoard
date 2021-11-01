using System.Collections.Generic;

namespace BoardApp.Common.Models
{
    public class BoardDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<BoardAccessDto> BoardAccesses { get; set; }
        public IList<ColumnDto> Columns { get; set; }
    }
}
