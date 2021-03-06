using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Models
{
    public class User:IModelBase
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } 
        public string Nome { get; set; }
        public string Email { get; set; }
        public Roles? Roles { get; set; }
    }
}