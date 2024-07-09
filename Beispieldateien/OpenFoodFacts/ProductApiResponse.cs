using System.Text.Json.Serialization;

namespace DontLetMeExpire.OpenFoodFacts
{
    public class ProductApiResponse
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("product")]
        public Product? Product { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("status_verbose")]
        public string StatusVerbose { get; set; }
    }
}
