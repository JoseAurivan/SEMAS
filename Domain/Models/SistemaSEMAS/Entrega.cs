using System;
using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Entrega:IModelBase
    {
        public int Id { get; set; }
        public DateTime? DataEntrega { get; set; }
        public StatusEntrega EntregaStatus { get; set; }
        public string NomeAgente { get; set; }

        public Unidade? Unidade { get; set; }
        public virtual CestaBasica CestaBasica { get; set; }
    }
}