using System.Text.Json.Serialization;

namespace Application.Authentication.Common
{
    public class AuthResult
    {
        [JsonIgnore]
        public int Status { get; init; }
        public bool Succeeded => Status >= 200 && Status <= 299;
        public string Message { get; init; }
        public string JwtToken { get; init; }
        public DateTime JwtTokenExpiresOn { get; init; }
        public string RefreshToken { get; init; }
        public DateTime RefreshTokenExpiresOn { get; init; }
    }
}
