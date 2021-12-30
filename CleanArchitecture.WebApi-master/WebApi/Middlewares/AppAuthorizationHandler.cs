using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity.Contexts;

using Application.DTOs;
using System;

namespace WebApi.Extensions.Required
{
    public class AppAuthorizationHandler : IAuthorizationHandler
    {
      
        private readonly UserManager<User> _userManager;
        public AppAuthorizationHandler(IAccountService accountService, UserManager<User> userManager)
        {
            
            _userManager = userManager;
        }
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var requirements = context.PendingRequirements.ToList();
            foreach (var requirement in requirements)
            {
                if (requirement is PositionNameRequirement)
                {
                    if (IsUserNameRequirement(context.User, (PositionNameRequirement)requirement))
                    {
                        context.Succeed(requirement);
                    }
                }
            }
            return Task.CompletedTask;
        }

        public bool IsUserNameRequirement(ClaimsPrincipal user, PositionNameRequirement requirement)
        {
           
            try
            {
    
                 var test = user.FindFirst(c=>c.Type== ClaimTypes.NameIdentifier);

                if (test == null)
                {
                    return false;
                    
                }
                if (test.Value == requirement.PositionName)
                {
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
    }
}