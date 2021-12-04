using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authentication;
        private readonly IConfiguration _configuration;

        public TokenController(IAuthenticate authentication, IConfiguration configuration)
        {
            _authentication = authentication;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserToken>> Register([FromBody] RegisterModel userInfo)
        {
            var result = await _authentication.RegisterUser(userInfo.Email, userInfo.Password);

            if (!result)
            {
                return BadRequest();
            }

            return GenerateToken(new LoginModel()
            {
                Email = userInfo.Email,
                Password = userInfo.Password
            });
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
        {
            var result = await _authentication.Authenticate(userInfo.Email, userInfo.Password);

            if (!result)
            {
                return BadRequest();
            }

            return GenerateToken(userInfo);
        }

        private UserToken GenerateToken(LoginModel userInfo)
        {
            var claims = new[]
            {
                new Claim("email", userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var audience = _configuration["Jwt:Audience"];
            var issuer = _configuration["Jwt:Issuer"];
            var secretKey = _configuration["Jwt:SecretKey"];

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(10);

            var token = new JwtSecurityToken(
                audience: audience,
                issuer: issuer,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserToken()
            {
                Token = serializedToken,
                Expiration = expiration
            };
        }
    }
}
