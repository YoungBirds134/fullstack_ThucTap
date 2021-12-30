using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NLog.Fluent;
using WebApi.Extensions;

namespace WebApi.Controllers.v1
{
    [Route("api/{culture}/[controller]")]
    [MiddlewareFilter(typeof(LocalizationExtensions))]

    public class ProductLangController : BaseApiController
    {
        private readonly ILangProductService _langProductService;

        private readonly IProductService _productService;



        public ProductLangController(ILangProductService langProductService, IProductService productService)
        {
            _langProductService = langProductService;
            _productService = productService;


        }

        // GET: api/<controller>

        [HttpGet("GetAllLangProducts/")]
        //[Authorize(Policy = "ReadProduct")]
        public async Task<IEnumerable<ProductDTO>> GetAllProduct()
        {
            try
            {
                var culture = CultureInfo.CurrentCulture.Name;

                return await _langProductService.GetAllProduct(culture);


            }
            catch (Exception ex)
            {
                Log.Error("Api Get All Product error " + ex);
                throw ex;
            }

        }

      



    }
}
