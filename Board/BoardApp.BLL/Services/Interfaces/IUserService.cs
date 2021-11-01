using BoardApp.Common.Models;

namespace BoardApp.BLL.Services
{
    public interface IUserService : IGenericService<UserDto>
    {
       UserDto Authenticate(string loginOrEmail, string password);
    }
}
