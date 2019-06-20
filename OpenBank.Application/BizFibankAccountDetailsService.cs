using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace OpenBank.Application
{
    public class BizFibankAccountDetailsService : IBankAccountService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;

        public BizFibankAccountDetailsService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        public Banks Bank => Banks.BizFiBank;

        public async Task<(UserDetailsDto dto, Error error)> GetUserDetails(string accountNumber)
        {
            JObject jobject = JObject.Parse((await _httpClientWrapper.Get($"http://bizfibank-bizfitech.azurewebsites.net/api/v1/accounts/{accountNumber}")));

            if (jobject.Value<string>("errorCode") != null)
            {
                return (null, new Error(jobject.Value<string>("message")));
            }

            UserDetailsDto dto = new UserDetailsDto
            {
                AccountName = jobject.Value<string>("account_name"),
                AccountNumber = jobject.Value<string>("account_number"),
                SortCode = jobject.Value<string>("sort_code"),
                Balance = jobject.Value<decimal>("balance"),
                AvailableBalance = jobject.Value<decimal>("available_balance"),
                Overdraft = jobject.Value<decimal?>("overdraft")
            };

            return (dto, null);
        }
    }
}
