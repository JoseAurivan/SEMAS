using System;
using System.Collections.Generic;
using Domain.Enums;
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
        public virtual Endereco Endereco { get; set; }
        public  ICollection<Entrega> Entregas { get; set; }
        public int Quant { get; set; }
        public string NumeroMeses { get; set; }
        public bool? DeterminacaoJuridica { get; set; }
        public bool? RecomendacaoTecnica { get; set; }
        
        public Demandas? Demandas { get; set; }
        public string? Caminhos { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is CestaBasica cestaBasica) return Equals(cestaBasica);
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public  bool Equals(CestaBasica other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetHashCode() != other.GetHashCode()) return false;
            
            return Id == other.Id;
        }
    }
}