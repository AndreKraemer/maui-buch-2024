using System.Threading.Tasks;
using Xamarin.Forms;

namespace ElVegetarianoFurio.Core
{
    public class NavigationService : INavigationService
    {
        public async Task GoToAsync(string location)
        {
            await Shell.Current.GoToAsync(location);
        }

        public async Task GoToAsync(string location, bool animate)
        {
            await Shell.Current.GoToAsync(location, animate);
        }
    }
}