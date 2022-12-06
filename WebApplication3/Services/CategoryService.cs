using System.ComponentModel.Design;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data;
using WebApplication3.Models;


namespace WebApplication3.Services;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _db;
    public CategoryService(ApplicationDbContext db)
    {
        _db = db;
    }

    public void AddCategory(Category obj)
    {
        _db.Categories.Add(obj);
        _db.SaveChanges();
    }

    public IEnumerable<Category> GetAllCategories()
    {
        return _db.Categories;
    }

    public Category FindCategory(int id)
    {
         return _db.Categories.Find(id)!;
    }

    public void EditCategory(Category category)
    {
        _db.Categories.Update(category);
        _db.SaveChanges();
    }

    public void DeleteCategory(Category category)
    {
        _db.Categories.Remove(category);
        _db.SaveChanges();
    }
}
