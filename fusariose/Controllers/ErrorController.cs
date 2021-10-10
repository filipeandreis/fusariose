using Microsoft.AspNetCore.Mvc;

namespace fusariose.Controllers
{
    public class ErrorController : Controller
    {
        // GET: ErrorController
        public ActionResult Error404()
        {
            return View("Error404");
        }
    }
}
