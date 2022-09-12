using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace APIBantrab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenController : ControllerBase
    {
        public static IWebHostEnvironment _enviroment;
        static Dictionary<string, Imagen> imagenes = new Dictionary<string, Imagen>();

        public ImagenController(IWebHostEnvironment enviroment)
        {
            _enviroment = enviroment;
        }

        public class Imagen
        {
            public string nombre { get; set; }
            public IFormFile archivo { get; set; }
        }

        //GET api/imagen
        [HttpGet]
        public IEnumerable<Imagen> Get()
        {
            return new List<Imagen>(imagenes.Values);
        }

        [HttpPost]
        public async Task<string> Post([FromForm] Imagen objArchivo)
        {
            try
            {
                if (objArchivo.archivo.Length > 0)
                {
                    if (!Directory.Exists(_enviroment.WebRootPath + "\\Upload\\"))
                    {
                        Directory.CreateDirectory(_enviroment.WebRootPath + "\\Upload\\");
                    }


                    using (FileStream archivos = System.IO.File.Create(_enviroment.WebRootPath + "\\Upload\\" + objArchivo.archivo.FileName))
                    {
                        objArchivo.archivo.CopyTo(archivos);
                        archivos.Flush();
                        //convierte a base64
                        byte[] imageArray = System.IO.File.ReadAllBytes(_enviroment.WebRootPath + "\\Upload\\" + objArchivo.archivo.FileName);
                        string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                        imagenes.Add(objArchivo.nombre, objArchivo);
                        return "[" + objArchivo.nombre + "," + base64ImageRepresentation + "]";
                    }
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }
    }
}
