using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.Dtos;
using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // BaseUrl/api/Products
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        #region Get All Products
        [HttpGet] // BaseUrl/api/Products
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams queryParamas )
        {
            var Products = await _serviceManager.ProductService.GetAllProductsAsync(queryParamas);
            return Ok(Products); //  بترجع الداتا علي هيئة جيسون داتا
        }

        #endregion

        #region Get Product By Id 
        [HttpGet("{Id:int}")] // BaseUrl/api/Products/1
        public async Task<ActionResult<ProductDto>> GetProduct(int Id)
        {
            var Product = await _serviceManager.ProductService.GetProductByIdAsync(Id);
            return Ok(Product);
        }

        #endregion

        #region Get All Types

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var Types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }
        #endregion

        #region Get All Brands

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }

        #endregion

    }
}
