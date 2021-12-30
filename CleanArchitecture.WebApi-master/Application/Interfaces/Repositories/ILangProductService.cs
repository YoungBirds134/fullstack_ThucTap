using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces.Repositories
{
    public interface ILangProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProduct(string nameLanguage);
        Task<ProductDTO> GetProductByID(int idProduct);
        Task<ProductDTO> CreateProduct(ProductDTO request);
        Task<bool> DeleteProductByID(int idProduct);
        Task<bool> UpdateProduct(int id, ProductDTO request);
    }
}
