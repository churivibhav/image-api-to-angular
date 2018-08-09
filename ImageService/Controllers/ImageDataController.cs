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
    public class ImageDataController : ControllerBase
    {
        private readonly IHostingEnvironment _env;

        public ImageDataController(IHostingEnvironment env)
        {
            _env = env;
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            Guid guid = Guid.Parse(id);
            using (var data = new DataService()) {
                data.Initialize("images.db");
            Image image = data.GetImage(guid);
            return $"data:{image.Filetype};base64," + Convert.ToBase64String(image.Data);
            }
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<Guid>> Post()
        {
            try
            {
                var file = Request.Form.Files[0];
                
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        byte [] buffer = stream.ToArray();
                        using (var data = new DataService()){
                            data.Initialize("images.db");
                            var image = new Image {
                                Filename = file.FileName,
                                Filetype = file.ContentType,
                                Data = buffer
                            };
                            return data.CreateImage(image);
                        }
                    }
                }
                else {
                    return new JsonResult("Empty file!");
                }
            }
            catch (Exception ex)
            {
                return new JsonResult("Upload failed : " + ex.Message);
            }
        }
    }
}