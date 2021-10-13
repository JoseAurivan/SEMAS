using System.Collections.Generic;
using Domain.Interfaces;

namespace Domain.Models
{
    public class CestaBasica:IModelBase
    {
        public CestaBasica()
        {
            Entregas = new List<Entrega>();
        }
        public int Id { get; set; }
        public  ICollection<Entrega> Entregas { get; set; }
        public int Quant {get; set;}
        public string NumeroMeses { get; set; }
    }
}