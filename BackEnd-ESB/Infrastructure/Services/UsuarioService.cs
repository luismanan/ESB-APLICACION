using AutoMapper;
using ESB.application.Interfaces.Services.Security;
using ESB.Application.DTOs;
using ESB.Application.DTOs.ViewModel;
using ESB.Application.Exceptions;
using ESB.Application.Interfaces.Repositories;
using ESB.Application.Interfaces.Services;
using ESB.Application.Wrappers;
using ESB.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESB.Infrastructure.Services
{
    public class UsuarioService : BaseService, IUsuariosService
    {
        private readonly IRepositoryAsync<Usuarios> _UsuarioRepo;
        private readonly ICryptographyProcessorService _cryptographyProcessorService;
        private readonly IMapper _mapper;
        private readonly IValidator<UsuariosDto> _validator; 
        private readonly IRepositoryAsync<UsuarioRoles> _userRoles;
       
        public UsuarioService(ICryptographyProcessorService cryptographyProcessorService,  IRepositoryAsync<Usuarios> usuarioRepo, IRepositoryAsync<UsuarioRoles> userRoles,IMapper mapper, IValidator<UsuariosDto> validator, IHttpContextAccessor context) : base(context)
        {
            _UsuarioRepo = usuarioRepo;
            _mapper = mapper;
            _validator = validator;
            _cryptographyProcessorService = cryptographyProcessorService;
            _userRoles = userRoles;
  
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _UsuarioRepo.Exists(x => x.Id.Equals(Id));
            if (result) { return true; }
            throw new ApiException("Usuarios no encontrada");
        }

        public async Task<Response<UsuariosVm>> RegisterAsync(UsuariosDto dto)
        {
            var result = await _UsuarioRepo.Exists(x => x.CorreoElectronico.Equals(dto.CorreoElectronico));
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            if (result == true)
            {
                throw new KeyNotFoundException($"Usuario ya Existe ={dto.CorreoElectronico}");
            }
            var obj = _mapper.Map<Usuarios>(dto);
            var password = _cryptographyProcessorService.GetPasswordAndSecurityKeyInfo(dto.Contraseña);
            obj.Contraseña = password.HashedPassword;
            obj.ClaveSeguridad = password.SecurityKey;
            obj.FechaCreacion = DateTime.Now;
            obj.Estado = true;
            return new Response<UsuariosVm>(_mapper.Map<UsuariosVm>(await _UsuarioRepo.AddAsync(obj)));

        }

        //public async Task<Response<string>> RegisterSubUsuarioAsync(UsuariosSubUsuarioDto dto)
        //{
        //    var result = await _UsuarioRepo.Exists(x => x.CorreoElectronico.Equals(dto.CorreoElectronico));
        //    //var valResult = _validator.Validate(dto);
        //    //if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

        //    if (result == true)
        //    {
        //        throw new KeyNotFoundException($"Usuario ya Existe ={dto.CorreoElectronico}");
        //    }
        //    Guid clave = Guid.NewGuid();
        //    dto.Contraseña = clave.ToString().Substring(0,8);
        //    var obj = _mapper.Map<Usuarios>(dto);
        //    var password = _cryptographyProcessorService.GetPasswordAndSecurityKeyInfo(dto.Contraseña);
        //    obj.Contraseña = password.HashedPassword;
        //    obj.ClaveSeguridad = password.SecurityKey;
        //    obj.FechaCreacion = DateTime.Now;
        //    //return new Response<UsuariosVm>(_mapper.Map<UsuariosVm>(await _UsuarioRepo.AddAsync(obj)));
        //    return new Response<string>("El Usuario se Insertado Correctamente");
        //}
        //public async Task<Response<UsuariosVm>> UpdateAsync(int id, UsuariosDto dto)
        //{
        //    var valResult = _validator.Validate(dto);
        //    if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);
        //    await ExitsAsync(id);

        //    var objDb = await _UsuarioRepo.WhereAsync(x => x.Id.Equals(id));
        //    var obj = _mapper.Map<Usuarios>(dto);

        //    obj.Id = objDb.Id;
        //    var password = _cryptographyProcessorService.GetPasswordAndSecurityKeyInfo(dto.Contraseña);
        //    obj.Contraseña = password.HashedPassword;
        //    obj.ClaveSeguridad = password.SecurityKey;
        //    obj.FechaCreacion = DateTime.Now;

        //    return new Response<UsuariosVm>(_mapper.Map<UsuariosVm>(await _UsuarioRepo.UpdateAsync(obj)));
        //}


        //public async Task<Response<UsuariosUpdateVm>> DesactivarAsync(int id, UsuariosUpdateDto dto)
        //{
        //    //var valResult = _validator.Validate(dto);
        //    //if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

        //    await ExitsAsync(id);
        //    var objDb = await _UsuarioRepo.WhereAsync(x => x.Id.Equals(id));
        //    objDb.Estado = dto.Estado;
        //    //var obj = _mapper.Map<Usuario>(dto);
        //    return new Response<UsuariosUpdateVm>(_mapper.Map<UsuariosUpdateVm>(await _UsuarioRepo.UpdateAsync(objDb)));
        //}

        //public async Task<Response<UsuariosVm>> GetByIdAsync(int id)
        //{
        //    var data = await _UsuarioRepo.GetByIdAsync(id);
        //    if (data == null)
        //    {
        //        throw new KeyNotFoundException($"Usuarios no encontrado con id={id}");
        //    }

        //    return new Response<UsuariosVm>(_mapper.Map<UsuariosVm>(data));
        //}

        //public async Task<Response<IList<UsuariosVm>>> GetByIdOrganizacionActivoAsync(int id)
        //{
        //    var data = await _UsuarioRepo.WhereAllAsync( x=> x.IdOrganizacion == id && x.Estado == true);
        //    if (data == null)
        //    {
        //        throw new KeyNotFoundException($"Usuarios no encontrado con id={id}");
        //    }

        //    return new Response<IList<UsuariosVm>>(_mapper.Map<IList<UsuariosVm>>(data));
        //}
        //public async Task<Response<IList<UsuariosVm>>> GetByIdOrganizacionInactivoAsync(int id)
        //{
        //    var data = await _UsuarioRepo.WhereAllAsync(x => x.IdOrganizacion == id && x.Estado == false);
        //    if (data == null)
        //    {
        //        throw new KeyNotFoundException($"Usuarios no encontrado con id={id}");
        //    }

        //    return new Response<IList<UsuariosVm>>(_mapper.Map<IList<UsuariosVm>>(data));
        //}
        //public async Task<Response<IList<UsuariosVm>>> GetListAllasync()
        //{
        //    var data = await _UsuarioRepo.GelAllAsync();
        //    if (data == null)
        //    {
        //        throw new KeyNotFoundException($"Usuario no Encontrado");
        //    }

        //    return new Response<IList<UsuariosVm>>(_mapper.Map<IList<UsuariosVm>>(data));
        //}

        //public async Task<PagedResponse<IList<UsuariosVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null)
        //{

        //    List<Expression<Func<Usuarios, bool>>> queryFilter = new List<Expression<Func<Usuarios, bool>>>();
        //    List<Expression<Func<Usuarios, Object>>> includes = new List<Expression<Func<Usuarios, Object>>>();

        //    var user = base.GetLoggerUser(); 
        //    var UserId = base.GetLoggerUserId();

        //    if (filter != null || filter.Length > 0)
        //    {
        //        queryFilter.Add(x => x.CorreoElectronico.Contains(filter));
        //    }

        //    var list = await _UsuarioRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
        //    if (list == null || list.Data.Count == 0)
        //    {
        //        throw new KeyNotFoundException($"Usuarios no encontrada");
        //    }

        //    return new PagedResponse<IList<UsuariosVm>>(_mapper.Map<IList<UsuariosVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        //}
    }
}
