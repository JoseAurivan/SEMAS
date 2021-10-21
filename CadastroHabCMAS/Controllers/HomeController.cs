﻿using System.Collections.Generic;
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
using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Services.DataStructures;

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

            var user = userCreateViewModel.GetModel(userCreateViewModel.Cpf, userCreateViewModel.Nome,
                userCreateViewModel.Senha, userCreateViewModel.Email);
            var result = await _userService.Save(user);
            
            if(result.Type != ServiceResultType.Success) LidarComErro(result,userCreateViewModel);
            
            return View(nameof(Index));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index), userLoginViewModel);
            }

            var result = await _userService.LoginAsync(userLoginViewModel.Username, userLoginViewModel.Password);
            if (result.Type != ServiceResultType.Success)
                return LidarComErro(result, userLoginViewModel, nameof(Index));

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
        public async Task<IActionResult> MyPage()
        {
            UserMyPageViewModel userMyPageViewModel = new UserMyPageViewModel();
            var identity = (ClaimsIdentity) User.Identity;
            if (identity != null)
            {
                var name = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                    .SingleOrDefault();

                var result = await _userService.FindCpfAsync(name);
                if (result is ServiceResult<User> resultado && resultado.Type == ServiceResultType.Success)
                {
                    var user = resultado.Result;
                    userMyPageViewModel.Cpf = user.Username;
                    userMyPageViewModel.Senha = user.Password;
                    userMyPageViewModel.Matricula = user.Nome;
                    userMyPageViewModel.Email = user.Email;
                    userMyPageViewModel.Id = user.Id;
                }
            }


            return View(userMyPageViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        [Authorize]
        public async Task<IActionResult> MudarSenha()
        {
            UserEditPasswordViewModel userEditPasswordViewModel = new UserEditPasswordViewModel();
            var identity = (ClaimsIdentity) User.Identity;
            if (identity != null)
            {
                var name = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                    .SingleOrDefault();

                var result = await _userService.FindCpfAsync(name);
                if (result is ServiceResult<User> resultado && resultado.Type == ServiceResultType.Success)
                {
                    var user = resultado.Result;
                    userEditPasswordViewModel.Id = user.Id;
                }
            }

            return View(userEditPasswordViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> MudarSenha(UserEditPasswordViewModel userEditPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userService.CheckOldPasswordAsync(userEditPasswordViewModel.OldPassword,
                userEditPasswordViewModel.Id);
            if (result.Type == ServiceResultType.Success)
            {
                var task = await _userService.FindUserAsync(userEditPasswordViewModel.Id);
                if (task is ServiceResult<User> resultado && resultado.Type == ServiceResultType.Success)
                {
                    var user = resultado.Result;
                    user.Password = userEditPasswordViewModel.NewPassword;
                    await _userService.Save(user);
                    //TODO retornar feedback de sucesso
                    return View(nameof(Panel));
                }
            }
            
            
            //TODO tratar erros com mensagens amigáveis
            return LidarComErro(result, userEditPasswordViewModel, nameof(MudarSenha));
        }

        [Authorize]
        public async Task<IActionResult> MudarEmail()
        {
            UserEditEmailViewModel userEditEmailViewModel = new UserEditEmailViewModel();
            var identity = (ClaimsIdentity) User.Identity;
            if (identity != null)
            {
                var name = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                    .SingleOrDefault();

                var result = await _userService.FindCpfAsync(name);
                if (result is ServiceResult<User> resultado && resultado.Type == ServiceResultType.Success)
                {
                    var user = resultado.Result;
                    userEditEmailViewModel.Id = user.Id;
                }
            }

            return View(userEditEmailViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> MudarEmail(UserEditEmailViewModel userEditEmailViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            var task = await _userService.FindUserAsync(userEditEmailViewModel.Id);
            if (task is ServiceResult<User> resultado && resultado.Type == ServiceResultType.Success)
            {
                var user = resultado.Result;
                user.Email = userEditEmailViewModel.NewEmail;
                await _userService.Save(user);
                //TODO retornar feedback de sucesso
                return View(nameof(Panel));
            }

            //TODO tratar erros com mensagens amigáveis
            return LidarComErro(task, userEditEmailViewModel, nameof(MudarEmail));
        }
    }
}