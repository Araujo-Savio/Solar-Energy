using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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

        public IActionResult Index()
        {
        [Authorize]
        public IActionResult Leads()
        {
            return View();
        }

        [Authorize]
        public IActionResult AdminDashboard()
        {
            return View();
        }

        [Authorize]
        public IActionResult Simulation()
        {
            return View();
        }

        [Authorize]
        public IActionResult Evaluations()
        {
            return View();
        }

        [Authorize]
        public IActionResult CompanyPanel()
        {
            return View();
        }

        [Authorize]
        public IActionResult UserDashboard()
        {
            return View();
        }

            // Se o usuário está logado, redireciona para o dashboard apropriado
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Dashboard");
            }
            
            return View();
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            // Aqui você pode adicionar lógica para determinar qual dashboard mostrar
            // baseado no tipo de usuário (Cliente, Empresa, Administrador)
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
