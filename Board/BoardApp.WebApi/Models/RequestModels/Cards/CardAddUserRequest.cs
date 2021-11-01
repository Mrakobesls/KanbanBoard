using System.ComponentModel.DataAnnotations;

namespace BoardApp.WebApi.Models.RequestModels
{
    public class CardAddUserRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
