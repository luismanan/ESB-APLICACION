using ESB.Api.Controllers.Base;
using ESB.Application.DTOs;
using ESB.Application.DTOs.ViewModel;
using ESB.Application.Interfaces.Services;
using ESB.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESB.Api.Controllers
{

    [Route("api/[controller]")]
    public class RegistroIncendiosController : OwnBaseController
    {
        private readonly IRegistroIncendiosService _RegistroIncendiosService;
        public RegistroIncendiosController(IRegistroIncendiosService RegistroIncendiosService)
        {
            _RegistroIncendiosService = RegistroIncendiosService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<RegistroIncendiosVm>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        { 
            return Ok(await _RegistroIncendiosService.GetByIdDetalleAsync(id));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<RegistroIncendiosVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, string filter = "")
        {
            return Ok(await _RegistroIncendiosService.GetPagedListAsync(pageNumber, pageSize, filter));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<RegistroIncendiosVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] RegistroIncendiosDto obj)
        {
            return Ok(await  _RegistroIncendiosService.InsertAsync(obj));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<IList<RegistroIncendiosVm>>), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] RegistroIncendiosDto obj)
        {
            return Ok(await _RegistroIncendiosService.UpdateAsync(id, obj));
        }
        [HttpGet("GetListAllasync")]
        [ProducesResponseType(typeof(Response<RegistroIncendiosDetalleVm>), 200)]
        public async Task<IActionResult> GetListAllasync()
        {
            return Ok(await _RegistroIncendiosService.GetListAllasync());
        }
        [HttpGet("GetBomberoDestacadoAsync")]
        [ProducesResponseType(typeof(Response<BomberoDestacadoMesVm>), 200)]
        public async Task<IActionResult> GetBomberoDestacadoAsync()
        {
            return Ok(await _RegistroIncendiosService.GetBomberoDestacadoAsync());
        }
    }
}
