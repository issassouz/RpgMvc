using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using RpgMvc.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace RpgMvc.Controllers
{
    [Route("[controller]")]
    public class PersonagensController : Controller
    {
        public string uriBase = "https://luizsilva.somee.com/rpgapi/personagens/";
        private readonly ILogger<PersonagensController> _logger;

        public PersonagensController(ILogger<PersonagensController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                string uriComplementar = "GetAll";
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<PersonagemViewModel> listaPersonagens = await Task.Run(() =>
                        JsonConvert.DeserializeObject<List<PersonagemViewModel>>(serialized));

                    return View(listaPersonagens);
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Index");
            }
        }
   
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }  
}
