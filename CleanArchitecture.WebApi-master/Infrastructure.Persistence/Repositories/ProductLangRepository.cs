using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Store;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductLangRepository : ILangProductService
    {
        private readonly ProductLangStore _productLangStore;
        public ProductLangRepository(ProductLangStore productLangStore)
        {
            _productLangStore = productLangStore;
        }

        public Task<ProductDTO> CreateProduct(ProductDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProductByID(int idProduct)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDTO>> GetAllProduct(string nameLanguage)
        {
            try
            {
                return _productLangStore.GetAllProduct(nameLanguage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ProductDTO> GetProductByID(int idProduct)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProduct(int id, ProductDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
