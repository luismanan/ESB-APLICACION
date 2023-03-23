using ESB.Application.DTOs;
using ESB.Application.DTOs.ViewModel;
using ESB.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESB.Application.Interfaces.Services
{
    public interface IRegistroIncendiosService
    {
        Task<Response<RegistroIncendiosVm>> InsertAsync(RegistroIncendiosDto dto);

        Task<Response<RegistroIncendiosVm>> UpdateAsync(int id, RegistroIncendiosDto dto);
        Task<Response<BomberoDestacadoMesVm>> GetBomberoDestacadoAsync();
        Task<Response<List<RegistroIncendiosDetalleVm>>> GetListAllasync();
        Task<Response<RegistroIncendiosVm>> GetByIdAsync(int id);
        Task<Response<RegistroIncendiosDetalleVm>> GetByIdDetalleAsync(int id);

        Task<PagedResponse<IList<RegistroIncendiosVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null);

    }
}
