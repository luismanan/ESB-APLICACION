using ESB.application.DTOs.Security;
using ESB.application.DTOs.Settings;
using ESB.application.Interfaces.Services.Security;
using ESB.Application.Exceptions;
using ESB.Application.Interfaces.Repositories;
using ESB.Application.Wrappers;
using ESB.Domain.Entities;
using ESB.Identity.Helper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ESB.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly JWTSettings _jwtSettings;
        private readonly ICryptographyProcessorService _cryptographyProcessorService;
        private readonly IRepositoryAsync<Usuarios> _userRepo;
        private readonly IRepositoryAsync<UsuarioRoles> _userRoles;

        public AccountService(JWTSettings jwtSettings, ICryptographyProcessorService cryptographyProcessorService, IRepositoryAsync<Usuarios> userRepo, IRepositoryAsync<UsuarioRoles> userRoles)
        {
            _jwtSettings = jwtSettings;
            _cryptographyProcessorService = cryptographyProcessorService;
            _userRepo = userRepo;
            _userRoles = userRoles;

        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {

            var userData = await _userRepo.WhereAllAsync(x => x.CorreoElectronico == request.Email);
            var usuario = userData.LastOrDefault();
            if (usuario == null )
            {
                throw new ApiException($"Usuario {request.Email} no encontrado.");
            }
            if (usuario.Estado == false)
            {
                throw new ApiException($"Usuario {request.Email} Esta Desactivado.");
            }
            var PasswordsMatchResult = _cryptographyProcessorService.PasswordsMatch(request.Password, usuario.ClaveSeguridad, usuario.Contraseña);
            if (!PasswordsMatchResult)
            {
                throw new ApiException($"Datos invalidos");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJWTToken(usuario);
            AuthenticationResponse response = new AuthenticationResponse();
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Expiration = DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes);

            usuario.UltimoAcceso =  DateTime.Now;
            
            await _userRepo.UpdateAsync(usuario);   
            return new Response<AuthenticationResponse>(response, $"Authenticate {usuario.CorreoElectronico}");
        }


        private async Task<JwtSecurityToken> GenerateJWTToken(Usuarios usuario)
        {
            ///ROLES PARA JWT 
            var rolesDatas = await _userRoles.WhereAllAsync(x => x.IdUsuario == usuario.Id, include: x => x.Role);
            var Roles = rolesDatas.LastOrDefault();

            /// ROLES PARA LOS CLAIM
            var rolesData = await _userRoles.WhereAllAsync(x => x.Id.Equals(usuario.Id), xd => xd.Role);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < rolesData.Count; i++)
            {
                roleClaims.Add(new Claim("roles", rolesData[i].Role.NombreRoles));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.CorreoElectronico),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Email", usuario.CorreoElectronico),
                new Claim("uid", usuario.Id.ToString()),
                new Claim("ip", ipAddress),
                new Claim("name", usuario.Nombre),
                new Claim("IdUsuario",usuario.Id.ToString()),
                new Claim("NombreRol",Roles.Role.NombreRoles),
             }
            .Union(roleClaims);


            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
                );

            return jwtSecurityToken;
        }

    }
}
