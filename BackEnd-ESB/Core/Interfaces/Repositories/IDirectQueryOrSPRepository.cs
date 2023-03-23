using ESB.Application.DTOs;
using ESB.Application.Wrappers;
using System.Threading.Tasks;

namespace ESB.Application.Interfaces.Repositories
{
    public interface IDirectQueryOrSPRepository
    {
        Task<Response<SpResult>> SpTestResult(string nombre, string apellido); 
       
    }
}
