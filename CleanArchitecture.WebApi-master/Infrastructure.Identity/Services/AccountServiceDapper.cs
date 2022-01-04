using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Account;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Settings;
using Infrastructure.Identity.Contexts;
using Serilog;


using Infrastructure.Identity.Store;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Identity.ultils;

namespace Infrastructure.Identity.Services
{
    public class AccountServiceDapper : IAccountService
    {


        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;
        private readonly AccountStore _accountStore;
        private readonly EncryptionHash _encryptionHash;

 



        public AccountServiceDapper(
            IOptions<JWTSettings> jwtSettings,
            IDateTimeService dateTimeService,
            IEmailService emailService, AccountStore accountStore,EncryptionHash encryptionHash)
        {
            _accountStore = accountStore;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
           _encryptionHash=encryptionHash;
            this._emailService = emailService;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            
            try
            {
                var user = await _accountStore.FindByEmail(request.Email);
                if (user == null)
                {
                    throw new ApiException($"No Accounts Registered with {request.Email}.");
                    
                }
                var result = await _accountStore.LoginUser(user.Email, user.PasswordHash);
                if (!result)
                {
                    throw new ApiException($"Invalid Credentials for '{request.Email}'.");
                }
                if (!user.EmailConfirmed)
                {
                    throw new ApiException($"Account Not Confirmed for '{request.Email}'.");
                }
                JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
                AuthenticationResponse response = new AuthenticationResponse();
                // response.Id = user.Id;
                response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                
                // response.Roles = await _accountStore.GetRoles(user.Id);
                // response.roleClaim = await _accountStore.GetClaims(user.Id);
                // response.userClaim = await _accountStore.GetUserClaims(user.Id);
                response.IsVerified = user.EmailConfirmed;
                var refreshToken = GenerateRefreshToken(ipAddress);
                response.RefreshToken = refreshToken.Token;
                Log.Information("User Login Success {user} Function {name}",user.UserName, "AuthenticateAsync");
                return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}");
                
              
            }
            catch (Exception ex)
            {
                Log.Warning("Error User Login {ex}", ex);
                throw ex;

            }
        }

        public Task<Response<string>> ConfirmEmailAsync(string userId, string code)
        {
            throw new NotImplementedException();
        }

        public Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            try
            {
                var userCheck = await _accountStore.FindByEmail(request.Email);
                if (userCheck == null)
                {
                throw new ApiException($"Username '{request.UserName}' is already taken.");
                }
                var user = new UserDTO
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName
            };
            var passwordUser = _encryptionHash.Encrypt(request.Password);
            if (userCheck  == null)
            {
                var result = await _accountStore.CreateAccount(user, passwordUser);
              
                if (result)
                {
                    // await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                    // var verificationUri = await SendVerificationEmail(user, origin);
                    // //TODO: Attach Email Service here and configure it via appsettings
                    // await _emailService.SendAsync(new Application.DTOs.Email.EmailRequest() { From = "mail@codewithmukesh.com", To = user.Email, Body = $"Please confirm your account by visiting this URL {verificationUri}", Subject = "Confirm Registration" });
                    // return new Response<string>(user.Id, message: $"User Registered. Please confirm your account by visiting this URL {verificationUri}");
                }
                else
                {
                    // throw new ApiException($"{result.Errors}");
                    return null;
                }
            }
            else
            {
                throw new ApiException($"Email {request.Email } is already registered.");
            }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Response<string>> ResetPassword(ResetPasswordRequest model)
        {
            throw new NotImplementedException();
        }

        private async Task<JwtSecurityToken> GenerateJWToken(User user)
        {
            
            try
            {
                 var roleClaims = await _accountStore.GetClaims(user.Id);
            var userClaims = await _accountStore.GetUserClaims(user.Id);

            var roles = await _accountStore.GetRoles(user.Id);
            //string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("positionName",user.PositionName),
                new Claim("uid", user.Id),
                //new Claim("ip", ipAddress)
            }.Union(roleClaims).Union(roles).Union(userClaims);
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
                
                Log.Warning("Error Function GenerateJWToken with error ",ex);
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

        public async Task<UserDTO> getUser(string userId)
        {
          try
          {
            return await _accountStore.getUser(userId);
          }
          catch (System.Exception ex)
          {
               // TODO
               throw ex;
          }
        }
    }
}
