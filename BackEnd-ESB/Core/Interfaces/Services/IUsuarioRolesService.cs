using ESB.Application.DTOs;
using ESB.Application.DTOs.ViewModel;
using ESB.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESB.Application.Interfaces.Services
{
    public interface IUsuarioRolesService
    {
        Task<Response<UsuarioRolesVm>> InsertAsync(UsuarioRolesDto dto);

        Task<Response<UsuarioRolesVm>> UpdateAsync(int id, UsuarioRolesDto dto);

        Task<Response<UsuarioRolesVm>> GetByIdAsync(int id);

        Task<PagedResponse<IList<UsuarioRolesVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null);

    }
}
