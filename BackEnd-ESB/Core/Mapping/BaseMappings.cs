using AutoMapper;
using ESB.Application.DTOs;
using ESB.Application.DTOs.ViewModel;
using ESB.Domain.Entities;
using SEGM.Application.DTOs.ViewModel;

namespace ESB.Application.Mapping
{
    public class BaseMappings : Profile
    {
        public BaseMappings()
        {

            CreateMap<Roles, RoleVm>().ReverseMap();
            CreateMap<Roles, RoleDto>().ReverseMap();

            CreateMap<Usuarios, UsuariosVm>().ReverseMap();
            CreateMap<Usuarios, UsuariosDto>().ReverseMap();
            CreateMap<Usuarios, UsuariosSubUsuarioDto>().ReverseMap();

            CreateMap<Usuarios, UsuariosUpdateVm>().ReverseMap();

            CreateMap<UsuarioRoles, UsuarioRolesDto>().ReverseMap();
            CreateMap<UsuarioRoles, UsuarioRolesVm>()
            .ForMember(o => o.User, b => b.MapFrom(z => z.User))
            .ForMember(o => o.Role, b => b.MapFrom(z => z.Role)).ReverseMap();

            CreateMap<Bomberos, BomberoVm>().ReverseMap();
            CreateMap<Bomberos, BomberosDto>().ReverseMap();

            CreateMap<RegistroIncendios, RegistroIncendiosVm>().ReverseMap();
            CreateMap<RegistroIncendios, RegistroIncendiosDto>().ReverseMap();
        }
    }
}
