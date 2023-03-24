using System;
using System.Collections.Generic;

namespace ESB.Domain.Entities
{
    public partial class RegistroIncendios
    {
        public int Id { get; set; }
        public string Direccion { get; set; }
        public int IdBomberoCargo { get; set; }
        public DateTime FechaCreacion { get; set; }

        public virtual Bomberos IdBomberoCargoNavigation { get; set; }
    }
}