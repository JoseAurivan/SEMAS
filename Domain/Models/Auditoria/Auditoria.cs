using System;
using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Auditoria:IModelBase
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime DateTime { get; set; }
        public string Exception { get; set; }
        public string Properties { get; set; }
        public string LogEvent { get; set; }
    }
}