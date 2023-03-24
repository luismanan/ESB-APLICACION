using AutoMapper;
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
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ESB.Infrastructure.Services
{
    public class UsuarioRolesService : BaseService, IUsuarioRolesService
    {
        private readonly IRepositoryAsync<UsuarioRoles> _UsuarioRoleRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<UsuarioRolesDto> _validator;
        private readonly IList<Expression<Func<UsuarioRoles, object>>> _includes;
        public UsuarioRolesService(IRepositoryAsync<UsuarioRoles> usuarioRoleRepo, IMapper mapper, IValidator<UsuarioRolesDto> validator, IHttpContextAccessor context) : base(context)
        {
            _UsuarioRoleRepo = usuarioRoleRepo;
            _mapper = mapper;
            _validator = validator;
            //_includes = new List<Expression<Func<UsuarioRoles, object>>>() { x => x.User, x => x.Role };
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _UsuarioRoleRepo.Exists(x => x.Id.Equals(Id));
            if (result) { return true; }
            throw new ApiException("UsuarioRoles not found");
        }

        public async Task<Response<UsuarioRolesVm>> InsertAsync(UsuarioRolesDto dto)
        {

            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            var obj = _mapper.Map<UsuarioRoles>(dto);


            return new Response<UsuarioRolesVm>(_mapper.Map<UsuarioRolesVm>(await _UsuarioRoleRepo.AddAsync(obj)));

        }

        public async Task<Response<UsuarioRolesVm>> UpdateAsync(int id, UsuarioRolesDto dto)
        {
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            await ExitsAsync(id);

            var objDb = await _UsuarioRoleRepo.WhereAsync(x => x.Id.Equals(id));
            var obj = _mapper.Map<UsuarioRoles>(dto);

            obj.Id = objDb.Id;

            return new Response<UsuarioRolesVm>(_mapper.Map<UsuarioRolesVm>(await _UsuarioRoleRepo.UpdateAsync(obj)));
        }

        public async Task<Response<UsuarioRolesVm>> GetByIdAsync(int id)
        {
            var data = await _UsuarioRoleRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"UsuarioRoles not found by id={id}");
            }

            return new Response<UsuarioRolesVm>(_mapper.Map<UsuarioRolesVm>(data));
        }



        public async Task<PagedResponse<IList<UsuarioRolesVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null)
        {

            List<Expression<Func<UsuarioRoles, bool>>> queryFilter = new List<Expression<Func<UsuarioRoles, bool>>>();
            List<Expression<Func<UsuarioRoles , Object>>> includes = new List<Expression<Func<UsuarioRoles, Object>>>();




            //if (filter != null || filter.Length > 0)
            //{
            //    queryFilter.Add(x => x.Id();
            //}

            var list = await _UsuarioRoleRepo.GetPagedList(pageNumber, pageSize, queryFilter, null, _includes);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"UsuarioRoles not found");
            }

            return new PagedResponse<IList<UsuarioRolesVm>>(_mapper.Map<IList<UsuarioRolesVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }


    }
}
