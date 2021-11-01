using System.ComponentModel.DataAnnotations;

namespace BoardApp.WebApi.Models
{
    public class BoardModel
    {
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
