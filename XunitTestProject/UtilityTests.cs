using LuckysDepartmentStore;
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
            var defaultImage = "img\\NoImage.jpg";


            var fileInfo = new FileInfo(defaultImage);
            var fileStream = new FileStream(defaultImage, FileMode.Open);

            var formFile = new FormFile(fileStream, 0, fileInfo.Length, fileInfo.Name, fileInfo.Name)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg" // Or whatever the MIME type of the image is
            };

            IFormFile formFileNew = null;

            var util = new Utility();

            byte[] imageBytes = util.ImageBytes(formFileNew, _config);
        }


    }
}
