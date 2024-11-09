
namespace DontLetMeExpire.OpenFoodFacts
{
    public interface IOpenFoodFactsApiClient
    {
        Task<byte[]> DownloadImage(string imageUrl);
        Task<ProductApiResponse> GetProductByCodeAsync(string code);
    }
}