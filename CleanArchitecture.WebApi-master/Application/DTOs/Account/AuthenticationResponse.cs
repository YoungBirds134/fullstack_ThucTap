using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Application.DTOs.Account
{
    public class AuthenticationResponse
    {
        //public string Id { get; set; }
        //public string UserName { get; set; }
        //public string Email { get; set; }
        //public List<Claim> Roles { get; set; }
        //public List<Claim> roleClaim { get; set; }
        //public List<Claim> userClaim { get; set; }

        public bool IsVerified { get; set; }
        public string JWToken { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
