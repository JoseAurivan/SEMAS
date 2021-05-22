using Domain.Interfaces;

namespace Domain.Models
{
    public class CestaBasica:IModelBase
    {

        public int Id { get; set; }
        public string PrxEntrega { get; set; }
        public int Quant { get; set; }
        public bool Status { get; set; }
        public string NumeroMeses { get; set; }
        
         
    }
}