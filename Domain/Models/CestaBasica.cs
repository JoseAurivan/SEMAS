using Domain.Interfaces;

namespace Domain.Models
{
    public class CestaBasica:IModelBase
    {

        public int Id { get; set; }
        public string prxEntrega { get; set; }
        public int quant { get; set; }
        public bool status { get; set; }
        public string statusDesc { get; set; }
         
    }
}