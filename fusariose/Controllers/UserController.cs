using fusariose.ClientHttp;
using fusariose.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fusariose.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        [Route("/admin/users")]
        public IActionResult Index()
        {
            APIHttpClient clienteHTTP = new();

            var users = clienteHTTP.Get<UserModel>("User/GetAll");

            var data = JsonConvert.DeserializeObject<List<UserModel>>(users);

            return View(data);
        }

        [HttpGet]
        [Route("/admin/new-user")]
        public IActionResult Store()
        {
            ViewBag.User = new UserModel();
            
            return View("Formulario");
        }

        [HttpPost]
        public bool Search(string user)
        {
            APIHttpClient clienteHTTP = new();

            var userExist = clienteHTTP.Get<UserModel>("User/Get/" + user);

            var data = JsonConvert.DeserializeObject<UserModel>(userExist);

            if (String.IsNullOrEmpty(data.Username))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(UserModel user)
        {
            if (String.IsNullOrEmpty(user.Username))
            {
                ModelState.AddModelError("user.Username", "Insira um usuário");
            }
            if (String.IsNullOrEmpty(user.Password))
            {
                ModelState.AddModelError("user.Password", "Insira uma senha");
            }

            if (ModelState.IsValid)
            {
                APIHttpClient clienteHTTP = new();

                clienteHTTP.Post<UserModel>("User/Store", user);

                return RedirectToAction("Index");
            }
            else
            {
                return View("Formulario");
            }
        }
    }
}
