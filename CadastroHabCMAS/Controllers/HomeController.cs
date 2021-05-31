using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Enums;
using Application.Services.Interfaces;
using CadastroHabCMAS.Base;
using Microsoft.AspNetCore.Mvc;
using CadastroHabCMAS.Models;
using CadastroHabCMAS.ViewModel.UserViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace CadastroHabCMAS.Controllers
{
    public class HomeController : CustomControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        public HomeController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }

            return View();
        }

        public IActionResult CriarUser()
        {
            return View();
        }

        public IActionResult Esqueci()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Esqueci(UserEsqueciViewModel userEsqueciViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Email nao encontrado");
                return View(userEsqueciViewModel);
            }

            var result = await _emailService.SendForgottenPasswordAsync(userEsqueciViewModel.Email);
            if (result.Type != ServiceResultType.Success) return LidarComErro(result, userEsqueciViewModel);
            return View(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> CriarUser(UserCreateViewModel userCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(CriarUser), userCreateViewModel);
            }

            var user = userCreateViewModel.GetModel(userCreateViewModel.Cpf, userCreateViewModel.Matricula,
                userCreateViewModel.Senha, userCreateViewModel.Email);
            await _userService.Save(user);
            return View("CriarUser");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index), userLoginViewModel);
            }

            var result = await _userService.LoginAsync(userLoginViewModel.Username, userLoginViewModel.Password);
            if (result.Type != ServiceResultType.Success) return LidarComErro(result, userLoginViewModel, nameof(Index));

            //TODO remodelar esse sistema

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userLoginViewModel.Username));
            claims.Add(new Claim(ClaimTypes.Sid, userLoginViewModel.Password));
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);
            return View(nameof(Panel));
        }

        [Authorize]
        public IActionResult Panel()
        {
            return View();
        }

        public IActionResult Contato()
        {
            return View();
        }

        [Authorize]
        public IActionResult MyPage()
        {
            UserLoginViewModel userLoginViewModel = new UserLoginViewModel();
            var identity = (ClaimsIdentity) User.Identity;
            if (identity != null)
            {
                var name = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                    .SingleOrDefault();
                var password = identity.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value)
                    .SingleOrDefault();

                userLoginViewModel.Username = name;
                userLoginViewModel.Password = password;
            }


            return View(userLoginViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}