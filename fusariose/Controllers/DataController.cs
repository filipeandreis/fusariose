using fusariose.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using fusariose.Filtros;
using fusariose.ClientHttp;

namespace fusariose.Controllers
{
    [AuthFilter]
    public class DataController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            APIHttpClient clienteHTTP = new();

            var dataResponse = clienteHTTP.Get<DataModel>("data/getrisk");

            List<DataModel> data = JsonConvert.DeserializeObject<List<DataModel>>(dataResponse);

            return View(data);
        }

        [Route("/send-data")]
        public IActionResult Formulario()
        {
            ViewBag.Data = new DataModel();

            return View();
        }

        [Route("/produto/cadastro/{id:int}")]
       
        public IActionResult Cadastro(int id)
        {
            return View("formulario");
        }

        [Route("/produto/cadastro/{id:alpha}")]
        public IActionResult Cadastro(string id)
        {
            return View("formulario");
        }

        [HttpPost]
        public IActionResult Cadastrar(DataModel data)
        {
            if (ModelState.IsValid)
            {
                string dataList = HttpContext.Session.GetString("dataList");

                if(dataList != null)
                {
                    List<DataModel> info = JsonConvert.DeserializeObject<List<DataModel>>(dataList);

                    info.Add(data);

                    dataList = JsonConvert.SerializeObject(info);
                    HttpContext.Session.SetString("dataList", dataList);

                    return RedirectToAction("Index");
                }
                else
                {
                    List<DataModel> info = new();

                    info.Add(data);

                    dataList = JsonConvert.SerializeObject(info);
                    HttpContext.Session.SetString("dataList", dataList);

                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View("Formulario");
            }
        }
    }
}
