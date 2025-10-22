using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SolarEnergy.Models;
using SolarEnergy.ViewModels;

namespace SolarEnergy.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment,
            ILogger<ProfileController> logger)
        {
            _userManager = userManager;
            _environment = environment;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null)
            {
                _logger.LogWarning("Authenticated user not found while accessing profile page.");
                return RedirectToAction("Login", "Auth");
            }

            var viewModel = CreateViewModelFromUser(user);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UserProfileViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null)
            {
                _logger.LogWarning("Authenticated user not found while updating profile.");
                TempData["ErrorMessage"] = "Não foi possível localizar o usuário logado.";
                return RedirectToAction("Login", "Auth");
            }

            model.Email = model.Email?.Trim() ?? string.Empty;
            model.Phone = string.IsNullOrWhiteSpace(model.Phone) ? null : model.Phone.Trim();
            model.Location = string.IsNullOrWhiteSpace(model.Location) ? null : model.Location.Trim();

            ModelState.Clear();
            if (!TryValidateModel(model))
            {
                PopulateReadOnlyFields(model, user);
                return View("Index", model);
            }

            if (!string.Equals(user.Email, model.Email, StringComparison.OrdinalIgnoreCase))
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    AddErrors(setEmailResult);
                    PopulateReadOnlyFields(model, user);
                    return View("Index", model);
                }

                var setUserNameResult = await _userManager.SetUserNameAsync(user, model.Email);
                if (!setUserNameResult.Succeeded)
                {
                    AddErrors(setUserNameResult);
                    PopulateReadOnlyFields(model, user);
                    return View("Index", model);
                }
            }

            user.PhoneNumber = model.Phone;
            user.Phone = model.Phone;
            user.Location = model.Location;

            if (model.ProfileImageFile is not null && model.ProfileImageFile.Length > 0)
            {
                if (!model.ProfileImageFile.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError(nameof(model.ProfileImageFile), "Selecione um arquivo de imagem válido.");
                    PopulateReadOnlyFields(model, user);
                    return View("Index", model);
                }

                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "profiles");
                Directory.CreateDirectory(uploadsFolder);

                var extension = Path.GetExtension(model.ProfileImageFile.FileName);
                var fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                await using (var stream = System.IO.File.Create(filePath))
                {
                    await model.ProfileImageFile.CopyToAsync(stream);
                }

                if (!string.IsNullOrWhiteSpace(user.ProfileImagePath) &&
                    !string.Equals(user.ProfileImagePath, "/images/default-profile.svg", StringComparison.OrdinalIgnoreCase))
                {
                    var existingPath = Path.Combine(_environment.WebRootPath, user.ProfileImagePath.TrimStart('/')
                        .Replace('/', Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(existingPath))
                    {
                        System.IO.File.Delete(existingPath);
                    }
                }

                user.ProfileImagePath = $"/uploads/profiles/{fileName}";
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                AddErrors(updateResult);
                PopulateReadOnlyFields(model, user);
                model.ProfileImagePath = user.ProfileImagePath;
                return View("Index", model);
            }

            TempData["SuccessMessage"] = "Perfil atualizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private static void PopulateReadOnlyFields(UserProfileViewModel model, ApplicationUser user)
        {
            model.Id = user.Id;
            model.FullName = user.FullName;
            model.UserType = user.UserType;
            model.IsActive = user.IsActive;
            model.CreatedAt = user.CreatedAt;
            model.DateOfBirth = user.DateOfBirth;
            model.CPF = user.CPF;
            model.CNPJ = user.CNPJ;
            model.ProfileImagePath = user.ProfileImagePath;
        }

        private static UserProfileViewModel CreateViewModelFromUser(ApplicationUser user) => new()
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email ?? string.Empty,
            Phone = user.Phone,
            Location = user.Location,
            UserType = user.UserType,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt,
            DateOfBirth = user.DateOfBirth,
            CPF = user.CPF,
            CNPJ = user.CNPJ,
            ProfileImagePath = user.ProfileImagePath
        };

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
