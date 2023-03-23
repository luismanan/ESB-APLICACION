using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESB.Application.DTOs
{
    public class UsuariosDto
    {
        //public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contraseña { get; set; }
        public string ConfirmarContraseña { get; set; }
        //public string ClaveSeguridad { get; set; }
        //public DateTime FechaCreacion { get; set; }
        //public DateTime? UltimoAcceso { get; set; }
        //public bool Estado { get; set; }

    }
}
