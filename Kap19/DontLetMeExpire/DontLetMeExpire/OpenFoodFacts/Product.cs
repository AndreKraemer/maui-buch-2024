using System.Text.Json.Serialization;

namespace DontLetMeExpire.OpenFoodFacts;

public class Product
{
  [JsonPropertyName("_id")]
  public string Id { get; set; }

  [JsonPropertyName("image_url")]
  public string? ImageUrl { get; set; }

  [JsonPropertyName("product_name")]
  public string? ProductName { get; set; }
}
