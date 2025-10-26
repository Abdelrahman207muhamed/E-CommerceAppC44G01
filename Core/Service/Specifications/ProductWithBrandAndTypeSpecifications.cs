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
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParamas)
            : base(P => (!queryParamas.BrandId.HasValue || P.BrandId == queryParamas.BrandId) &&
            (!queryParamas.TypeId.HasValue || P.TypeId == queryParamas.TypeId)
            && (string.IsNullOrWhiteSpace(queryParamas.SreachValue) || P.Name.Contains(queryParamas.SreachValue.ToLower())))
        {
            AddInclude(P => P.productBrand);
            AddInclude(P => P.productType);
            #region Sorting
            switch (queryParamas.sortingOptions)
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
