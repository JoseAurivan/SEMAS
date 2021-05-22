using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Services.DataStructures;

namespace CadastroHabCMAS.Base
{
    public abstract class CustomControllerBase : Controller
    {

        public IActionResult LidarComErro(ServiceResult resultado, object viewModel, [CallerMemberName] string nomeView = null )
        {
            foreach (var mensagens in resultado.Messages)
            {
                ModelState.AddModelError(Guid.NewGuid().ToString(), mensagens);
            }
            
            return View(nomeView, viewModel);
        }
        
    }
}