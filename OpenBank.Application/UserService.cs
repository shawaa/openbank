using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OpenBank.Application
{
    public class UserService : IUserService
    {
        private readonly UserContext _userContext;

        public UserService(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<(User user, Error error)> AddUser(CreateUserDto createUserDto)
        {
            (User user, Error error) userResult = User.CreateUser(createUserDto);

            if (userResult.error != null)
            {
                return (null, userResult.error);
            }

            var usersWithSameAccountNumber = await _userContext.Users
                .Where(x => x.AccountNumber == createUserDto.AccountNumber)
                .Select(x => x.AccountNumber)
                .ToListAsync();

            if (usersWithSameAccountNumber.Count() != 0)
            {
                return (null, new Error("A user with that account number already exists"));
            }

            await _userContext.AddAsync(userResult.user);

            return (userResult.user, null);
        }
    }
}
