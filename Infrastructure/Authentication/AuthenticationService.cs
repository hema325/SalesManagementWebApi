using Application.Authentication.Common;
using Infrastructure.Authentication.Models;
using Infrastructure.Authentication.Settings;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Authentication
{
    internal class AuthenticationService: IAuthentication
    {
        private readonly JwtBearerSettings _jwtSettings;
        private readonly RefreshTokenSettings _refreshTokenSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDateTime _dateTime;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(IOptions<JwtBearerSettings> jwtOptions,
                                     IOptions<RefreshTokenSettings> refreshTokenOptions,
                                     IDateTime dateTime,
                                     UserManager<ApplicationUser> userManager,
                                     ILogger<AuthenticationService> logger)
        {
            _jwtSettings = jwtOptions.Value;
            _refreshTokenSettings = refreshTokenOptions.Value;
            _dateTime = dateTime;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<AuthResult> RegisterAsync(string userName, string password, IEnumerable<Roles> roles)
        {
            var user = new ApplicationUser { UserName = userName };

            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRolesAsync(user, roles.Select(r => r.ToString()));

            var jwtSecurityToken = GenerateJwtSecurityToken(await GetUserClaimsAsync(user.Id));
            var refreshToken = await GetFirstActiveOrNewRefreshTokenAsync(user);

            _logger.LogInformation("New user with id {id} has been registered", user.Id);

            return new AuthResult
            {
                Status = StatusCodes.Status200OK,
                Message = "You have Registered Successfully",
                JwtToken = WriteToken(jwtSecurityToken),
                JwtTokenExpiresOn = jwtSecurityToken.ValidTo,
                RefreshToken = refreshToken.Value,
                RefreshTokenExpiresOn = refreshToken.ExpiresOn
            };
        }

        public async Task<AuthResult> AuthenticateAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                return new AuthResult { Message = "Invalid username or password", Status = StatusCodes.Status400BadRequest };

            var jwtSecurityToken = GenerateJwtSecurityToken(await GetUserClaimsAsync(user.Id));
            var refreshToken = await GetFirstActiveOrNewRefreshTokenAsync(user);

            return new AuthResult
            {
                Status = StatusCodes.Status200OK,
                Message = "You have authenticated Successfully",      
                JwtToken = WriteToken(jwtSecurityToken),
                JwtTokenExpiresOn = jwtSecurityToken.ValidTo,
                RefreshToken = refreshToken.Value,
                RefreshTokenExpiresOn = refreshToken.ExpiresOn
            };
        }
        
        public async Task<AuthResult> RequestJwtTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Value == refreshToken));

            if (user == null)
                return new AuthResult { Status = StatusCodes.Status404NotFound, Message = "this token wasn't found" };

            var oldRefreshToken = user.RefreshTokens.Single(t => t.Value == refreshToken);

            if (oldRefreshToken.ExpiresOn < _dateTime.Now || oldRefreshToken.RevokedOn != null)
                return new AuthResult { Status = StatusCodes.Status400BadRequest, Message = "this token is invalid" };

            oldRefreshToken.RevokedOn = _dateTime.Now;

            var newRefreshToken = GenerateRefreshToken();
            var requestedJwtToken = GenerateJwtSecurityToken(await GetUserClaimsAsync(user.Id));

            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            return new AuthResult 
            {
                Status = StatusCodes.Status200OK,
                Message = "You have requested the jwt token Successfully",
                JwtToken = WriteToken(requestedJwtToken),
                JwtTokenExpiresOn = requestedJwtToken.ValidTo,
                RefreshToken = newRefreshToken.Value,
                RefreshTokenExpiresOn = newRefreshToken.ExpiresOn
            };
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Value == refreshToken));

            if (user != null)
            {
                var oldRefreshToken = user.RefreshTokens.Single(t => t.Value == refreshToken);
                oldRefreshToken.RevokedOn = _dateTime.Now;

                await _userManager.UpdateAsync(user);
            }
        }

        private async Task<RefreshToken> GetFirstActiveOrNewRefreshTokenAsync(ApplicationUser? user)
        {
            var refreshToken = user.RefreshTokens.FirstOrDefault(t => t.RevokedOn == null && t.ExpiresOn > _dateTime.Now);

            if (refreshToken == null)
            {
                refreshToken = GenerateRefreshToken();

                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
            }

            return refreshToken;
        }

        private JwtSecurityToken GenerateJwtSecurityToken(IEnumerable<Claim> Claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: Claims,
                expires: _dateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
        }

        private async Task<IEnumerable<Claim>> GetUserClaimsAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName)
            }
            .Union((await _userManager.GetRolesAsync(user)).Select(r => new Claim(ClaimTypes.Role, r)))
            .Union((await _userManager.GetClaimsAsync(user)));
        }

        private RefreshToken GenerateRefreshToken()
        {
            var bytes = new byte[_refreshTokenSettings.Length];
            RandomNumberGenerator.Create().GetBytes(bytes);

            return new RefreshToken
            {
                Value = Convert.ToBase64String(bytes),
                CreatedOn = _dateTime.Now,
                ExpiresOn = _dateTime.Now.AddDays(_refreshTokenSettings.ExpirationInDayes)
            };
        }

        private string WriteToken(JwtSecurityToken jwtSecurityToken)
        {
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
