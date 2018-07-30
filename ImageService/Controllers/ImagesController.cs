using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ImageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IHostingEnvironment _env;

        public ImagesController(IHostingEnvironment env)
        {
            _env = env;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<string>> Get(string name)
        {
            string path = _env.WebRootPath + "/images/" + name;
            byte[] b = await System.IO.File.ReadAllBytesAsync(path);
            return "data:image/png;base64," + Convert.ToBase64String(b);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post()
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = "images";
                string path = Path.Combine(_env.WebRootPath, folderName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string filePath = Path.Combine(path, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                return new JsonResult("Upload successful.");
            }
            catch (Exception ex)
            {
                return new JsonResult("Upload failed : " + ex.Message);
            }
        }
    }
}