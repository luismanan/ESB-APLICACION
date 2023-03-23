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
    public class RolesController : OwnBaseController
    {
        private readonly IRoleService _RoleService;
        public RolesController(IRoleService roleService)
        {
            _RoleService = roleService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<RoleVm>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        { 
            return Ok(await _RoleService.GetByIdAsync(id));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<RoleVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, string filter = "")
        {
            return Ok(await _RoleService.GetPagedListAsync(pageNumber, pageSize, filter));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<RoleVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] RoleDto obj)
        {
            return Ok(await _RoleService.InsertAsync(obj));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<IList<RoleVm>>), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] RoleDto obj)
        {
            return Ok(await _RoleService.UpdateAsync(id, obj));
        }

    }
}
