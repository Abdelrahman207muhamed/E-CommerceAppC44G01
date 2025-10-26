using Shared;
using Shared.Dtos;
using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        //Get All Products
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParamas);
        //Get Product By Id
        Task<ProductDto?> GetProductByIdAsync(int Id);
        //Get All Types
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
        //GetAllBrands
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
    }
}
