using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WannaWhat.UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        IHostEnvironment _environment;

        public ImagesController(IHostEnvironment environment)
        {
            this._environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Please upload a file");

            string fileName = file.FileName;
            string extension = Path.GetExtension(fileName);
            string[] allowedExtensions = { ".jpg", ".JPG", ".png", ".PNG",".bmp", ".BMP" };

            if (!allowedExtensions.Contains(extension))
                return BadRequest("Please upload a VALID file type");

            string newFilelName = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(_environment.ContentRootPath,"wwwroot","Images",newFilelName);
            //FileStream stream = null;

            using (var fs = new FileStream(filePath,FileMode.Create,FileAccess.Write))
            {
                await file.CopyToAsync(fs);
                //await fs.CopyToAsync(stream);
            }

            return Ok($"Saved successfully here : '{newFilelName}'");

        }

    }
}
