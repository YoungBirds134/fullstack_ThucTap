using System;
using System.Threading.Tasks;
using AutoMapper;

using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Security.Claims;

using Application.DTOs;
using Infrastructure.Identity.Contexts;

namespace Infrastructure.Identity.Store
{
    public class AccountStore 
    {
        private readonly IConfiguration _configuration;
        string connection = "";
        private readonly IMapper _iMapper;
        private readonly identityDbContext _identityDbContext;
        public AccountStore(IConfiguration configuration, IMapper iMapper, identityDbContext identityDbContext)
        {
            _configuration = configuration;
            _iMapper = iMapper;
            connection = _configuration["ConnectionStrings:IdentityConnection"];
            _identityDbContext = identityDbContext;
        }

        public async Task<User> FindByEmail(string eMail) {
            try
            {
                var result = _identityDbContext.User.Where(x=>x.Email==eMail).Single();
                if (result==null)
                {
                    return null;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> LoginUser(string emailUser,string  Password) {
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@userMail ", emailUser, DbType.String, ParameterDirection.Input);
                parameter.Add("@userPassWord", emailUser, DbType.String, ParameterDirection.Input);

                using (var con = new SqlConnection(connection))
                {
                    con.Open();


                    var result =  con.Execute("getLoginUser", parameter, commandType: CommandType.StoredProcedure);
                    if (result==null)
                    {
                        return false;
                    }


                    return true;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Claim>> GetRoles(string idUser)
        {
            try
            {
                var roles = from role in _identityDbContext.Role select role;
                var result = (from ur in _identityDbContext.UserRoles
                              join r in roles on ur.RoleId equals r.Id
                             where ur.UserId == idUser
                              select new { RoleName = r.Name }).ToList() ;

                if (result==null)
                {
                    return null;
                }
                var roleClaims = new List<Claim>();
               


                for (int i = 0; i < result.Count; i++)
                {
                    roleClaims.Add(new Claim(ClaimTypes.Role, result[i].RoleName));
                }
                return roleClaims;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Claim>> GetClaims(string idUser)
        {
            try
            {
                
                var result = (from r in _identityDbContext.Role
                              from rc in _identityDbContext.RoleClaims
                              join ur in _identityDbContext.UserRoles on r.Id equals ur.RoleId
                            where rc.RoleId== r.Id && ur.UserId == idUser
                    
                              select new { ClaimType = rc.ClaimType,ClaimValue=rc.ClaimValue }).ToList();

                if (result == null)
                {
                    return null;
                }
                var roleClaims = new List<Claim>();
             


                for (int i = 0; i < result.Count; i++)
                {
                    roleClaims.Add(new Claim(result[i].ClaimType, result[i].ClaimValue));
                }
                return roleClaims;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 public async Task<List<Claim>> GetUserClaims(string idUser)
        {
            try
            {
                
                var result = (from u in _identityDbContext.User              
                              join ur in _identityDbContext.UserClaims on u.Id equals ur.UserId       
                              select new { ClaimType = ur.ClaimType,ClaimValue=ur.ClaimValue }).ToList();

                if (result == null)
                {
                    return null;
                }
                var roleClaims = new List<Claim>();
             


                for (int i = 0; i < result.Count; i++)
                {
                    roleClaims.Add(new Claim(result[i].ClaimType, result[i].ClaimValue));
                }
                return roleClaims;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
public async Task<UserDTO> getUser(string userId){
    try
    {
       return  _iMapper.Map<UserDTO>(_identityDbContext.User.SingleOrDefault(x=>x.Id == userId));
    }
    catch (System.Exception ex)
    {
         // TODO
         throw ex;
    }
}

        public async Task<bool> CreateAccount(UserDTO users, string passwordUser) {
            try
            {
                var user = _iMapper.Map<UserDTO,User>(users);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //  public async Task<bool> AddToRole(UserDTO users, string roles) {
        //     try
        //     {
        //     //     var user = _iMapper.Map<UserDTO,User>(users);
        //     //     var result =  _identityDbContext.User.Where(x => x.Email==user.Email);
        //     //     var userrole = new UserRole{
        //     //         RoleId
        //     //     };
        //     //     var addRole = _identityDbContext.UserRoles.Add();
        //     //   return true;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        // }
    }
}
