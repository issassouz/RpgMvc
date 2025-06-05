using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RpgMvc.Controllers
{
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        public string uriBase ="https://luizsilva.somee.com/rpgapi/usuarios";

        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(ILogger<UsuariosController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

         [HttpGet]
         public IActionResult IndexLogin()
        
        {
            return View("AutentificarUsuario");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}