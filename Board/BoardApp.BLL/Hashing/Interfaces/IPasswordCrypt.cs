namespace BoardApp.BLL.Hashing
{
    public interface IPasswordCrypt
    {
        public string HashPassword(string password);
        public bool Verify(string password, string hashedPassword);
    }
}
