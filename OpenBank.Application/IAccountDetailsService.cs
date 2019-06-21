using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenBank.Application
{
    public interface IAccountDetailsService
    {
        Task<(UserDetailsDto dto, Error error)> GetUserAccountDetails(Guid id);

        Task<(IEnumerable<TransactionDto> transactions, Error error)> GetTransactions(Guid id);
    }
}
