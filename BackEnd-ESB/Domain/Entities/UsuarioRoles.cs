using System;
using System.Collections.Generic;

namespace ESB.Domain.Entities
{
    public partial class UsuarioRoles
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdRoles { get; set; }

        public virtual Roles Role { get; set; }
        public virtual Usuarios User { get; set; }
    }
}