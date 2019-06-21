using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenBank.Application
{
    public interface IUserService
    {
        Task<(User user, Error error)> AddUser(CreateUserDto user);

        Task<IEnumerable<UserDto>> GetAllUsers();
    }
}
