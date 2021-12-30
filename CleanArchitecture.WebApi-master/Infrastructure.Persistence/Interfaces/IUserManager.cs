using System;
using System.Threading.Tasks;
using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Persistence.Interfaces
{
    public enum SignUpResultError
    {
        CredentialTypeNotFound
    }
    public class SignUpResult
    {
        public AspNetUsers User { get; set; }
        public bool Success { get; set; }
        public SignUpResultError? Error { get; set; }

        public SignUpResult(AspNetUsers user = null, bool success = false, SignUpResultError? error = null)
        {
            this.User = user;
            this.Success = success;
            this.Error = error;
        }
    }
    public interface IUserManager
    {
        SignUpResult SignUp(string name, string credentialTypeCode, string identifier);
        SignUpResult SignUp(string name, string credentialTypeCode, string identifier, string secret);
        void AddToRole(AspNetUsers user, string roleCode);
        void AddToRole(AspNetUsers user, AspNetRoles role);
        void RemoveFromRole(AspNetUsers user, string roleCode);
        void RemoveFromRole(AspNetUsers user, AspNetRoles role);
       
        Task SignIn(HttpContext httpContext, AspNetUsers user, bool isPersistent = false);
        Task SignOut(HttpContext httpContext);
        int GetCurrentUserId(HttpContext httpContext);
        AspNetUsers GetCurrentUser(HttpContext httpContext);
    }
}
