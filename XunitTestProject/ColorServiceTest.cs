using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XunitTestProject
{
    public class ColorServiceTest
    {
        [Fact]
        public void GetColor()
        {
            ProductVM productVM = new ProductVM();
            
            productVM.ColorSelection = "Purple";
            var mapper = new Mock<IMapper>();          

            var options = new DbContextOptionsBuilder<LuckysContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            var mockContext = new Mock<LuckysContext>(options);


            ColorService colorService = new ColorService(mockContext.Object, mapper.Object);

            var colorServ = colorService.Create("Violet");

            var colorsList = mockContext.Object.Colors.ToList();

            Assert.Equal(1, colorsList.Count);
        }


    }
}
