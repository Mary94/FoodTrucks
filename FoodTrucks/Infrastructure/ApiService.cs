
namespace FindFoodTrucks.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public ApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = configuration.GetValue<Uri>("ApiBaseAddress");
            _apiUrl = configuration.GetValue<string>("ApiUrl");
        }

        public async Task<T> GetJsonAsync<T>()
        {
            var response = await _httpClient.GetAsync(_apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }

            else
            {
                throw new HttpRequestException($"API request failed with status code {response.StatusCode}");
            }
        }
    }
}
