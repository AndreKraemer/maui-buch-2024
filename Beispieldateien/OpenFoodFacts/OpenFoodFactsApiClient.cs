using System.Net.Http.Headers;
using System.Text.Json;

namespace DontLetMeExpire.OpenFoodFacts
{
    public class OpenFoodFactsApiClient : IOpenFoodFactsApiClient
    {

        private readonly HttpClient _httpClient;

        public OpenFoodFactsApiClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://world.openfoodfacts.org/api/v2/"),
                DefaultRequestHeaders =
                {
                    Accept =
                    {
                        new MediaTypeWithQualityHeaderValue("application/json")
                    },
                    UserAgent =
                    {
                        new ProductInfoHeaderValue("DontLetMeExpire", "1.0")
                    }
                }
            };
        }

        public async Task<ProductApiResponse> GetProductByCodeAsync(string code)
        {
            var requestUri = $"product/{code}.json";
            var response = await _httpClient.GetAsync(requestUri);

            var content = await response.Content.ReadAsStringAsync();
            var productApiResponse = JsonSerializer.Deserialize<ProductApiResponse>(content);

            return productApiResponse;
        }
        public async Task<byte[]> DownloadImage(string imageUrl)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync(imageUrl);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to download image from URL: {imageUrl}");
            }

            return await response.Content.ReadAsByteArrayAsync();
        }


    }
}
