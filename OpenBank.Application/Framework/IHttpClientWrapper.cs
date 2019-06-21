using System.Threading.Tasks;

namespace OpenBank.Application
{
    public interface IHttpClientWrapper
    {
        Task<string> Get(string uri);
    }
}
