using System.ComponentModel.DataAnnotations;

namespace CadastroHabCMAS.ViewModel.LoginTi
{
    public class LoginTiViewModel
    {
        public readonly string Username = "PortoTI";
        public readonly string Password = "$$admP2020G7";
        
        [Required]
        public string TypedUser { get; set; }
        [Required]
        public string TypedPassword { get; set; }
    }
}