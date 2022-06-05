using App.Interfaces;
using App.Models;
using Microsoft.Extensions.Options;

namespace App.Services
{
    public class MovieReviewService : Interfaces.IMovieReviewService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiOptions _options;

        public MovieReviewService(IHttpClientFactory httpClientFactory, IOptions<ApiOptions> options)
        {
            _options = options.Value;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = GetBaseUrl();
        }

        public async Task<string> GetStringAsync(string name)
        {
            var url = GetQuery(name);
            var json = await _httpClient.GetStringAsync(url);

            return json;
        }

        public async Task<string> GetAsync(string name)
        {
            var url = GetQuery(name);
            var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }

        public async Task<ApiResponse> GetFromJsonAsync(string name)
        {
            var url = GetQuery(name);
            var model = await _httpClient.GetFromJsonAsync<ApiResponse>(url);

            return model;
        }

        public async Task<string> SendAsync(string name)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(GetBaseUrl(), GetQuery(name))
            };

            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }

        private Uri GetBaseUrl() => new(_options.BaseUrl, UriKind.Absolute);

        private string GetQuery(string query) => string.Format(_options.MovieReviewsEndpoint, query, _options.ApiKey);
    }
}