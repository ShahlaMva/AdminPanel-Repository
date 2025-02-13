
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminPanel.Controllers
{
    public class DashboardController : Controller
    {
        public  IActionResult Index()
        {
            return View();
        }
    }
}
