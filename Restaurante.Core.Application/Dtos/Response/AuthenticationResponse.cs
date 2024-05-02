
using System.Text.Json.Serialization;

namespace Restaurante.Core.Application.Dtos.Response
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
        public string? JWToken { get; set; }
        //Esto es para ocultar o ignorar el refresh token a la hora de actualizarse.
        [JsonIgnore]
        public string? RefreshToken { get; set; }

    }
}
