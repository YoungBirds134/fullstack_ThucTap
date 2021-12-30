using System;
using System.Threading.Tasks;
using Application.DTOs.Account;
using Application.Wrappers;
using AutoMapper;
using Infrastructure.Persistence.Contexts;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Store
{
    public class AccountLangStore 
    {
        private readonly IConfiguration _configuration;
        string connection = "";
        private readonly IMapper _iMapper;
        private readonly LangCodeApiContext _langCodeApiContext;
        public AccountLangStore(IConfiguration configuration, IMapper iMapper, LangCodeApiContext langCodeApiContext)
        {
            _configuration = configuration;
            _iMapper = iMapper;
            connection = _configuration["ConnectionStrings:LangCodeApiConnection"];
            _langCodeApiContext = langCodeApiContext;
        }


        public Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
