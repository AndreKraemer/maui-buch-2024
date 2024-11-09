using System.Text.Json.Serialization;

namespace DontLetMeExpire.OpenFoodFacts
{
    public class Product
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        [JsonPropertyName("brands")]
        public string? Brands { get; set; }

        [JsonPropertyName("image_thumb_url")]
        public string? ImageThumbUrl { get; set; }

        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("product_name")]
        public string? ProductName { get; set; }
    }
}
