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
            var login = HttpContext.Session.GetString("login");

            if (!String.IsNullOrEmpty(login) && login.Equals("1")) {
                return Redirect("/");
            } else
            {
                ViewBag.Login = new LoginModel();

                ViewBag.Login.Username = "admin";

                return View("Index");
            }
        }

        [Route("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("login", "0");

            return Redirect("/login");
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
                    HttpContext.Session.SetString("login", "1");

                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("login.Invalid", "Crendenciais inválidas");

                    HttpContext.Session.SetString("login", "0");

                    return this.Index();
                }
            }
            else
            {
                return View("Formulario");
            }
        }
    }
}
