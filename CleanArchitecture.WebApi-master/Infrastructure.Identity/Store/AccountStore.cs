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
using System.Security.Cryptography;
using System.Text;
using Infrastructure.Identity.Models;

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
                var result = _identityDbContext.User.Where(x => x.Email == eMail).FirstOrDefault();
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

        public async Task<bool> FindByNameAsync(string userName)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@userName", userName, DbType.String, ParameterDirection.Input);
                using (var con = new SqlConnection(connection))
                {
                    con.Open();

                    var result = con.Execute("getUserName", parameter, commandType: CommandType.StoredProcedure);
                    if (result == null)
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

        public async Task<string> CreateUser(Models.ApplicationUser request, string Password)
        {
            try
            {
                request.PasswordHash = EncodePassword(Password);

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@userId", request.Id, DbType.String, ParameterDirection.Input);
                parameter.Add("@userName", request.UserName, DbType.String, ParameterDirection.Input);
                parameter.Add("@userNormalName", request.NormalizedUserName, DbType.String, ParameterDirection.Input);
                parameter.Add("@userEmail", request.Email, DbType.String, ParameterDirection.Input);
                parameter.Add("@userNormalEmail", request.NormalizedEmail, DbType.String, ParameterDirection.Input);
                parameter.Add("@userEmailconfig", request.EmailConfirmed, DbType.String, ParameterDirection.Input);
                parameter.Add("@userPassHash", request.PasswordHash, DbType.String, ParameterDirection.Input);
                parameter.Add("@userSecurity", request.SecurityStamp, DbType.String, ParameterDirection.Input);
                parameter.Add("@userConcuren", request.ConcurrencyStamp, DbType.String, ParameterDirection.Input);
                parameter.Add("@userPhone", request.PhoneNumber, DbType.String, ParameterDirection.Input);
                parameter.Add("@userPhoneConfirm", request.PhoneNumberConfirmed, DbType.String, ParameterDirection.Input);
                parameter.Add("@userTwo", request.TwoFactorEnabled, DbType.String, ParameterDirection.Input);
                parameter.Add("@userLockou", request.LockoutEnabled, DbType.String, ParameterDirection.Input);
                parameter.Add("@userAccess", request.AccessFailedCount, DbType.String, ParameterDirection.Input);
                parameter.Add("@userFname", request.FirstName, DbType.String, ParameterDirection.Input);
                parameter.Add("@userLname", request.LastName, DbType.String, ParameterDirection.Input);
                using (var con = new SqlConnection(connection))
                {
                    con.Open();

                    var result = con.Execute("insertUser", parameter, commandType: CommandType.StoredProcedure);
                    if (result == null)
                    {
                        return "er";
                    }
                }
                return "Succeeded";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string EncodePassword(string originalPassword)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }
        internal void AddToRole(ApplicationUser user, string v)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@userId", user.Id, DbType.String, ParameterDirection.Input);
                parameter.Add("@userIRole", v, DbType.String, ParameterDirection.Input);
                using (var con = new SqlConnection(connection))
                {
                    con.Open();
                    var result = con.Execute("getRoleUser", parameter, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
