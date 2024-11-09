using WebserviceSample.Models;

namespace WebserviceSample.Services
{
  public interface ISpeakerService
  {
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Speaker>> GetAsync(bool forceRefresh = false);
    Task<Speaker> GetAsync(int id);
    Task<bool> CreateAsync(Speaker speaker);
    Task<bool> UpdateAsync(Speaker speaker);
  }
}