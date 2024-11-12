

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
    public class UnitTest1
    {
        [Fact]
        public void ColorTest()
        {
            ProductVM vm = new ProductVM();
            vm.Color = "RED";

            IMapper mapper;

            var options = new DbContextOptionsBuilder<LuckysContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            var mockColorTable = new Mock<DbSet<Color>>();
            var colors =  new List<Color>
            {
                new Color {ColorID=1, Name="Red",CreatedDate=DateTime.Now}
            }.AsQueryable();

            mockColorTable.As<IQueryable<Color>>().Setup(m => m.Provider).Returns(colors.Provider);
            mockColorTable.As<IQueryable<Color>>().Setup(m => m.Expression).Returns(colors.Expression);
            mockColorTable.As<IQueryable<Color>>().Setup(m => m.ElementType).Returns(colors.ElementType);
            mockColorTable.As<IQueryable<Color>>().Setup(m => m.GetEnumerator()).Returns(colors.GetEnumerator());

            var mockContext = new Mock<LuckysContext>(options);
            mockContext.Setup(c => c.Colors).Returns(mockColorTable.Object);

            // Create the service or repository that uses the context
            var service = new ProductService(mockContext.Object);

            
            var res= service.GetItemAs<Product>(vm);

            // Assert
            Assert.Equal(1, res.ColorProductID); // or whatever assertion makes sense for your test
        }
    }
}