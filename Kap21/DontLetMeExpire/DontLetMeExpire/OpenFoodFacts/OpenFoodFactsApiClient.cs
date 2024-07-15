using System.Net.Http.Headers;
using System.Text.Json;

namespace DontLetMeExpire.OpenFoodFacts;

public class OpenFoodFactsApiClient : IOpenFoodFactsApiClient
{

  private readonly HttpClient _httpClient;

  // Konfigurieren der JSON-Serialisierungsoptionen
  // um sicherzustellen, dass die Deserialisierung
  // nicht case-sensitive ist
  private readonly JsonSerializerOptions _jsonSerializerOptions = new()
  {
    PropertyNameCaseInsensitive = true
  };

  public OpenFoodFactsApiClient()
  {
    // Konfigurieren des HTTP-Clients
    _httpClient = new HttpClient
    {
      BaseAddress = new Uri("https://world.openfoodfacts.org/api/v2/"),
      DefaultRequestHeaders =
      {
        // Setzen des Accept-Headers auf application/json
        // um sicherzustellen, dass die API JSON zurückgibt
        Accept =
        {
          new MediaTypeWithQualityHeaderValue("application/json")
        },
        // Setzen des User-Agent-Headers um den API-Endpunkt
        // darüber zu informieren, welche Anwendung die Anfrage sendet
        UserAgent =
        {
          new ProductInfoHeaderValue("DontLetMeExpire", "1.0")
        }
      }
    };
  }

  // Abrufen eines Produkts anhand des Barcodes
  public async Task<ProductApiResponse> GetProductByCodeAsync(string code)
  {
    // Senden der Anfrage an den API-Endpunkt
    var requestUri = $"product/{code}.json";
    var response = await _httpClient.GetAsync(requestUri);

    // Deserialisieren der Antwort in ein Produkt-API-Antwortobjekt
    var content = await response.Content.ReadAsStringAsync();
    var productApiResponse = JsonSerializer.Deserialize<ProductApiResponse>(content, _jsonSerializerOptions);

    return productApiResponse;
  }

  // Herunterladen des Produktbilds
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
