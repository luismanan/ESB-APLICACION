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
    public class RolesService : BaseService, IRoleService
    {
        private readonly IRepositoryAsync<Roles> _RoleRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<RoleDto> _validator;
        public RolesService(IRepositoryAsync<Roles> roleRepo, IMapper mapper, IValidator<RoleDto> validator, IHttpContextAccessor context) : base(context)
        {
            _RoleRepo = roleRepo;
            _mapper = mapper;
            _validator = validator;
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _RoleRepo.Exists(x => x.Id.Equals(Id));
            if (result) { return true; }
            throw new ApiException("Roles not found");
        }

        public async Task<Response<RoleVm>> InsertAsync(RoleDto dto)
        {

            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            var obj = _mapper.Map<Roles>(dto);


            return new Response<RoleVm>(_mapper.Map<RoleVm>(await _RoleRepo.AddAsync(obj)));

        }

        public async Task<Response<RoleVm>> UpdateAsync(int id, RoleDto dto)
        {
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            await ExitsAsync(id);

            var objDb = await _RoleRepo.WhereAsync(x => x.Id.Equals(id));
            var obj = _mapper.Map<Roles>(dto);

            obj.Id = objDb.Id;
            //Note: You can automap the object or map manualy, as this code down.

            //#region Mapping 
            //obj.Name = dto.Name;
            //obj.LasName = dto.LasName;
            //obj.Address = dto.Address;
            //obj.Status = dto.Status;
            //obj.Note = dto.Note;
            //obj.YearOfbirth = dto.YearOfbirth;
            //obj.MonthOfbirth = dto.MonthOfbirth;
            //obj.DayOfbirth = dto.DayOfbirth;
            //#endregion Mapping

            return new Response<RoleVm>(_mapper.Map<RoleVm>(await _RoleRepo.UpdateAsync(obj)));
        }

        public async Task<Response<RoleVm>> GetByIdAsync(int id)
        {
            var data = await _RoleRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"Roles not found by id={id}");
            }

            return new Response<RoleVm>(_mapper.Map<RoleVm>(data));
        }



        public async Task<PagedResponse<IList<RoleVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null)
        {

            List<Expression<Func<Roles, bool>>> queryFilter = new List<Expression<Func<Roles, bool>>>();
            List<Expression<Func<Roles, Object>>> includes = new List<Expression<Func<Roles, Object>>>();




            //if (filter != null || filter.Length > 0)
            //{
            //    queryFilter.Add(x => x.Id();
            //}

            var list = await _RoleRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"Roles not found");
            }

            return new PagedResponse<IList<RoleVm>>(_mapper.Map<IList<RoleVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }


    }
}
