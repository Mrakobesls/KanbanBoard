namespace BoardApp.Common.Models
{
    public class BoardAccessDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BoardId { get; set; }
        public int PermissionId { get; set; }
        public PermissionDto Permission { get; set; }
    }
}
