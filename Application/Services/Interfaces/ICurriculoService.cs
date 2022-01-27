using System.Threading.Tasks;
using Domain.Models;
using Services.DataStructures;

namespace Application.Services.Interfaces
{
    public interface ICurriculoService
    {
        Task<ServiceResult> SalvarCurriculo(Curriculo curriculo, Certificado certificado, Experiencias experiencias);
    }
}