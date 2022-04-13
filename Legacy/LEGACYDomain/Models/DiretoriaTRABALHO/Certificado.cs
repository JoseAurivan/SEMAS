using Domain.Interfaces;

namespace Domain.Models
{
    public class Certificado:IModelBase
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Expeditor { get; set; }
        public string CopiaPdf { get; set; }
        public Curriculo Curriculo { get; set; }
    }
}