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
    public class BomberosService : BaseService, IBomberosService
    {
        private readonly IRepositoryAsync<Bomberos> _BomberosRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<BomberosDto> _validator;
        public BomberosService(IRepositoryAsync<Bomberos> roleRepo, IMapper mapper, IValidator<BomberosDto> validator, IHttpContextAccessor context) : base(context)
        {
            _BomberosRepo = roleRepo;
            _mapper = mapper;
            _validator = validator;
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _BomberosRepo.Exists(x => x.Id.Equals(Id));
            if (result) { return true; }
            throw new ApiException("Roles not found");
        }

        public async Task<Response<BomberoVm>> InsertAsync(BomberosDto dto)
        {

            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            var obj = _mapper.Map<Bomberos>(dto);


            return new Response<BomberoVm>(_mapper.Map<BomberoVm>(await _BomberosRepo.AddAsync(obj)));

        }

        public async Task<Response<BomberoVm>> UpdateAsync(int id, BomberosDto dto)
        {
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            await ExitsAsync(id);

            var objDb = await _BomberosRepo.WhereAsync(x => x.Id.Equals(id));
            var obj = _mapper.Map<Bomberos>(dto);

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

            return new Response<BomberoVm>(_mapper.Map<BomberoVm>(await _BomberosRepo.UpdateAsync(obj)));
        }

        public async Task<Response<BomberoVm>> GetByIdAsync(int id)
        {
            var data = await _BomberosRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"Roles not found by id={id}");
            }

            return new Response<BomberoVm>(_mapper.Map<BomberoVm>(data));
        }



        public async Task<PagedResponse<IList<BomberoVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null)
        {

            List<Expression<Func<Bomberos, bool>>> queryFilter = new List<Expression<Func<Bomberos, bool>>>();
            List<Expression<Func<Bomberos, Object>>> includes = new List<Expression<Func<Bomberos, Object>>>();




            //if (filter != null || filter.Length > 0)
            //{
            //    queryFilter.Add(x => x.Id();
            //}

            var list = await _BomberosRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"Roles not found");
            }

            return new PagedResponse<IList<BomberoVm>>(_mapper.Map<IList<BomberoVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }


    }
}
