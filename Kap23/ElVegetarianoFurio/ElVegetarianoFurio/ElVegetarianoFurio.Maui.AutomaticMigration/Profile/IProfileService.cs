namespace ElVegetarianoFurio.Maui.AutomaticMigration.Profile
{
    public interface IProfileService
    {
        Task<Profile> GetAsync();
        Task<bool> SaveAsync(Profile profile);
    }
}