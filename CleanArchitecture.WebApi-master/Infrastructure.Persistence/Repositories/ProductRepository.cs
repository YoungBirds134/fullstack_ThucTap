using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
//using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Models;

using System.Linq;
using Infrastructure.Persistence.StoreProceduce;
using Infrastructure.Persistence.Contexts;
using Application.DTOs.Account;
using Application.Wrappers;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductService
    {
        private readonly IMapper _iMapper;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ProductStore _productStore;



        public ProductRepository(IMapper iMapper, ApplicationDbContext applicationDbContext, ProductStore productStore)
        {
            _iMapper = iMapper;
            _applicationDbContext = applicationDbContext;
            _productStore = productStore;
        }

        public async Task<bool> DeleteProductByID(int idProduct)
        {

            try
            {
                bool result = await _productStore.DeleteProduct(idProduct);
                if (result)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProduct()
        {
            try
            {
                return await _productStore.GetAllProduct();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductDTO> GetProductByID(int idProduct)
        {
            try
            {

                var product = _applicationDbContext.Products.Where(x => x.Id == idProduct).First();

                return _iMapper.Map<Product, ProductDTO>(product);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return null;
        }

        public async Task<bool> UpdateProduct(int id, ProductDTO request)
        {
            try
            {
                var result = await _productStore.UpdateProduct(id, request);
                if (result)
                {
                    return true;

                }
                return false;
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
                var result = await _productStore.CreateProduct(request);
                if (result == null)
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

        public Task<IEnumerable<MenuDTO>> GetAllMenus()
        {
            try
            {
                return _productStore.GetAllMenus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<IEnumerable<LanguagesDTO>> GetTranslationsMenu(string languages)
        {
            try
            {
                return _productStore.GetTranslationsMenu(languages);
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
                return await _productStore.GetProductPaging(numberOfPages, pageSize);
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
                return await _productStore.GetProductPaging_Order(productViewModel_OrderBy);
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
                return await _productStore.GetProductPaging_Filter(productViewModel_Filter);
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
                return await _productStore.GetProductPaging_Order_Filter(productViewModel);
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
                return await _productStore.GetProductPaging_Filter_Multi_Param(productViewModel_Filter_Multi);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
