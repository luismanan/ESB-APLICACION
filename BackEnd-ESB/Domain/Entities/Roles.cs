using System;
using System.Collections.Generic;

namespace ESB.Domain.Entities
{
    public partial class Roles
    {
        public Roles()
        {
            UsuarioRoles = new HashSet<UsuarioRoles>();
        }
        public int Id { get; set; }
        public string NombreRoles { get; set; }
        public virtual ICollection<UsuarioRoles> UsuarioRoles { get; set; }
    }
}