using Microsoft.AspNetCore.Mvc;

namespace csproject.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        public ActionResult Index()
        {
            return View();
        }

    }
}
