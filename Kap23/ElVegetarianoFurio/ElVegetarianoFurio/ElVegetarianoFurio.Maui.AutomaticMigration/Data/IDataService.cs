using ElVegetarianoFurio.Maui.AutomaticMigration.Menu;

namespace ElVegetarianoFurio.Maui.AutomaticMigration.Data
{
    public interface IDataService
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<List<Dish>> GetDishesAsync(int? categoryId = null);
        Task<Dish> GetDishAsync(int id);
    }
}