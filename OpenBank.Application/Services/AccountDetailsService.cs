using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OpenBank.Application
{
    public class AccountDetailsService : IAccountDetailsService
    {
        private IEnumerable<IBankAccountService> _bankAccountServices;

        private UserContext _userContext;

        public AccountDetailsService(IEnumerable<IBankAccountService> bankAccountServices, UserContext userContext)
        {
            _bankAccountServices = bankAccountServices;
            _userContext = userContext;
        }

        public async Task<(UserDetailsDto dto, Error error)> GetUserAccountDetails(Guid id)
        {
            User user = await _userContext.Users.Where(x => x.Id == id).SingleOrDefaultAsync();

            if (user == null)
            {
                return (null, new Error($"User id {id} was not found"));
            }

            return await _bankAccountServices.Single(x => x.Bank == user.Bank).GetUserDetails(user.AccountNumber);
        }

        public async Task<(IEnumerable<TransactionDto> transactions, Error error)> GetTransactions(Guid id)
        {
            User user = await _userContext.Users.Where(x => x.Id == id).SingleOrDefaultAsync();

            if (user == null)
            {
                return (null, new Error($"User id {id} was not found"));
            }

            return await _bankAccountServices.Single(x => x.Bank == user.Bank).GetTransactions(user.AccountNumber);
        }
    }
}
