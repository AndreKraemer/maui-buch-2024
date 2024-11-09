namespace ElVegetarianoFurio.Maui.ManualMigration.Core
{
    public interface INavigationService
    {
        Task GoToAsync(string location);
        Task GoToAsync(string location, bool animate);
    }
}