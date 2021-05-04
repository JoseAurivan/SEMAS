using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CadastroHabCMAS.Models;
using CadastroHabCMAS.ViewModel.UserViewModel;
using Microsoft.AspNetCore.Authorization;
using Services.DataStructures;
using Services.Repositories;
using Services.Services.Interfaces;

namespace CadastroHabCMAS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUserService userService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CriarUser()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CriarUser(UserCreateViewModel userCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(CriarUser), userCreateViewModel);
            }
            var user = userCreateViewModel.GetModel(userCreateViewModel.Cpf, userCreateViewModel.Matricula,
                userCreateViewModel.Senha);
            await _userService.Save(user);
            await _unitOfWork.SaveChangesAsync();
            return View("CriarUser");
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index), userLoginViewModel);
            }
            var result = await _userService.LoginAsync(userLoginViewModel.Username, userLoginViewModel.Password);
            if(result.IsSucessfull)
                return View("Panel");
            return BadRequest();
        }
        
        public IActionResult Panel()
        {
            return View();
        }

        public IActionResult Contato()
        {
            return View();
        }
        
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
    }
}