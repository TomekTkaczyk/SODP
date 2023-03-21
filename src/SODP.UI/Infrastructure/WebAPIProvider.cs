using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Infrastructure
{
    public class WebAPIProvider : IWebAPIProvider
    {
        private readonly HttpClient _httpClient;

        public WebAPIProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("SODPApiClient");
        }

        public async Task<HttpResponseMessage> GetAsync(string endPoint)
        {
            var apiResponse = await _httpClient.GetAsync(endPoint);

            apiResponse.EnsureSuccessStatusCode();

            return apiResponse;
        }

        public async Task<HttpResponseMessage> PostAsync(string endPoint, StringContent content)
        {
            var apiResponse = await new HttpClient().PostAsync($"{_httpClient.BaseAddress}{endPoint}", content);

            apiResponse.EnsureSuccessStatusCode();

            return apiResponse;
        }

        public async Task<HttpResponseMessage> PutAsync(string endPoint, StringContent content)
        {
            var apiResponse = await new HttpClient().PutAsync($"{_httpClient.BaseAddress}{endPoint}", content);

            apiResponse.EnsureSuccessStatusCode();

            return apiResponse;
        }

        public async Task<HttpResponseMessage> PatchAsync(string endPoint, StringContent content)
        {
            var apiResponse = await new HttpClient().PatchAsync($"{_httpClient.BaseAddress}{endPoint}", content);

            apiResponse.EnsureSuccessStatusCode();

            return apiResponse;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string endPoint)
        {
            var apiResponse = await new HttpClient().DeleteAsync($"{_httpClient.BaseAddress}{endPoint}");

            apiResponse.EnsureSuccessStatusCode();

            return apiResponse;
        }

        public async Task<T> GetContent<T>(HttpResponseMessage message)
        {
            return await message.Content.ReadAsAsync<T>();
        }
    }
}
