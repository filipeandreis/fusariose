using fusariose.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fusariose.Controllers
{
    public class LoginController : Controller
    {
        [Route("/login")]
        public IActionResult Index()
        {
            ViewBag.Login = new LoginModel();

            return View();
        }

        [HttpPost]
        public IActionResult Authenticate(LoginModel login)
        {
            if (login.Username.Equals(""))
            {
                ModelState.AddModelError("login.Username", "Digite o usuário");
            }
            if (login.Password.Equals(""))
            {
                ModelState.AddModelError("login.Password", "Digite a senha");
            }

            if (ModelState.IsValid)
            {
                if(login.Password.Equals("admin") && login.Username.Equals("admin"))
                {
                    HttpContext.Session.SetString("login", "true");

                    return RedirectToRoute("/");
                }
                else
                {
                    ModelState.AddModelError("login.Invalid", "Digite a senha");

                    return View();
                }
            }
            else
            {
                return View("Formulario");
            }
        }
    }
}
