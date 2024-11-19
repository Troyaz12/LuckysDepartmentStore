using LuckysDepartmentStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.IO.Compression;

namespace LuckysDepartmentStore
{
    public class Utility
    {        
        public IFormFile FileUpload { get; private set; }
        public IConfiguration Config { get; private set; }   

        public byte[] ImageBytes(IFormFile? fileImport, IConfiguration config)
        {
            byte[]? imageBytes = null;
            var defaultImage = config["ImagePaths:ShoppingImageEmpty"];

            using (var memoryStream = new MemoryStream())
            {
                if (fileImport != null)
                {
                    //await file.form FormFile.CopyToAsync(memoryStream);
                    fileImport.CopyTo(memoryStream);
                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        imageBytes = memoryStream.ToArray();
                    }
                    else
                    {

                    }
                }
                else
                {
                    var fileInfo = new FileInfo(defaultImage);
                    var fileStream = new FileStream(defaultImage, FileMode.Open);

                    var formFile = new FormFile(fileStream, 0, fileInfo.Length, fileInfo.Name, fileInfo.Name)
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = "image/jpeg" // Or whatever the MIME type of the image is
                    };

                    //await file.form FormFile.CopyToAsync(memoryStream);
                    fileImport.CopyTo(memoryStream);
                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        imageBytes = memoryStream.ToArray();
                    }
                    else
                    {

                    }
                }
            }

            return imageBytes;
        }
    }
    public class AppFile
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
    }
}
