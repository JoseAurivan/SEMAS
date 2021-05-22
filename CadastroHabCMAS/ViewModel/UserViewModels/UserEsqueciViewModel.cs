using System.ComponentModel.DataAnnotations;

namespace CadastroHabCMAS.ViewModel.UserViewModel
{
    public class UserEsqueciViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}