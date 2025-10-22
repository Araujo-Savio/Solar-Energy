using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using SolarEnergy.Models;

namespace SolarEnergy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Páginas públicas
        [AllowAnonymous]
        public IActionResult Index()
        {
            // Se o usuário está logado, redireciona para o dashboard apropriado
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult About() => View();

        [AllowAnonymous]
        public IActionResult Contact() => View();

        [AllowAnonymous]
        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Ações protegidas (exigem login)
        [Authorize]
        public IActionResult Dashboard() => View();

        [Authorize]
        public IActionResult Leads() => View();

        [Authorize]
        public IActionResult AdminDashboard() => View();

        [Authorize]
        public IActionResult Simulation() => View();

        [Authorize]
        public IActionResult Evaluations() => View();

        [Authorize]
        public IActionResult CompanyPanel() => View();

        [Authorize]
        public IActionResult UserDashboard() => View();
    }
}
