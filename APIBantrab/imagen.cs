using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIBantrab
{
    public class imagen
    {
        public string Nombre { get; set; }
        public IFormFile Archivo { get; set; }
    }
}
