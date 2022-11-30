using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace authAuthorization.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController1 : Controller
    {
        public IActionResult Index()
        {
            return View("admin");
        }
    }
}
