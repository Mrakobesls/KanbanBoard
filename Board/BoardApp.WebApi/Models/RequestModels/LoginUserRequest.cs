using System.ComponentModel.DataAnnotations;

namespace BoardApp.WebApi.Models.RequestModels
{
    public class LoginUserRequest
    {
        [Required]
        public string LoginOrEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
