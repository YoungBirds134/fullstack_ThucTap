using Microsoft.AspNetCore.Authorization;
namespace WebApi.Extensions.Required
{
    public class PositionNameRequirement : IAuthorizationRequirement
    {
        public string PositionName { get; set; }
        public PositionNameRequirement(string positionName="SalesCar")
        {
            PositionName = positionName;
        }
    }
}