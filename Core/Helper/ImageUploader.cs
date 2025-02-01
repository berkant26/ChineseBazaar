using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
namespace Core.Helper
{
    public static class ImageUploader
    {
        private static IHostEnvironment _env;

        public static void Initialize(IHostEnvironment env)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public static string UploadImage(IFormFile imageFile)
        {
            if (_env == null)
            {
                throw new InvalidOperationException("ImageUploader has not been initialized. Call Initialize() first.");
            }

            try
            {
                if (imageFile == null || imageFile.Length == 0)
                {
                    throw new ArgumentException("The image file is null or empty.");
                }

                // **WebRootPath yoksa manuel olarak wwwroot ayarla**
                var webRootPath = Path.Combine(_env.ContentRootPath, "wwwroot");
                var imageDirectory = Path.Combine(webRootPath, "images");

                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                var filePath = Path.Combine(imageDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                return $"/images/{fileName}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Image upload failed: {ex.Message}");
                return string.Empty;
            }
        }
    }
}