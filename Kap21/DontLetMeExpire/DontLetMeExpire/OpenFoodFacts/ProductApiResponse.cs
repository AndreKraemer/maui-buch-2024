namespace DontLetMeExpire.OpenFoodFacts;

public class ProductApiResponse
{
  public string Code { get; set; }

  public Product? Product { get; set; }

  public int Status { get; set; }
}
