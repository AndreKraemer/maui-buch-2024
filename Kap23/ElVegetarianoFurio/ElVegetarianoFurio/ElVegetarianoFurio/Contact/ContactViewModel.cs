using ElVegetarianoFurio.Core;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ElVegetarianoFurio.Contact
{
    public class ContactViewModel : BaseViewModel
    {
        public ContactViewModel()
        {
            Title = "Kontakt";
            NavigateCommand = new Command(OnNavigate);
            CallCommand = new Command(OnCall);
        }

        public Command NavigateCommand { get; }
        public Command CallCommand { get; }

        private void OnNavigate()
        {
            var placemark = new Placemark
            {
                CountryName = "Germany",
                PostalCode = "53498",
                Locality = "Bad Breisig",
                Thoroughfare = "Brunnenstraße 21"
            };

            var options = new MapLaunchOptions
            {
                Name = "El Vegetariano Furio",
                NavigationMode = NavigationMode.Driving
            };
            Map.OpenAsync(placemark, options);
            
        }

        private void OnCall()
        {
            PhoneDialer.Open("0123456789");
        }
    }
}