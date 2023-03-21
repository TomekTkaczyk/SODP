using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Infrastructure
{
    public interface IWebAPIProvider
    {
        Task<HttpResponseMessage> GetAsync(string endPoint);
        Task<HttpResponseMessage> PostAsync(string endPoint, StringContent content);
        Task<HttpResponseMessage> PutAsync(string endPoint, StringContent content);
        Task<HttpResponseMessage> PatchAsync(string endPoint, StringContent content);
        Task<HttpResponseMessage> DeleteAsync(string endPoint);

        Task<T> GetContent<T>(HttpResponseMessage message);
    }
}
