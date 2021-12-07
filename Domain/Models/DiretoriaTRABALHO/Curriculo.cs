using System.Collections.Generic;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Curriculo:IModelBase
    {
        public int Id { get; set; }
        public string Resumo { get; set; }
        public string Competencias { get; set; }
        public ICollection<Certificado> Certificados { get; set; }
        public ICollection<Experiencias> Experiencias { get; set; }

    }
}