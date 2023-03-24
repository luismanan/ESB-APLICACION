using ESB.Application.DTOs;
using ESB.Application.DTOs.ViewModel;
using ESB.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESB.Application.Interfaces.Services
{
    public interface IUsuariosService
    {
        Task<Response<UsuariosVm>> RegisterAsync(UsuariosDto dto);
        Task<Response<UsuariosVm>> UpdateAsync(int id, UsuariosDto dto);
        Task<Response<UsuariosVm>> GetByIdAsync(int id);
        Task<PagedResponse<IList<UsuariosVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null);
    }
}
