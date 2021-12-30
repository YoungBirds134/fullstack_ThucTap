using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Wrappers;

namespace Application.Interfaces.Repositories
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProduct();
        Task<ProductDTO> GetProductByID(int idProduct);
        Task<ProductDTO> CreateProduct(ProductDTO request);
        Task<bool> DeleteProductByID(int idProduct);
        Task<bool> UpdateProduct(int id,ProductDTO request);

        //Multi-Language
        Task<IEnumerable<MenuDTO>> GetAllMenus();
        Task<IEnumerable<LanguagesDTO>> GetTranslationsMenu(string languages);


        //Paging
        Task<PagedResponse<IEnumerable<ProductPagingDTO>>> GetProductPaging(int numberOfPages,int pageSize);
        //Paging_Order
        Task<PagedResponse<IEnumerable<ProductPagingDTO>>> GetProductPaging_Order(ProductViewModel_OrderBy productViewModel_OrderBy);
        //Paging_Filter
        Task<PagedResponse<IEnumerable<ProductPagingDTO>>> GetProductPaging_Filter(ProductViewModel_Filter productViewModel_Filter);
        //Paging_Oder_Filter
        Task<PagedResponse<IEnumerable<ProductPagingDTO>>> GetProductPaging_Order_Filter(ProductViewModel productViewModel);
        //Paging_Filter_Multi_Param
        Task<PagedResponse<IEnumerable<ProductPagingDTO>>> GetProductPaging_Filter_Multi_Param(ProductViewModel_Filter_Multi productViewModel_Filter_Multi);


    }
}
