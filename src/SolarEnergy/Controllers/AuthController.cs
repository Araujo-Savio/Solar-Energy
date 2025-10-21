using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolarEnergy.Models;

namespace SolarEnergy.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Email, 
                model.Password, 
                model.RememberMe, 
                lockoutOnFailure: true);

            if (result.Succeeded)
            {
                _logger.LogInformation("User {Email} logged in successfully.", model.Email);
                return RedirectToLocal(returnUrl);
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account {Email} locked out.", model.Email);
                ModelState.AddModelError(string.Empty, "Sua conta foi temporariamente bloqueada devido a muitas tentativas de login. Tente novamente em alguns minutos.");
                return View(model);
            }

            if (result.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, "Login n�o permitido. Verifique se sua conta foi confirmada.");
                return View(model);
            }

            ModelState.AddModelError(string.Empty, "Email ou senha incorretos. Verifique seus dados e tente novamente.");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Custom validation based on user type
            if (model.UserType == UserType.Company)
            {
                if (string.IsNullOrEmpty(model.CNPJ))
                {
                    ModelState.AddModelError("CNPJ", "CNPJ � obrigat�rio para empresas.");
                    return View(model);
                }
                
                // Check if CNPJ already exists
                var existingUserWithCNPJ = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.CNPJ == model.CNPJ);
                if (existingUserWithCNPJ != null)
                {
                    ModelState.AddModelError("CNPJ", "Este CNPJ j� est� cadastrado em nosso sistema.");
                    return View(model);
                }
                
                // Clear CPF if filled by mistake
                model.CPF = null;
            }
            else
            {
                if (string.IsNullOrEmpty(model.CPF))
                {
                    ModelState.AddModelError("CPF", "CPF � obrigat�rio para consumidores.");
                    return View(model);
                }
                
                // Check if CPF already exists
                var existingUserWithCPF = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.CPF == model.CPF);
                if (existingUserWithCPF != null)
                {
                    ModelState.AddModelError("CPF", "Este CPF j� est� cadastrado em nosso sistema.");
                    return View(model);
                }
                
                // Clear CNPJ if filled by mistake
                model.CNPJ = null;
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                CPF = model.CPF,
                CNPJ = model.CNPJ,
                Phone = model.Phone,
                PhoneNumber = model.Phone,
                UserType = model.UserType,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User {Email} created successfully.", model.Email);
                
                // Add role based on user type
                string roleName = model.UserType switch
                {
                    UserType.Company => "Company",
                    UserType.Administrator => "Administrator",
                    _ => "Client"
                };

                // Check if role exists before adding
                if (await _roleManager.RoleExistsAsync(roleName))
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }

                // Show success message
                TempData["SuccessMessage"] = "Conta criada com sucesso! Bem-vindo � Solar Energy.";
                
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            // Translate Identity errors to more friendly messages
            foreach (var error in result.Errors)
            {
                string friendlyMessage = error.Code switch
                {
                    "DuplicateEmail" => "Este email j� est� cadastrado. Tente fazer login ou use outro email.",
                    "InvalidEmail" => "Por favor, insira um email v�lido.",
                    "PasswordTooShort" => "A senha deve ter pelo menos 8 caracteres.",
                    "PasswordRequiresDigit" => "A senha deve conter pelo menos um n�mero.",
                    "PasswordRequiresLower" => "A senha deve conter pelo menos uma letra min�scula.",
                    "PasswordRequiresUpper" => "A senha deve conter pelo menos uma letra mai�scula.",
                    "PasswordRequiresNonAlphanumeric" => "A senha deve conter pelo menos um caractere especial.",
                    _ => error.Description
                };
                
                ModelState.AddModelError(string.Empty, friendlyMessage);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            TempData["InfoMessage"] = "Voc� foi desconectado com sucesso.";
            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}