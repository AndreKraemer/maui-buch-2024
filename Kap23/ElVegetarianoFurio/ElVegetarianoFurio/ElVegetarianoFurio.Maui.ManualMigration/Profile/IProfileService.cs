using System.Threading.Tasks;

namespace ElVegetarianoFurio.Maui.ManualMigration.Profile
{
    public interface IProfileService
    {
        Task<ElVegetarianoFurio.Maui.ManualMigration.Profile.Profile> GetAsync();
        Task<bool> SaveAsync(ElVegetarianoFurio.Maui.ManualMigration.Profile.Profile profile);
    }
}