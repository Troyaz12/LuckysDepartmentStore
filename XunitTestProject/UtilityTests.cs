using LuckysDepartmentStore;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XunitTestProject
{
    public class UtilityTests
    {
        private readonly IConfiguration _config;


        public UtilityTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _config = builder.Build();
        }

        [Fact]
        public void ImageConversionTest()
        {
            //IFormFile formFileNew = null;

            //var util = new Utility(_config);

            //var imageBytes = util.ImageBytes(formFileNew);

            //Assert.True(imageBytes.IsSuccess);
        }
    }
}
