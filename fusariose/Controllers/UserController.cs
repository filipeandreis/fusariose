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
        [Route("/admin/users")]
        public IActionResult Index()
        {
            List<UserModel> data = new();

            string dataList = HttpContext.Session.GetString("users");
            if (string.IsNullOrEmpty(dataList))
            {
                data = MockFactory.MockFactory.CreateAdminUser();
            }
            else
            {
                data = JsonConvert.DeserializeObject<List<UserModel>>(dataList);
            }

            dataList = JsonConvert.SerializeObject(data);
            HttpContext.Session.SetString("users", dataList);

            return View(data);
        }

        [Route("/admin/new-user")]
        public IActionResult Store()
        {
            ViewBag.User = new UserModel();

            return View("Formulario");
        }

        [HttpPost]
        public bool Search(string user)
        {
            string users = HttpContext.Session.GetString("users");

            List<UserModel> usersList = JsonConvert.DeserializeObject<List<UserModel>>(users);

            if (string.IsNullOrEmpty(user))
            {
                return false;
            }
            else
            {
                var produto = usersList.Where(p => p.Username.Equals(user)).ToList<UserModel>();

                if (produto.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
                string users = HttpContext.Session.GetString("users");

                if (users != null)
                {
                    List<UserModel> info = JsonConvert.DeserializeObject<List<UserModel>>(users);

                    info.Add(user);

                    users = JsonConvert.SerializeObject(info);
                    HttpContext.Session.SetString("users", users);

                    return RedirectToAction("Index");
                }
                else
                {
                    List<UserModel> info = new();

                    info.Add(user);

                    users = JsonConvert.SerializeObject(info);
                    HttpContext.Session.SetString("users", users);

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
