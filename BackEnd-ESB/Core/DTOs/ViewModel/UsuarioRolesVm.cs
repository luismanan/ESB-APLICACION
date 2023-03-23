namespace ESB.Application.DTOs.ViewModel
{
    public class UsuarioRolesVm
    {
        public int Id { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdRoles { get; set; }

        public virtual RoleVm Role { get; set; }
        public virtual UsuariosVm User { get; set; }
    }
}
