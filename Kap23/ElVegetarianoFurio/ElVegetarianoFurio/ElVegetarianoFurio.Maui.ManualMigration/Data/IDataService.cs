using ElVegetarianoFurio.Maui.ManualMigration.Menu;

namespace ElVegetarianoFurio.Maui.ManualMigration.Data;

public interface IDataService
{
  Task<List<Category>> GetCategoriesAsync();
  Task<List<Dish>> GetDishesAsync(int? categoryId = null);
  Task<Dish> GetDishAsync(int id);
}