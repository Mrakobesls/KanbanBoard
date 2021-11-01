using BoardApp.Common.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoardApp.WebApi.Models.ResponseModels
{
    public class BoardsGetByIdResponse
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }
        public List<ColumnDto> Columns { get; set; }
        public List<BoardsMemberListResponse> Members { get; set; }
    }
}
