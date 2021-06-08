using System.ComponentModel.DataAnnotations;

namespace CadastroHabCMAS.ViewModel.UserViewModel
{
    public class UserEditEmailViewModel
    {
        [Required]
        public int Id { get; set; }
        [EmailAddress(ErrorMessage = "Email inserido não é valido")]
        [Required(ErrorMessage = "Campo Novo Email é obrigatório"), Compare(nameof(NewEmailVerify),ErrorMessage = "Emails não são iguais")]
        public string NewEmail { get; set; }
        [EmailAddress(ErrorMessage = "Email inserido não é valido")]
        [Required(ErrorMessage = "Campo Verificar Novo Email é obrigatório")]
        public string NewEmailVerify { get; set; }
    }
}