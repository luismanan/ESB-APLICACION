using ESB.application.DTOs.Security;
using ESB.Application.Wrappers;
using System.Threading.Tasks;

namespace ESB.application.Interfaces.Services.Security
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);

    }
}
