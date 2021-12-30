using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using Dapper;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Store
{
    public class ProductLangStore
    {
        private readonly IConfiguration _configuration;
        string connection = "";
        private readonly IMapper _iMapper;
        private readonly LangCodeApiContext _langCodeApiContext;
        public ProductLangStore(IConfiguration configuration,IMapper iMapper, LangCodeApiContext langCodeApiContext)
        {
            _configuration = configuration;
            _iMapper = iMapper;
            connection = _configuration["ConnectionStrings:LangCodeApiConnection"];
            _langCodeApiContext = langCodeApiContext;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProduct(string nameLanguage)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@language", nameLanguage, DbType.String, ParameterDirection.Input);
                using (var con = new SqlConnection(connection))
                {
                    con.Open();


                    var product = await con.QueryAsync<Product>("GetAllProducts", parameter,commandType: CommandType.StoredProcedure);


                    con.Close();
                    return _iMapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(product);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
