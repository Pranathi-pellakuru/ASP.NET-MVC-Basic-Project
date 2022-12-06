using WebApplication3.Models;

namespace WebApplication3.Services;

public interface ICategoryService 
{
    void AddCategory(Category obj);
    IEnumerable<Category> GetAllCategories();
    Category FindCategory(int id);
    void EditCategory(Category category);
    void DeleteCategory(Category category);
}