using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ElVegetarianoFurio.Profile
{
    public class ProfileService : IProfileService
    {
        public Task<Profile> GetAsync()
        {
            var profile = new Profile
            {
                City = Preferences.Get(nameof(Profile.City), ""),
                FullName = Preferences.Get(nameof(Profile.FullName), ""),
                Phone = Preferences.Get(nameof(Profile.Phone), ""),
                Street = Preferences.Get(nameof(Profile.Street), ""),
                Zip = Preferences.Get(nameof(Profile.Zip), "")
            };
            return Task.FromResult(profile);
        }

        public Task<bool> SaveAsync(Profile profile)
        {
            Preferences.Set(nameof(Profile.City), profile.City);
            Preferences.Set(nameof(Profile.FullName), profile.FullName);
            Preferences.Set(nameof(Profile.Phone), profile.Phone);
            Preferences.Set(nameof(Profile.Street), profile.Street);
            Preferences.Set(nameof(Profile.Zip), profile.Zip);
            return Task.FromResult(true);
        }
    }
}