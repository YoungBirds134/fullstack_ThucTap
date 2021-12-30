using System;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Account;
using Application.Interfaces;
using Application.Wrappers;

namespace Infrastructure.Persistence.Repositories
{
    public class AccountLangRepository : IAccountLangService
    {
        public AccountLangRepository()
        {
        }

        public Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> ConfirmEmailAsync(string userId, string code)
        {
            throw new NotImplementedException();
        }

        public Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> getUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> ResetPassword(ResetPasswordRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
