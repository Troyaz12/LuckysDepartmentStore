

using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Service;
using Microsoft.EntityFrameworkCore;
using Moq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Sockets;
using LuckysDepartmentStore.Models.ViewModels.Product;
using NuGet.Packaging.Licenses;
using AutoMapper;

namespace XunitTestProject
{
    public class ProductService
    {
        //[Fact]
        //public void ProductServiceTestPass()
        //{
        //    ProductVM vm = new ProductVM();
        //    vm.ColorSelection = "RED";

        //    var catService = new Mock<ICategoryService>();
        //    catService.Setup(cat => cat.Create(vm)).Returns(5);

        //    var colorService = new Mock<IColorService>();
        //    colorService.Setup(color => color.Create(vm)).Returns(5);

        //    var subCatService = new Mock<ISubCategoryService>();
        //    subCatService.Setup(subCat => subCat.Create(vm)).Returns(5);

        //    var brandService = new Mock<IBrandService>();
        //    brandService.Setup(brandCat => brandCat.Create(vm)).Returns(5);


        //    var mapper = new Mock<IMapper>();

        //    var options = new DbContextOptionsBuilder<LuckysContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

        //    var mockColorTable = new Mock<DbSet<Color>>();
        //    var colors =  new List<Color>
        //    {
        //        new Color {ColorID=5, Name="Red",CreatedDate=DateTime.Now}
        //    }.AsQueryable();

        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.Provider).Returns(colors.Provider);
        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.Expression).Returns(colors.Expression);
        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.ElementType).Returns(colors.ElementType);
        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.GetEnumerator()).Returns(colors.GetEnumerator());

        //    var mockContext = new Mock<LuckysContext>(options);
        //    mockContext.Setup(c => c.Colors).Returns(mockColorTable.Object);

        //    // Create the service or repository that uses the context
        //    var service = new LuckysDepartmentStore.Service.ProductService(mockContext.Object, mapper.Object, 
        //        colorService.Object, catService.Object, subCatService.Object, brandService.Object, );

            
        //    var res= service.Create(vm);

        //    // Assert
        //    Assert.Equal(true, res.IsCompleted);
        //    Assert.Equal(null, res.Exception);
        //}
        //[Fact]
        //public void ProductServiceTestFail()
        //{
        //    ProductVM vm = new ProductVM();
        //    vm.ColorSelection = "RED";

        //    var catService = new Mock<ICategoryService>();
        //    catService.Setup(cat => cat.Create(vm)).Throws(new Exception("Upload failed."));

        //    var colorService = new Mock<IColorService>();
        //    colorService.Setup(color => color.Create(vm)).Returns(5);

        //    var subCatService = new Mock<ISubCategoryService>();
        //    subCatService.Setup(subCat => subCat.Create(vm)).Returns(5);

        //    var brandService = new Mock<IBrandService>();
        //    brandService.Setup(brandCat => brandCat.Create(vm)).Returns(5);


        //    var mapper = new Mock<IMapper>();

        //    var options = new DbContextOptionsBuilder<LuckysContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

        //    var mockColorTable = new Mock<DbSet<Color>>();
        //    var colors = new List<Color>
        //    {
        //        new Color {ColorID=5, Name="Red",CreatedDate=DateTime.Now}
        //    }.AsQueryable();

        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.Provider).Returns(colors.Provider);
        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.Expression).Returns(colors.Expression);
        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.ElementType).Returns(colors.ElementType);
        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.GetEnumerator()).Returns(colors.GetEnumerator());

        //    var mockContext = new Mock<LuckysContext>(options);
        //    mockContext.Setup(c => c.Colors).Returns(mockColorTable.Object);

        //    // Create the service or repository that uses the context
        //    var service = new LuckysDepartmentStore.Service.ProductService(mockContext.Object, mapper.Object, colorService.Object, catService.Object, subCatService.Object, brandService.Object);


        //    var res = service.Create(vm);

        //    // Assert
        //    Assert.Equal(true, res.IsCompleted);
        //    Assert.Equal("An error occurred while processing your request", res.Exception.InnerException.Message);
        //}
        //[Fact]
        //public void ColorIDProvidedPass() // since color has an id. this test should not make a color service request.
        //{
        //    ProductVM vm = new ProductVM();
        //    vm.ColorSelection = "RED";
        //    vm.ColorId = 1;

        //    var catService = new Mock<ICategoryService>();
        //    catService.Setup(cat => cat.Create(vm)).Returns(5);

        //    var colorService = new Mock<IColorService>();
        //    colorService.Setup(color => color.Create(vm)).Throws(new Exception("Timed out"));

        //    var subCatService = new Mock<ISubCategoryService>();
        //    subCatService.Setup(subCat => subCat.Create(vm)).Returns(5);

        //    var brandService = new Mock<IBrandService>();
        //    brandService.Setup(brandCat => brandCat.Create(vm)).Returns(5);


        //    var mapper = new Mock<IMapper>();

        //    var options = new DbContextOptionsBuilder<LuckysContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

        //    var mockColorTable = new Mock<DbSet<Color>>();
        //    var colors = new List<Color>
        //    {
        //        new Color {ColorID=5, Name="Red",CreatedDate=DateTime.Now}
        //    }.AsQueryable();

        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.Provider).Returns(colors.Provider);
        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.Expression).Returns(colors.Expression);
        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.ElementType).Returns(colors.ElementType);
        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.GetEnumerator()).Returns(colors.GetEnumerator());

        //    var mockContext = new Mock<LuckysContext>(options);
        //    mockContext.Setup(c => c.Colors).Returns(mockColorTable.Object);

        //    // Create the service or repository that uses the context
        //    var service = new LuckysDepartmentStore.Service.ProductService(mockContext.Object, mapper.Object, colorService.Object, catService.Object, subCatService.Object, brandService.Object);


        //    var res = service.Create(vm);

        //    // Assert
        //    Assert.Equal(true, res.IsCompleted);
        //    Assert.Equal(null, res.Exception);
        //}
        //[Fact]
        //public void CategroyIDProvidedPass() // since category has an id. this test should not make a category service request.
        //{
        //    ProductVM vm = new ProductVM();
        //    vm.ColorSelection = "RED";
        //    vm.CategoryId = 1;

        //    var catService = new Mock<ICategoryService>();
        //    catService.Setup(cat => cat.Create(vm)).Throws(new Exception("Timed out"));
            

        //    var colorService = new Mock<IColorService>();
        //    colorService.Setup(color => color.Create(vm)).Returns(5);

        //    var subCatService = new Mock<ISubCategoryService>();
        //    subCatService.Setup(subCat => subCat.Create(vm)).Returns(5);

        //    var brandService = new Mock<IBrandService>();
        //    brandService.Setup(brandCat => brandCat.Create(vm)).Returns(5);


        //    var mapper = new Mock<IMapper>();

        //    var options = new DbContextOptionsBuilder<LuckysContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

        //    var mockColorTable = new Mock<DbSet<Color>>();
        //    var colors = new List<Color>
        //    {
        //        new Color {ColorID=5, Name="Red",CreatedDate=DateTime.Now}
        //    }.AsQueryable();

        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.Provider).Returns(colors.Provider);
        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.Expression).Returns(colors.Expression);
        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.ElementType).Returns(colors.ElementType);
        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.GetEnumerator()).Returns(colors.GetEnumerator());

        //    var mockContext = new Mock<LuckysContext>(options);
        //    mockContext.Setup(c => c.Colors).Returns(mockColorTable.Object);

        //    // Create the service or repository that uses the context
        //    var service = new LuckysDepartmentStore.Service.ProductService(mockContext.Object, mapper.Object, colorService.Object, catService.Object, subCatService.Object, brandService.Object);


        //    var res = service.Create(vm);

        //    // Assert
        //    Assert.Equal(true, res.IsCompleted);
        //    Assert.Equal(null, res.Exception);
        //}
        //[Fact]
        //public void SubCategroyIDProvidedPass() // since subcategory has an id. this test should not make a subcategory service request.
        //{
        //    ProductVM vm = new ProductVM();
        //    vm.ColorSelection = "RED";
        //    vm.SubCategoryId = 1;

        //    var catService = new Mock<ICategoryService>();
        //    catService.Setup(cat => cat.Create(vm)).Returns(5);


        //    var colorService = new Mock<IColorService>();
        //    colorService.Setup(color => color.Create(vm)).Returns(5);

        //    var subCatService = new Mock<ISubCategoryService>();
        //    subCatService.Setup(subCat => subCat.Create(vm)).Throws(new Exception("Timed out"));

        //    var brandService = new Mock<IBrandService>();
        //    brandService.Setup(brandCat => brandCat.Create(vm)).Returns(5);

        //    var mapper = new Mock<IMapper>();

        //    var options = new DbContextOptionsBuilder<LuckysContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

        //    var mockColorTable = new Mock<DbSet<Color>>();
        //    var colors = new List<Color>
        //    {
        //        new Color {ColorID=5, Name="Red",CreatedDate=DateTime.Now}
        //    }.AsQueryable();

        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.Provider).Returns(colors.Provider);
        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.Expression).Returns(colors.Expression);
        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.ElementType).Returns(colors.ElementType);
        //    mockColorTable.As<IQueryable<Color>>().Setup(m => m.GetEnumerator()).Returns(colors.GetEnumerator());

        //    var mockContext = new Mock<LuckysContext>(options);
        //    mockContext.Setup(c => c.Colors).Returns(mockColorTable.Object);

        //    // Create the service or repository that uses the context
        //    var service = new LuckysDepartmentStore.Service.ProductService(mockContext.Object, mapper.Object, colorService.Object, catService.Object, subCatService.Object, brandService.Object);


        //    var res = service.Create(vm);

        //    // Assert
        //    Assert.Equal(true, res.IsCompleted);
        //    Assert.Equal(null, res.Exception);
        //}
    }
}