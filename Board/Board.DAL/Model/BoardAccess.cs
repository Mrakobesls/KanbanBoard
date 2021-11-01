namespace BoardApp.DAL.Model
{
    public class BoardAccess
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int BoardId { get; set; }
        public virtual Board Board { get; set; }
        public int PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
