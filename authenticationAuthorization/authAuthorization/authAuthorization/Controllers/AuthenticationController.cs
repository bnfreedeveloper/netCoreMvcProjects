using Microsoft.AspNetCore.Mvc;

namespace authAuthorization.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View("login");
        }
    }
}
