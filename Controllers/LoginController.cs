using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.Models.Domain;
using Library.Services;

namespace Library.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly ISession _session;

        public LoginController(ILoginService loginService, IHttpContextAccessor httpContextAccessor)
        {
            _loginService = loginService;
            _session = httpContextAccessor.HttpContext.Session;
        }

        [HttpGet]
        [Route("/login/login")] // Ścieżka do akcji Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("/login/login")]
        public IActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                if (_loginService.LoginUser(login))
                {
                    // Użytkownik został pomyślnie zalogowany
                    _session.SetString("UserName", login.UserName); // Ustawienie wartości sesji

                    return RedirectToAction("index", "home");
                }
                else
                {
                    
                    // Nieprawidłowe dane logowania lub użytkownik nie istnieje
                    ModelState.AddModelError("", "Nieprawidłowe dane logowania");
                }
            }

            // Przekazanie modelu logowania do widoku
            return View(login);
        }

        public IActionResult Logout()
        {
            _session.Clear(); // Wyczyszczenie sesji

            return RedirectToAction("Index", "Home");
        }
    }
}
