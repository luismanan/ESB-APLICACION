using System;
using System.Collections.Generic;

namespace ESB.Domain.Entities
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            UsuarioRoles = new HashSet<UsuarioRoles>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contraseña { get; set; }      
        public string ClaveSeguridad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? UltimoAcceso { get; set; }

        public bool Estado { get; set; }
        public virtual ICollection<UsuarioRoles> UsuarioRoles { get; set; }
    }
}