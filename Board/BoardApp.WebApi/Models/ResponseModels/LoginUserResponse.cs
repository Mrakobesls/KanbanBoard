using System.Text.Json.Serialization;

namespace BoardApp.WebApi.Models.ResponseModels
{
    public class LoginUserResponse
    {
        public bool IsSuccess { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Token { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string ExceptionHandler { get; set; }

    }
}
