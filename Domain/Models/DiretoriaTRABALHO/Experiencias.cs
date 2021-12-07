using System;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Experiencias:IModelBase
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFinal { get; set; }
        public string Descricao { get; set; }
        public Curriculo Curriculo { get; set; }
    }
}