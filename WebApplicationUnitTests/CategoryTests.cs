using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using WebApplication3.Controllers;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplicationUnitTests;

public class CategoryTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Create_WhenCalled_CreateViewShouldRender()
    {
        var service = new Mock<ICategoryService>();
        var controller = new CategoryController(service.Object);
        
        var result = controller.Create() as ViewResult;
        
        Assert.That(result?.ViewName, Is.EqualTo("Create"));
    }
    
    [Test]
    public void Create_WhenCalled_RedirectToIndex()
    {
        ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
        TempDataDictionaryFactory tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
        ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
        var service = new Mock<ICategoryService>();
        var controller = new CategoryController(service.Object)
        {
            TempData = tempData
        };
        
        var result = controller.Create(new Category()) as RedirectToActionResult;
        
        Assert.That(result?.ActionName, Is.EqualTo("Index"));
    }
    
    [Test]
    public void Index_WhenCalled_IndexViewShouldRender()
    {
        var service = new Mock<ICategoryService>();
        var controller = new CategoryController(service.Object);
        
        var result = controller.Index() as ViewResult;
        
        Assert.That(result?.ViewName, Is.EqualTo("Index"));
    }
    
    
    [Test]
    public void Edit_WhenCalled_EditViewShouldRender()
    {
        var service = new Mock<ICategoryService>();
        var controller = new CategoryController(service.Object);
        
        var result = controller.Edit(1) as ViewResult;
        
        Assert.That(result?.ViewName, Is.EqualTo("Edit"));
    }
    
    [Test]
    public void Edit_WhenCalled_FindThatCategoryInDataBase()
    {
        var service = new Mock<ICategoryService>();
        var controller = new CategoryController(service.Object);
        
        controller.Edit(1);
        
        service.Verify(s=> s.FindCategory(1));

    }
    
    [Test]
    public void EditPost_WhenCalled_EditACategoryInDataBase()
    {
        var service = new Mock<ICategoryService>();
        var controller = new CategoryController(service.Object);
        var category = new Category();
        
        controller.Edit(category);
        
        service.Verify(s=> s.EditCategory(category));

    }
    
    [Test]
    public void EditPost_WhenCalledWithValidCategory_ShouldRedirectToIndexAction()
    {
        var service = new Mock<ICategoryService>();
        var controller = new CategoryController(service.Object);
        var category = new Category
        {
            Id = 0,
            Name = "puzzles",
            DisplayOrder = 1,
            CreatedDateTime =Convert.ToString(DateTime.Now, CultureInfo.InvariantCulture)
        };
        
        var result =  controller.Edit(category) as RedirectToActionResult;
        
        Assert.That(result!.ActionName,Is.EqualTo("Index"));

    }

    [Test]
    public void Delete_WhenCalled_DeleteViewShouldRender()
    {
        var service = new Mock<ICategoryService>();
        var controller = new CategoryController(service.Object);
        
        var result = controller.Delete(1) as ViewResult;
        
        Assert.That(result?.ViewName, Is.EqualTo("Delete"));
    }
    
    [Test]
    public void Delete_WhenCalled_FindThatCategoryInDataBase()
    {
        var service = new Mock<ICategoryService>();
        var controller = new CategoryController(service.Object);
        
        controller.Delete(1);
        
        service.Verify(s=> s.FindCategory(1));

    }
    
    
    
    [Test]
    public void DeletePost_WhenCalled_DeleteThatCategoryInDataBase()
    {
        var service = new Mock<ICategoryService>();
        var controller = new CategoryController(service.Object);
        var category = new Category();
        
        controller.DeletePost(category);
        
        service.Verify(s=> s.DeleteCategory(category));

    }
    
    [Test]
    public void DeletePost_WhenCalledWithValidCategory_ShouldRedirectToIndexAction()
    {
        var service = new Mock<ICategoryService>();
        var controller = new CategoryController(service.Object);
        var category = new Category
        {
            Id = 0,
            Name = "puzzles",
            DisplayOrder = 1,
            CreatedDateTime =Convert.ToString(DateTime.Now, CultureInfo.InvariantCulture)
        };
        
        var result =  controller.DeletePost(category) as RedirectToActionResult;
        
        Assert.That(result!.ActionName,Is.EqualTo("Index"));

    }
    
    
    
}