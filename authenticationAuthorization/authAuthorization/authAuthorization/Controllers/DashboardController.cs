using Microsoft.AspNetCore.Mvc;

namespace authAuthorization.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View("dashboard");
        }
    }
}
