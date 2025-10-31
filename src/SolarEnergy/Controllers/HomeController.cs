using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarEnergy.Data;
using SolarEnergy.Models;
using SolarEnergy.ViewModels;

namespace SolarEnergy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(
            ILogger<HomeController> logger,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        // Páginas públicas
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            // Se o usuário está logado, redireciona para o dashboard apropriado
            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    return user.UserType switch
                    {
                        UserType.Company => RedirectToAction(nameof(CompanyRedirect)),
                        UserType.Administrator => RedirectToAction(nameof(AdminDashboard)),
                        _ => RedirectToAction(nameof(SearchCompanies))
                    };
                }

                return RedirectToAction(nameof(Dashboard));
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
        public IActionResult Quotes() => View();

        [Authorize]
        public IActionResult CompanyPanel() => View();

        [Authorize]
        public IActionResult UserDashboard() => View();

        [Authorize]
        public async Task<IActionResult> SearchCompanies(string? term)
        {
            var searchTerm = term?.Trim();

            var query = _context.Users
                .AsNoTracking()
                .Where(u => u.UserType == UserType.Company && u.IsActive);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(u =>
                    (u.CompanyTradeName != null && EF.Functions.Like(u.CompanyTradeName, $"%{searchTerm}%")) ||
                    (u.CompanyLegalName != null && EF.Functions.Like(u.CompanyLegalName, $"%{searchTerm}%")) ||
                    (u.Location != null && EF.Functions.Like(u.Location, $"%{searchTerm}%")));
            }

            var companies = await query
                .OrderBy(u => u.CompanyTradeName ?? u.CompanyLegalName ?? u.FullName)
                .Select(u => new CompanySummaryViewModel
                {
                    Id = u.Id,
                    Name = u.CompanyTradeName ?? u.CompanyLegalName ?? u.FullName,
                    LegalName = u.CompanyLegalName,
                    TradeName = u.CompanyTradeName,
                    Location = u.Location,
                    Phone = u.CompanyPhone ?? u.PhoneNumber,
                    Website = u.CompanyWebsite,
                    Description = u.CompanyDescription
                })
                .ToListAsync();

            var model = new CompanySearchViewModel
            {
                SearchTerm = searchTerm,
                Companies = companies
            };

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> CompanyRedirect()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (user.UserType != UserType.Company)
            {
                return RedirectToAction(nameof(SearchCompanies));
            }

            ViewData["Title"] = "Central da Empresa";
            ViewBag.CompanyName = user.CompanyTradeName ?? user.CompanyLegalName ?? user.FullName;
            return View();
        }
    }
}
