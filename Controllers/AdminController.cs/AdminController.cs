using Microsoft.AspNetCore.Mvc;

namespace AdminDashboardApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
