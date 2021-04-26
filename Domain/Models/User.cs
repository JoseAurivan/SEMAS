﻿using Domain.Interfaces;

namespace Domain.Models
{
    public class User:IModelBase
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } 
        public string Matricula { get; set; }
        
    }
}