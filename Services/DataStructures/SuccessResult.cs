using Services.DataStructures.Interfaces;

namespace Services.DataStructures
{
    public class SuccessResult:IServiceResult
    {
        public bool IsSucessfull => true;
    }
    public class SuccessResult<TResult>:SuccessResult
    {

        public TResult Result { get; init; }
    }
}