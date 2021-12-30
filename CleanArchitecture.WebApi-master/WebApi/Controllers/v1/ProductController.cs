using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Filters;
using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.v1
{
    //[MiddlewareFilter(typeof(LocalizationPipeline))]
    public class ProductController : BaseApiController
    {

        private readonly IProductService _productService;


        public ProductController(IProductService productService)
        {
          
            _productService = productService;
        }
        // GET: api/<controller>
       
        [HttpGet("GetAllProducts")]
        [Authorize(Policy = "ReadProduct")]
        public async Task<IEnumerable<ProductDTO>> GetAllProduct()
        {

            return await _productService.GetAllProduct();

        }

        // GET api/<controller>/5
        [HttpGet("GetProductByID/{id}")]
        [Authorize(Policy = "ReadProduct")]
        public async Task<ProductDTO> GetProductByID(int idProduct)
        {
            return await _productService.GetProductByID(idProduct);

        }

        // POST api/<controller>

        [HttpPost("CreateProduct")]
        [Authorize(Policy = "CreateProduct")]
        public async Task<ProductDTO> Post(ProductDTO product)
        {
            return await _productService.CreateProduct(product);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "UpdateProduct")]
        public async Task<ActionResult<dynamic>> Put(int id, ProductDTO product)
        {
            var result = await _productService.UpdateProduct(id,product);
            string message = "Resquest Succcess";

            if (!result)
            {
                return NotFound(new { message = "Product invalid" });

            }
            return message;
        }

        // DELETE api/<controller>/5

        [HttpDelete("DeleteProduct/{id}")]
        [Authorize(Policy = "DeleteProduct")]
        public async Task<ActionResult<dynamic>> DeleteProduct(int id)
        {
            var result = _productService.DeleteProductByID(id);
            string message = "";
            if (result == null)
            {
                return NotFound(new { message = "Id invalid" });

            }
            message = "Product Deleted";
            return message;
        }

        // GET: api/<controller>

        [HttpGet("GetAllMenus")]
        [AllowAnonymous]
        public async Task<IEnumerable<MenuDTO>> GetAllMenus()
        {

            return await _productService.GetAllMenus();

        }

        // GET: api/<controller>

        [HttpGet("GetAllMenusByLanguages/{languages}")]
        [AllowAnonymous]
        public async Task<IEnumerable<LanguagesDTO>> GetAllMenusByLanguages(string languages)
        {

            return await _productService.GetTranslationsMenu(languages);

        }



       
    }
}
