using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//using NewtonSoft.Json.Linq;

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
            await _userContext.SaveChangesAsync();

            return (userResult.user, null);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            IEnumerable<UserDto> users = (await _userContext.Users
                .Select(x => new { x.Id, x.Bank, x.AccountNumber })
                .ToListAsync())
                .Select(x => new UserDto
                {
                    Id = x.Id.Value,
                    Bank = x.Bank.ToString(),
                    AccountNumber = x.AccountNumber
                });

            return users;
        }
    }
}
