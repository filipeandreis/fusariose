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
                if(CheckAuth(login.Username, login.Password))
                {
                    HttpContext.Session.SetString("login", "1");

                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("login.Invalid", "Crendenciais inválidas");

                    HttpContext.Session.SetString("login", "0");

                    return Index();
                }
            }
            else
            {
                return View("Formulario");
            }
        }
        private bool CheckAuth(string user, string password)
        {
            string users = HttpContext.Session.GetString("users");

            if (String.IsNullOrEmpty(users))
            {
                if (user.Equals("admin") && password.Equals("admin"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                List<UserModel> usersList = JsonConvert.DeserializeObject<List<UserModel>>(users);

                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return false;
                }
                else
                {
                    var auth = usersList.Where(p => p.Username.Equals(user)).FirstOrDefault();

                    if (!String.IsNullOrEmpty(auth.ToString()) && auth.Password.Equals(password))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
