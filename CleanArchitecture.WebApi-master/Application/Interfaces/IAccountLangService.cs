using System;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Account;
using Application.Wrappers;

namespace Application.Interfaces
{
    // Of LangCodeApiDB
    public interface IAccountLangService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<Response<string>> ConfirmEmailAsync(string userId, string code);
        Task ForgotPassword(ForgotPasswordRequest model, string origin);
        Task<Response<string>> ResetPassword(ResetPasswordRequest model);

        Task<UserDTO> getUser(string userId);
    }
}
