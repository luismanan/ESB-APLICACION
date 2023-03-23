using System;
using System.Collections.Generic;

namespace ESB.application.DTOs.Security
{
    public class AuthenticationResponse
    {     
        public string JWToken { get; set; }
        public DateTime Expiration { get; set; }
        //public string NombreUsuario { get; set; }
        //public int  IdUsuario { get; set; }
        //public string Organizacion { get; set; }
        //public int IdOrganizacion { get; set; }
        //public string NombreRol { get; set; }   
       
        
        
        
    }
}
