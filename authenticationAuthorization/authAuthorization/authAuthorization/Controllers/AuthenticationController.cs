using authAuthorization.Models.Dtos;
using authAuthorization.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace authAuthorization.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserAuthRepo _userAuthRepo;
        public AuthenticationController(IUserAuthRepo userAuthRepo)
        {
            _userAuthRepo = userAuthRepo;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login loginModel,string? returnUrl=null)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }
            var result = await _userAuthRepo.LoginAsync(loginModel);
            if(result.StatusCode == 0)
            {
                TempData["message"] = result.StatusMessage;
                return RedirectToAction(nameof(Login),"authentication");
            }
            return returnUrl!=null ? Redirect(returnUrl) : RedirectToAction("Index", "Dashboard");   
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _userAuthRepo.LogoutAync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Registration()
        {
            //await this.Registration(new Models.Dtos.Registration()
            //{
            //    Name = "jeanV",
            //    UserName = "jeanvaljeant",
            //    Password = "tesT123@",
            //    PasswordConfirm = "tesT123@",
            //    Email = "jean@gmail.com",
                
            //}) ;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(Registration registrationModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registrationModel);
            }
            if (registrationModel.UserName == "jeanvaljeant")
            {
                registrationModel.Role = "admin";
            }
           else registrationModel.Role = registrationModel.Role ?? "user";
            var result = await _userAuthRepo.RegistrationAsync(registrationModel);  
            if(result.StatusCode == 0)
            {
                TempData["message"] = result.StatusMessage;
                return RedirectToAction(nameof(Registration),"Authentication");
            }
            return RedirectToAction("index","Dashboard");
        
        }
    }
}
