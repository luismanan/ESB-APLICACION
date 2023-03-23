using ESB.Application.DTOs;
using ESB.Application.DTOs.ViewModel;
using ESB.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESB.Application.Interfaces.Services
{
    public interface IBomberosService
    {
        Task<Response<BomberoVm>> InsertAsync(BomberosDto dto);

        Task<Response<BomberoVm>> UpdateAsync(int id, BomberosDto dto);


        Task<Response<BomberoVm>> GetByIdAsync(int id);

        Task<PagedResponse<IList<BomberoVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null);

    }
}
