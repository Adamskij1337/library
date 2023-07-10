using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Models.Domain;
using Library.Services;

namespace Library.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (await _registrationService.RegisterUser(user))
                {
                    // Rejestracja udana
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Użytkownik o podanej nazwie już istnieje
                    ViewBag.ErrorMessage = "Użytkownik o podanej nazwie już istnieje";
                }
            }

            return View("Registration", user);
        }
    }
}
