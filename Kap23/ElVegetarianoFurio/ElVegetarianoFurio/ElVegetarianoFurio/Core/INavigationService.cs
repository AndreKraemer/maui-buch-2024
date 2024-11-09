using System.Threading.Tasks;

namespace ElVegetarianoFurio.Core
{
    public interface INavigationService
    {
        Task GoToAsync(string location);
        Task GoToAsync(string location, bool animate);
    }
}