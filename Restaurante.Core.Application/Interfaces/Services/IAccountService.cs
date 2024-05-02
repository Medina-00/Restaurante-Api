using Restaurante.Core.Application.Dtos.Request;
using Restaurante.Core.Application.Dtos.Response;

namespace Restaurante.Core.Application.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request , string userId);
        Task SignOutAsync();
    }
}