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
    public class UsuarioRolesController : OwnBaseController
    {
        private readonly IUsuarioRolesService _usuarioRolesService;
        public UsuarioRolesController(IUsuarioRolesService usuarioRolesService)
        {
            _usuarioRolesService = usuarioRolesService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<UsuarioRolesVm>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _usuarioRolesService.GetByIdAsync(id));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<UsuarioRolesVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, string filter = "")
        {
        


            return Ok(await _usuarioRolesService.GetPagedListAsync(pageNumber, pageSize, filter));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<UsuarioRolesVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] UsuarioRolesDto obj)
        {
            return Ok(await _usuarioRolesService.InsertAsync(obj));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<IList<UsuarioRolesVm>>), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UsuarioRolesDto obj)
        {
            return Ok(await _usuarioRolesService.UpdateAsync(id, obj));
        }

    }
}
