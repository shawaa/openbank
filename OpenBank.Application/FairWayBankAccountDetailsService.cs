using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace OpenBank.Application
{
    public class FairWayBankAccountDetailsService : IBankAccountService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;

        public FairWayBankAccountDetailsService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        public Banks Bank => Banks.FairWayBank;

        public async Task<(UserDetailsDto dto, Error error)> GetUserDetails(string accountNumber)
        {
            JObject accountJobject = JObject.Parse((await _httpClientWrapper.Get($"http://fairwaybank-bizfitech.azurewebsites.net/api/v1/accounts/{accountNumber}")));

            if (accountJobject.Value<string>("errorCode") != null)
            {
                return (null, new Error(accountJobject.Value<string>("message")));
            }

            JObject balanceJobject = JObject.Parse((await _httpClientWrapper.Get($"http://fairwaybank-bizfitech.azurewebsites.net/api/v1/accounts/{accountNumber}/balance")));

            decimal availableBalance = balanceJobject.Value<decimal?>("overdraft.amount") != null
                ? balanceJobject.Value<decimal>("amount") + balanceJobject.Value<decimal?>("overdraft.amount").Value
                : balanceJobject.Value<decimal>("amount");

            UserDetailsDto dto = new UserDetailsDto
            {
                AccountName = accountJobject.Value<string>("name"),
                AccountNumber = accountJobject.Value<string>("identifier.accountNumber"),
                SortCode = accountJobject.Value<string>("identifier.accountNumber"),
                Balance = balanceJobject.Value<decimal>("amount"),
                AvailableBalance = availableBalance,
                Overdraft = balanceJobject.Value<decimal?>("overdraft.amount")
            };

            return (dto, null);
        }
    }
}
