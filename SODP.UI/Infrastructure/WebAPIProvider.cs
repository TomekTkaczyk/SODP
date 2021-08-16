using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Infrastructure
{
    public class WebAPIProvider : IWebAPIProvider
    {
        private readonly string _apiUrl;
        private readonly string _apiVersion;
        public WebAPIProvider(IConfiguration configuration)
        {
            _apiUrl = configuration.GetSection($"AppSettings:apiUrl").Value;
            _apiVersion = configuration.GetSection($"AppSettings:apiVersion").Value;
        }

        public async Task<HttpResponseMessage> GetAsync(string endPoint)
        {
            var apiResponse = await new HttpClient().GetAsync($"{_apiUrl}{_apiVersion}{endPoint}");

            return apiResponse;
        }

        public async Task<HttpResponseMessage> PostAsync(string endPoint, StringContent content)
        {
            return await new HttpClient().PostAsync($"{_apiUrl}{_apiVersion}{endPoint}", content);
        }

        public async Task<HttpResponseMessage> PutAsync(string endPoint, StringContent content)
        {
            var response = await new HttpClient().PutAsync($"{_apiUrl}{_apiVersion}{endPoint}", content);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string endPoint)
        {
            return await new HttpClient().DeleteAsync($"{_apiUrl}{_apiVersion}{endPoint}");
        }

        public async Task<T> GetContent<T>(HttpResponseMessage message)
        {
            return await message.Content.ReadAsAsync<T>();
        }
    }
}
