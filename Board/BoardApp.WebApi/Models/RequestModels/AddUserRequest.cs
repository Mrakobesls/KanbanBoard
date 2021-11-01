using System.ComponentModel.DataAnnotations;

namespace BoardApp.WebApi.Models.RequestModels
{
    public class AddUserRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BoardId { get; set; }
        [Required]
        public int PermissionId { get; set; }
    }
}
