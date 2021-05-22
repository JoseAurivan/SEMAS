using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace CadastroHabCMAS.ViewModel.UserViewModel
{
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "Campo Matrícula é obrigatório")]
        public string Matricula { get; set; }
        
        
        [EmailAddress(ErrorMessage = "Campo de email é obrigatório")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Campo CPF é obrigatório")]
        [MaxLength(11, ErrorMessage = "CPF deve ter 11 caracteres")]
        [MinLength(11, ErrorMessage = "CPF deve ter 11 caracteres")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "CPF deve ser numerico")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Campo Senha é obrigatório")]
        public string Senha { get; set; }


        public User GetModel(string cpf, string matricula, string senha, string email)
        {
            return new User()
            {
                Matricula = matricula,
                Username = cpf,
                Password = senha,
                Email = email
            };
        }
        
        public bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for(int i=0; i<9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if ( resto < 2 )
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for(int i=0; i<10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}