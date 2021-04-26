using System.Collections.Generic;
using Services.DataStructures.Interfaces;

namespace Services.DataStructures
{
    public class FailResult:IServiceResult
    {
        public bool IsSucessfull => false;
        public IEnumerable<string> Errors { get; init; }
    }
}