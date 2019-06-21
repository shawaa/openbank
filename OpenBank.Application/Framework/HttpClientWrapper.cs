using System.Net.Http;
using System.Threading.Tasks;

namespace OpenBank.Application
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> Get(string uri)
        {
            return await (await _httpClient.GetAsync(uri)).Content.ReadAsStringAsync();
        }
    }
}
