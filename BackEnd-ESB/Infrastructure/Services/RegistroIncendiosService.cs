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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ESB.Infrastructure.Services
{
    public class RegistroIncendiosService : BaseService, IRegistroIncendiosService
    {
        private readonly IRepositoryAsync<RegistroIncendios> _RegistroIncendiosRepo;
        private readonly IRepositoryAsync<Bomberos> _BomberosRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<RegistroIncendiosDto> _validator;
        public RegistroIncendiosService(IRepositoryAsync<RegistroIncendios> RegistroIncendiosRepo, IRepositoryAsync<Bomberos> BomberosRepo, IMapper mapper, IValidator<RegistroIncendiosDto> validator, IHttpContextAccessor context) : base(context)
        {
            _RegistroIncendiosRepo = RegistroIncendiosRepo;
            _BomberosRepo = BomberosRepo;
            _mapper = mapper;
            _validator = validator;
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _RegistroIncendiosRepo.Exists(x => x.Id.Equals(Id));
            if (result) { return true; }
            throw new ApiException("Roles not found");
        }

        public async Task<Response<RegistroIncendiosVm>> InsertAsync(RegistroIncendiosDto dto)
        {
            //var User = base.GetLoggerUserId();

            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            var obj = _mapper.Map<RegistroIncendios>(dto);
            obj.FechaCreacion = DateTime.Now;
            //obj.IdUsuariosRegistros = User;



            return new Response<RegistroIncendiosVm>(_mapper.Map<RegistroIncendiosVm>(await _RegistroIncendiosRepo.AddAsync(obj)));

        }

        public async Task<Response<RegistroIncendiosVm>> UpdateAsync(int id, RegistroIncendiosDto dto)
        {
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            await ExitsAsync(id);

            var objDb = await _RegistroIncendiosRepo.WhereAsync(x => x.Id.Equals(id));
            var obj = _mapper.Map<RegistroIncendios>(dto);

            obj.Id = objDb.Id;
            obj.FechaCreacion = objDb.FechaCreacion;

            return new Response<RegistroIncendiosVm>(_mapper.Map<RegistroIncendiosVm>(await _RegistroIncendiosRepo.UpdateAsync(obj)));
        }

        public async Task<Response<RegistroIncendiosDetalleVm>> GetByIdDetalleAsync(int id)
        {
         
            var data = await _RegistroIncendiosRepo.WhereAsync(x => x.Id == id);
            //var total = await _RegistroIncendiosRepo.WhereAllAsync(x => x.Id == data.Id);
            var NombreBombero = await _BomberosRepo.WhereAsync(x => x.Id == data.IdBomberoCargo);
            //int Cantidad2 = total.Count();
            var info2 = new RegistroIncendiosDetalleVm()
            {
                Id = data.Id,
                Direccion = data.Direccion,
                BomberoCargo = NombreBombero.Nombre +" "+NombreBombero.Apellido,
                //Cantidad = Cantidad2
            };
            return new Response<RegistroIncendiosDetalleVm>(info2);
        }
        public async Task<Response<RegistroIncendiosVm>> GetByIdAsync(int id)
        {
            var data = await    _RegistroIncendiosRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"Roles not found by id={id}");
            }

            return new Response<RegistroIncendiosVm>(_mapper.Map<RegistroIncendiosVm>(data));
        }
        public async Task<Response<List<RegistroIncendiosDetalleVm>>> GetListAllasync()
        {
            var _return = new List<RegistroIncendiosDetalleVm>();
            //int IdUser = base.GetLoggerUserId();

            var data = await _RegistroIncendiosRepo.GelAllAsync();
            foreach (var item in data)
            {
                var NombreBombero = await _BomberosRepo.WhereAsync(x => x.Id == item.IdBomberoCargo);
                var info2 = new RegistroIncendiosDetalleVm()
                {
                    Id = item.Id,
                    Direccion = item.Direccion,
                    IdBomberoCargo = item.IdBomberoCargo,
                    BomberoCargo = NombreBombero.Nombre + " " + NombreBombero.Apellido,
                    FechaCreacion = item.FechaCreacion,
                };
                _return.Add(info2);
            }  
            return new Response<List<RegistroIncendiosDetalleVm>>(_return);
        }

        public async Task<Response<BomberoDestacadoMesVm>> GetBomberoDestacadoAsync()
        {
            var data = await _RegistroIncendiosRepo.GelAllAsync();
            var list = data.GroupBy(g => g.IdBomberoCargo).Select(g => new { Key = g.Key, cnt = g.Count() }).OrderByDescending(x => x.cnt).FirstOrDefault();
            var NombreBombero = await _BomberosRepo.WhereAsync(x => x.Id == list.Key);

                var info2 = new BomberoDestacadoMesVm()
                {                
                    BomberoCargo = NombreBombero.Nombre + " " + NombreBombero.Apellido,
                    Cantidad_de_Incendio = list.cnt
                };
                return new Response<BomberoDestacadoMesVm>(info2);
            
        }
        public async Task<PagedResponse<IList<RegistroIncendiosVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null)
        {
            List<Expression<Func<RegistroIncendios, bool>>> queryFilter = new List<Expression<Func<RegistroIncendios, bool>>>();
            List<Expression<Func<RegistroIncendios, Object>>> includes = new List<Expression<Func<RegistroIncendios, Object>>>();

            //if (filter != null || filter.Length > 0)
            //{
            //    queryFilter.Add(x => x.Id();
            //}
            var list = await _RegistroIncendiosRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"Roles not found");
            }

            return new PagedResponse<IList<RegistroIncendiosVm>>(_mapper.Map<IList<RegistroIncendiosVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }


    }
}
