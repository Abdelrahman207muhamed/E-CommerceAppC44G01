using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Product :BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; } = default!;
        public string PictureUrl { get; set; } = null!;

        #region Product Brand
        public int BrandId { get; set; }
        public ProductBrand productBrand { get; set; }
        #endregion

        #region Product Type
        public int TypeId { get; set; }
        public ProductType productType { get; set; }
        #endregion


    }
}
