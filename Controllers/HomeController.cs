using Microsoft.AspNetCore.Mvc;

namespace HealthTrack.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
