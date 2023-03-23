using ESB.Application.DTOs;
using ESB.Application.DTOs.ViewModel;
using ESB.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESB.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<Response<RoleVm>> InsertAsync(RoleDto dto);

        Task<Response<RoleVm>> UpdateAsync(int id, RoleDto dto);


        Task<Response<RoleVm>> GetByIdAsync(int id);

        Task<PagedResponse<IList<RoleVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null);

    }
}
