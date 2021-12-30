using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    public class ProductPaging : BaseApiController
    {
        private readonly ILangProductService _langProductService;

        private readonly IProductService _productService;
        public ProductPaging(ILangProductService langProductService, IProductService productService)
        {
            _langProductService = langProductService;
            _productService = productService;

        }

        // GET api/<controller>/5
        [HttpGet("GetProductByPaging")]
        [AllowAnonymous]
        public async Task<PagedResponse<IEnumerable<ProductPagingDTO>>> GetProductByPaging(int numberPage, int pageSize)
        {
            return await _productService.GetProductPaging(numberPage, pageSize);

        }

        // Post api/<controller>/
        [HttpPost("GetProductByPaging_OrderBy")]
        [AllowAnonymous]
        public async Task<PagedResponse<IEnumerable<ProductPagingDTO>>> GetProductByPaging_OrderBy(ProductViewModel_OrderBy productViewModel_OrderBy)
        {
            return await _productService.GetProductPaging_Order(productViewModel_OrderBy);

        }

        // Post api/<controller>/
        [HttpPost("GetProductByPaging_FilterBy")]
        [AllowAnonymous]
        public async Task<PagedResponse<IEnumerable<ProductPagingDTO>>> GetProductByPaging_FilterBy(ProductViewModel_Filter productViewModel_Filter)
        {
            return await _productService.GetProductPaging_Filter(productViewModel_Filter);

        }

        // Post api/<controller>/
        [HttpPost("GetProductByPaging_OrderBy_FilterBy")]
        [AllowAnonymous]
        public async Task<PagedResponse<IEnumerable<ProductPagingDTO>>> GetProductByPaging_OrderBy_FilterBy(ProductViewModel productViewModel)
        {
            return await _productService.GetProductPaging_Order_Filter(productViewModel);

        }

        // Post api/<controller>/
        [HttpPost("GetProductByPaging_FilterBy_Multi_Param")]
        [AllowAnonymous]
        public async Task<PagedResponse<IEnumerable<ProductPagingDTO>>> GetProductByPaging_FilterBy_Multi(ProductViewModel_Filter_Multi productViewModel_Filter_Multi)
        {
            return await _productService.GetProductPaging_Filter_Multi_Param(productViewModel_Filter_Multi);
        }

    }
}