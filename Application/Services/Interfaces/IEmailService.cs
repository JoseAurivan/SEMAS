using System.Threading.Tasks;
using Services.DataStructures;

namespace Application.Services.Interfaces
{
    public interface IEmailService
    {
        Task<ServiceResult> SendForgottenPasswordAsync(string emailTo);
    }
}