using System.Collections.Generic;

namespace BoardApp.Common.Models
{
    public class ColumnDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int BoardId { get; set; }
        public IList<CardDto> Cards { get; set; }
    }
}
