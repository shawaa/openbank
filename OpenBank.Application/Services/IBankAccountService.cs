using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenBank.Application
{
    public interface IBankAccountService
    {
        Task<(UserDetailsDto dto, Error error)> GetUserDetails(string accountNumber);

        Task<(IEnumerable<TransactionDto> transactions, Error error)> GetTransactions(string accountNumber);

        Banks Bank { get; }
    }
}
