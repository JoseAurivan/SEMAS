using System.ComponentModel.DataAnnotations;

namespace CadastroHabCMAS.ViewModel.UserViewModel
{
    public class UserEditPasswordViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Senha Antiga é obrigatório")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Campo Nova Senha é obrigatório"), Compare(nameof(NewPasswordVerify),ErrorMessage = "Senhas não são iguais")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Campo Verificar Nova Senha é obrigatório")]
        public string NewPasswordVerify { get; set; }
    }
}