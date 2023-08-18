using Microsoft.AspNetCore.Mvc;

namespace InternshipProject_2.Controllers
{
    public class ControllerTesting : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
