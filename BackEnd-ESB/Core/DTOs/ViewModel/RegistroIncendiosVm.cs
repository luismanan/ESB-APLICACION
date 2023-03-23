using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESB.Application.DTOs.ViewModel
{
    public class RegistroIncendiosVm
    {
        public int Id { get; set; }
        public string Direccion { get; set; }
        public int IdBomberoCargo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdUsuariosRegistros { get; set; }
    }
}
