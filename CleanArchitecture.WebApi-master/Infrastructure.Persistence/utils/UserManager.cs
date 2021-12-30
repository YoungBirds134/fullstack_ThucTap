using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Account;
using Domain.Settings;
using Infrastructure.Identity.Contexts;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Infrastructure.Persistence.utils
{
    public class UserManager : IUserManager
    {
        private LangCodeApiContext _langCodeApiContext;
        private readonly JWTSettings _jwtSettings;

        public UserManager(LangCodeApiContext langCodeApiContext, JWTSettings jwtSettings)
        {
            _langCodeApiContext = langCodeApiContext;
            _jwtSettings = jwtSettings;
        }

        public void AddToRole(AspNetUsers user, string roleCode)
        {
            try
            {
                AspNetRoles role = _langCodeApiContext.AspNetRoles.FirstOrDefault(x => x.Name.ToLower() == roleCode.ToLower());
                if (role == null)
                {
                    return;
                }
                this.AddToRole(user, role);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddToRole(AspNetUsers user, AspNetRoles role)
        {
            try
            {
                AspNetUserRoles userRoles = _langCodeApiContext.AspNetUserRoles.Find(user.Id, role.Id);
                if (userRoles != null)
                {
                    return;
                }
                userRoles = new AspNetUserRoles();
                userRoles.UserId = user.Id;
                userRoles.RoleId = role.Id;
                _langCodeApiContext.AspNetUserRoles.Add(userRoles);
                _langCodeApiContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User GetCurrentUser(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        public int GetCurrentUserId(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromRole(AspNetUsers user, string roleCode)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromRole(AspNetUsers user, AspNetRoles role)
        {
            throw new NotImplementedException();
        }

        public Task SignIn(HttpContext httpContext, AspNetUsers user, bool isPersistent = false)
        {
            throw new NotImplementedException();
        }

        public Task SignOut(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        public SignUpResult SignUp(string name, string credentialTypeCode, string identifier)
        {
            throw new NotImplementedException();
        }

        public SignUpResult SignUp(string name, string credentialTypeCode, string identifier, string secret)
        {
            throw new NotImplementedException();
        }


        private async Task<JwtSecurityToken> GenerateJWToken(AspNetUsers user)
        {

            try
            {
                //var roleClaims = await _accountStore.GetClaims(user.Id);
                //var userClaims = await _accountStore.GetUserClaims(user.Id);

                //var roles = await _accountStore.GetRoles(user.Id);
                //string ipAddress = IpHelper.GetIpAddress();

                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
           
                //new Claim("ip", ipAddress)
            };
                //.Union(roleClaims).Union(roles).Union(userClaims);
                //


                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                    signingCredentials: signingCredentials);

                return jwtSecurityToken;
            }
            catch (System.Exception ex)
            {

                Log.Warning("Error Function GenerateJWToken with error ", ex);
                throw ex;

            }
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        AspNetUsers IUserManager.GetCurrentUser(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }
    }
}
