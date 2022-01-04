using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Account;
using Application.Wrappers;
using AutoMapper;
using Dapper;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.StoreProceduce
{
    public class ProductStore
    {
        private readonly IConfiguration _configuration;
        string connection = "";
        private readonly IMapper _iMapper;
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductStore(IConfiguration configuration, IMapper iMapper, ApplicationDbContext applicationDbContext)
        {
            _configuration = configuration;
            _iMapper = iMapper;
            connection = _configuration["ConnectionStrings:DefaultConnection"];
            _applicationDbContext = applicationDbContext;

        }

        public async Task<bool> DeleteProduct(int idProduct)
        {

            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@idProduct", idProduct, DbType.Int32, ParameterDirection.Input);
                using (var con = new SqlConnection(connection))
                {
                    con.Open();


                    var result = await con.ExecuteAsync("getDeleteProduct", parameter, commandType: CommandType.StoredProcedure);
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
                return false;
            }
        }


        public async Task<IEnumerable<ProductDTO>> GetAllProduct()
        {
            try
            {
                using (var con = new SqlConnection(connection))
                {
                    con.Open();


                    var product = await con.QueryAsync<Product>("getAllProducts", commandType: CommandType.StoredProcedure);


                    con.Close();
                    return _iMapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(product);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductDTO> CreateProduct(ProductDTO request)
        {
            try
            {
                var product = _iMapper.Map<Product>(request);
                _applicationDbContext.Products.Add(product);
                _applicationDbContext.SaveChanges();
                return request;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateProduct(int id, ProductDTO request)
        {
            try
            {
                var product = _iMapper.Map<Product>(request);
                var result = _applicationDbContext.Products.Single(x => x.Id == id);
                result.Barcode = product.Barcode;
                result.Name = product.Name;
                result.Rate = product.Rate;
                result.Description = product.Description;
                await _applicationDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<MenuDTO>> GetAllMenus()
        {
            try
            {
                var result = _applicationDbContext.Menu.ToList();
                return _iMapper.Map<IEnumerable<Menu>, IEnumerable<MenuDTO>>(result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<LanguagesDTO>> GetTranslationsMenu(string languages)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@languages", languages, DbType.String, ParameterDirection.Input);
                using (var con = new SqlConnection(connection))
                {
                    con.Open();


                    var product = await con.QueryAsync<Language>("getLanguagesTranslations", parameter, commandType: CommandType.StoredProcedure);


                    con.Close();
                    return _iMapper.Map<IEnumerable<Language>, IEnumerable<LanguagesDTO>>(product);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PagedResponse<IEnumerable<ProductPagingDTO>>> GetProductPaging(int numberOfPages, int pageSize)
        {
            try
            {
                // string orderByTable= orderBy.name+" "+orderBy.type;
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@PageNumber", numberOfPages, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@PageSize", pageSize, DbType.Int32, ParameterDirection.Input);
                // parameter.Add("@OrderBy", orderByTable, DbType.String, ParameterDirection.Input);


                using (var con = new SqlConnection(connection))
                {
                    con.Open();


                    var product = await con.QueryAsync<Product>("getPaging_2", parameter, commandType: CommandType.StoredProcedure);
                    var totalRow = _applicationDbContext.Products.Count();
                    var totalPages = (int)Math.Ceiling((double)totalRow / pageSize);
                    var productMapped = _iMapper.Map<IEnumerable<Product>, IEnumerable<ProductPagingDTO>>(product);
                    return new PagedResponse<IEnumerable<ProductPagingDTO>>(productMapped, numberOfPages, pageSize, totalRow, totalPages);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PagedResponse<IEnumerable<ProductPagingDTO>>> GetProductPaging_Order(ProductViewModel_OrderBy productViewModel_OrderBy)
        {
            try
            {

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@PageNumber", productViewModel_OrderBy.numberPage, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@PageSize", productViewModel_OrderBy.pageSize, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@OrderByNameColumn", productViewModel_OrderBy.nameColumnOrderBy, DbType.String, ParameterDirection.Input);
                parameter.Add("@SortOrder", productViewModel_OrderBy.typeColumnOrderBy, DbType.String, ParameterDirection.Input);



                using (var con = new SqlConnection(connection))
                {
                    con.Open();


                    var product = await con.QueryAsync<Product>("getPaging_OrderBy_2", parameter, commandType: CommandType.StoredProcedure);
                    var productMapped = _iMapper.Map<IEnumerable<Product>, IEnumerable<ProductPagingDTO>>(product);
                    var totalRow = _applicationDbContext.Products.Count();
                    var totalPages = (int)Math.Ceiling((double)totalRow / productViewModel_OrderBy.pageSize);

                    return new PagedResponse<IEnumerable<ProductPagingDTO>>(productMapped, productViewModel_OrderBy.numberPage, productViewModel_OrderBy.pageSize, totalRow, totalPages);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PagedResponse<IEnumerable<ProductPagingDTO>>> GetProductPaging_Filter(ProductViewModel_Filter productViewModel_Filter)
        {
            try
            {

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@PageNumber", productViewModel_Filter.numberPage, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@PageSize", productViewModel_Filter.pageSize, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@NameColumn", productViewModel_Filter.nameColumnFilter, DbType.String, ParameterDirection.Input);
                parameter.Add("@keyName", productViewModel_Filter.valueColumnFilter, DbType.String, ParameterDirection.Input);



                using (var con = new SqlConnection(connection))
                {
                    con.Open();


                    var product = await con.QueryAsync<Product>("getPaging_Filter_2", parameter, commandType: CommandType.StoredProcedure);
                    var productMapped = _iMapper.Map<IEnumerable<Product>, IEnumerable<ProductPagingDTO>>(product);
                    var totalRow = _applicationDbContext.Products.Count();
                    var totalPages = (int)Math.Ceiling((double)totalRow / productViewModel_Filter.pageSize);

                    return new PagedResponse<IEnumerable<ProductPagingDTO>>(productMapped, productViewModel_Filter.numberPage, productViewModel_Filter.pageSize, totalRow, totalPages);


                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PagedResponse<IEnumerable<ProductPagingDTO>>> GetProductPaging_Order_Filter(ProductViewModel productViewModel)
        {
            try
            {

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@PageNumber", productViewModel.numberPage, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@PageSize", productViewModel.pageSize, DbType.Int32, ParameterDirection.Input);
                
                    parameter.Add("@OrderByNameColumn", productViewModel.nameColumnOrderBy, DbType.String, ParameterDirection.Input);
                    parameter.Add("@SortOrder", productViewModel.typeColumnOrderBy, DbType.String, ParameterDirection.Input);

                    parameter.Add("@NameColumn", productViewModel.nameColumnFilter, DbType.String, ParameterDirection.Input);
                    parameter.Add("@keyName", productViewModel.valueColumnFilter, DbType.String, ParameterDirection.Input);

                


                using (var con = new SqlConnection(connection))
                {
                    con.Open();


                    var product = await con.QueryAsync<Product>("getPaging_OrderBy_Filter_2", parameter, commandType: CommandType.StoredProcedure);
                    var productMapped = _iMapper.Map<IEnumerable<Product>, IEnumerable<ProductPagingDTO>>(product);
                    var totalRow = _applicationDbContext.Products.Count();
                    var totalPages = (int)Math.Ceiling((double)totalRow / productViewModel.pageSize);

                    return new PagedResponse<IEnumerable<ProductPagingDTO>>(productMapped, productViewModel.numberPage, productViewModel.pageSize, totalRow, totalPages);


                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


             public async Task<PagedResponse<IEnumerable<ProductPagingDTO>>> GetProductPaging_Filter_Multi_Param(ProductViewModel_Filter_Multi productViewModel_Filter_Multi)
        {
           
            try
            {

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@PageNumber", productViewModel_Filter_Multi.numberPage, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@PageSize", productViewModel_Filter_Multi.pageSize, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@Name", productViewModel_Filter_Multi.nameSearch, DbType.String, ParameterDirection.Input);
                parameter.Add("@Barcode", productViewModel_Filter_Multi.barcodeSearch, DbType.String, ParameterDirection.Input);
                parameter.Add("@Description", productViewModel_Filter_Multi.descriptionSearch, DbType.String, ParameterDirection.Input);

                parameter.Add("@NameOrderBy", productViewModel_Filter_Multi.nameOrderBy, DbType.String, ParameterDirection.Input);
                parameter.Add("@BarcodeOrderBy", productViewModel_Filter_Multi.barcodeOrderBy, DbType.String, ParameterDirection.Input);
                parameter.Add("@DescriptionOrderBy", productViewModel_Filter_Multi.descriptionOrderBy, DbType.String, ParameterDirection.Input);
                parameter.Add("@RateOrderBy", productViewModel_Filter_Multi.rateOrderBy, DbType.String, ParameterDirection.Input);







                using (var con = new SqlConnection(connection))
                {
                    con.Open();


                    var product = await con.QueryAsync<Product>("getPaging_OrderBy_Filter_Multi_Param", parameter, commandType: CommandType.StoredProcedure);
                    var productMapped = _iMapper.Map<IEnumerable<Product>, IEnumerable<ProductPagingDTO>>(product);
                    var totalRow = _applicationDbContext.Products.Count();
                    var totalPages = (int)Math.Ceiling((double)totalRow / productViewModel_Filter_Multi.pageSize);

                    return new PagedResponse<IEnumerable<ProductPagingDTO>>(productMapped, productViewModel_Filter_Multi.numberPage, productViewModel_Filter_Multi.pageSize, totalRow, totalPages);


                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PagedResponse<IEnumerable<MenuDTO>>> GetMenu()
        {
            try
            {
                var menu = _applicationDbContext.Menu.ToList();
               
                using (var con = new SqlConnection(connection))
                {
                    con.Open();
                    var sql = con.Query<Menu>("Select * From Menu").ToList();
                    var map = _iMapper.Map<IEnumerable<Menu>, IEnumerable<MenuDTO>>(sql);


                    return new PagedResponse<IEnumerable<MenuDTO>>(getMenuTree(map,null),0,0,0,0);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<MenuDTO> getMenuTree(IEnumerable<MenuDTO> list,string? parentID) {
            try
            {
                return list.Where(x => x.parentID == parentID).Select(k => new MenuDTO() {
                    id = k.id,
                    nameMenu=k.nameMenu,
                    nameComponent=k.nameComponent,
                    route=k.route,
                    parentID=k.parentID,
                    active=k.active,
                    listMenu = getMenuTree(list, k.id),
                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
