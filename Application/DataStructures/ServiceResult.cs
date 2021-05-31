using System.Collections.Generic;
using Application.Enums;

namespace Services.DataStructures
{
    public class ServiceResult
    {
        public ServiceResultType Type { get; }
        public IEnumerable<string> Messages { get; init; }
        
        public ServiceResult(ServiceResultType type)
        {
            Type = type;
        }
    }

    public class ServiceResult<TResult> : ServiceResult
    {
        public TResult Result { get; init; }

        public ServiceResult(ServiceResultType type) : base(type)
        {
        }
    }
}