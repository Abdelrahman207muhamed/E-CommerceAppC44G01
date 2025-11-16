using DomainLayer.Models;
using Shared;
using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class ProductWithBrandAndTypeSpecifications :BaseSpecifications<Product,int>
    {
        //Get All Product With Brands And Types   
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams)
            : base( P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId)
            &&(!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId)
            && (string.IsNullOrWhiteSpace(queryParams.search) || P.Name.Contains(queryParams.search.ToLower())))
        {
            AddInclude(P => P.productBrand);
            AddInclude(P => P.productType);
            ApplyPagination(queryParams.PageSize, queryParams.pageNumber);
           
            #region Sorting
            switch (queryParams.sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                     break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(P => P.Name);
                     break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                     break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(P => P.Price);
                     break;
                default:
                    break;
            }


            #endregion
        }
        //  Get Product By Id 

        public ProductWithBrandAndTypeSpecifications(int id) : base(P=>P.Id==id) 
        {
            AddInclude(P => P.productBrand);
            AddInclude(P => P.productType);
        }

    }
}
