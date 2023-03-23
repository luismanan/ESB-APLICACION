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
    public class BomberosController : OwnBaseController
    {
        private readonly IBomberosService _BomberosService;
        public BomberosController(IBomberosService BomberosService)
        {
            _BomberosService = BomberosService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<BomberoVm>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        { 
            return Ok(await _BomberosService.GetByIdAsync(id));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<BomberoVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, string filter = "")
        {
            return Ok(await _BomberosService.GetPagedListAsync(pageNumber, pageSize, filter));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<BomberoVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] BomberosDto obj)
        {
            return Ok(await  _BomberosService.InsertAsync(obj));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<IList<BomberoVm>>), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] BomberosDto obj)
        {
            return Ok(await _BomberosService.UpdateAsync(id, obj));
        }

    }
}
