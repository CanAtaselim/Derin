using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Derin.Web.Attributes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Derin.Web.Areas.Admin.Controllers
{
    [Area("Administration")]
    public class FileController : BaseController
    {
        private IHostingEnvironment _env;

        public FileController(IHostingEnvironment env)
        {
            _env = env;
        }
        /// <summary>
        /// Image dosyalarını ekleme işlemini gerçekleştiren fonksiyondur.
        /// </summary>
        /// <param name="file"> Dosya bilgisini alan parametredir.</param>
        /// <returns></returns>
        [Authorize]
        public async Task<string> AddImageFile(IFormFile file)
        {
            var uploads = Path.Combine(_env.WebRootPath, "uploads/images");
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            return fileName;
        }
        public FileResult GetImageFilePath(string fileName, string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    var uploads2 = Path.Combine(_env.WebRootPath, "images");
                    byte[] fileBytes2 = System.IO.File.ReadAllBytes(Path.Combine(uploads2, "no-image1.png"));
                    return File(fileBytes2, "image/jpeg");
                }

                var uploads = Path.Combine(filePath);
                byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(uploads, fileName));
                return File(fileBytes, "image/jpeg");
            }
            catch (Exception ex)
            {
                var uploads = Path.Combine(_env.WebRootPath, "images");

                byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(uploads, "no-image1.png"));
                return File(fileBytes, "image/jpeg");
            }

        }
    }
}