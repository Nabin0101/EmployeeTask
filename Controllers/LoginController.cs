using Microsoft.AspNetCore.Mvc;

namespace EmployeeTask.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult LoginForm()
        {
            return View();
        }

        public IActionResult SignupForm()
        {
            return View();
        }

    }
}
