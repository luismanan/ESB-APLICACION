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
    public class UsuarioController : OwnBaseController
    {
        private readonly IUsuariosService _UsuariosService;
        public UsuarioController(IUsuariosService UsuariosService)
        {
            _UsuariosService = UsuariosService;
        }

        //[HttpGet("{id}")]
        //[ProducesResponseType(typeof(Response<UsuariosVm>), 200)]
        //public async Task<IActionResult> GetAsync(int id)
        //{
        //    return Ok(await _UsuariosService.GetByIdAsync(id));
        //}

        //[HttpGet("")]
        //[ProducesResponseType(typeof(PagedResponse<IList<UsuarioRolesVm>>), 200)]
        //public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, string filter = "")
        //{
        


        //    return Ok(await _UsuariosService.GetPagedListAsync(pageNumber, pageSize, filter));
        //}

        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<UsuariosVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] UsuariosDto obj)
        {
            return Ok(await _UsuariosService.RegisterAsync(obj));
        }

        //[HttpPut]
        //[ProducesResponseType(typeof(Response<IList<UsuarioRolesVm>>), 200)]
        //public async Task<IActionResult> PutAsync(int id, [FromBody] UsuarioRolesDto obj)
        //{
        //    return Ok(await _UsuariosService.UpdateAsync(id, obj));
        //}

    }
}
