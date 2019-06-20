using System;
using System.Threading.Tasks;

namespace OpenBank.Application
{
    public interface IAccountDetailsService
    {
        Task<(UserDetailsDto dto, Error error)> GetUserAccountDetails(Guid id);
    }
}
