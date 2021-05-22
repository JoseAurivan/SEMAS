using System.Collections.Generic;
using Application.Enums;

namespace Services.DataStructures
{
    public class ServiceResult
    {
        public ServiceResultType Type { get; init; }
        public IEnumerable<string> Messages { get; init; }
    }

    public class ServiceResult<TResult> : ServiceResult
    {
        public TResult Result { get; init; }
    }
}