namespace Domain.Models
{
    public class CadastroCmas
    {
        
        public Pessoa Pessoa { get; set; }
        public int Id { get; set; }
        public string Nis { get; set; }
        public bool Inseguranca { get; set; }
        public string Residencia { get; set; }
        public string Localidade { get; set; }
        public bool Beneficio { get; set; }
        public string Familia { get; set; }
        public bool Sanitizacao { get; set; }
    }
    
}