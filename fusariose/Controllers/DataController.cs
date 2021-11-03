using fusariose.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using fusariose.Filtros;

namespace fusariose.Controllers
{
    [AuthFilter]
    public class DataController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<DataModel> data = new();

            string dataList = HttpContext.Session.GetString("dataList");
            if (string.IsNullOrEmpty(dataList))
            {
                data = MockFactory.MockFactory.GerarListaDados();
            }
            else
            {
                data = JsonConvert.DeserializeObject<List<DataModel>>(dataList);
            }
            
            dataList = JsonConvert.SerializeObject(data);
            HttpContext.Session.SetString("dataList", dataList);
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
