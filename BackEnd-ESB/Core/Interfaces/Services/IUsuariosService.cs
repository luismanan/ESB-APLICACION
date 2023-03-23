using ESB.Application.DTOs;
using ESB.Application.DTOs.ViewModel;
using ESB.Application.Wrappers;
using System.Threading.Tasks;

namespace ESB.Application.Interfaces.Services
{
    public interface IUsuariosService
    {
        //Task<Response<IList<UsuariosVm>>> GetListAllasync();
        Task<Response<UsuariosVm>> RegisterAsync(UsuariosDto dto);
        //Task<Response<string>> RegisterSubUsuarioAsync(UsuariosSubUsuarioDto dto);
        //Task<Response<UsuariosVm>> UpdateAsync(int id, UsuariosDto dto);
        //Task<Response<IList<UsuariosVm>>> GetByIdOrganizacionActivoAsync(int id);
        //Task<Response<IList<UsuariosVm>>> GetByIdOrganizacionInactivoAsync(int id);
        //Task<Response<UsuariosUpdateVm>> DesactivarAsync(int id, UsuariosUpdateDto dto);
        //Task<Response<UsuariosVm>> GetByIdAsync(int id);
        //Task<PagedResponse<IList<UsuariosVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null);

    }
}
