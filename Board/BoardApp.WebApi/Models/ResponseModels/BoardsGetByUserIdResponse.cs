using BoardApp.Common.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BoardApp.WebApi.Models.ResponseModels
{
    public class BoardsGetByUserIdResponse
    {
        [JsonPropertyName("BoardId")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<PermissionDto> Permission { get; set; }
    }
}
