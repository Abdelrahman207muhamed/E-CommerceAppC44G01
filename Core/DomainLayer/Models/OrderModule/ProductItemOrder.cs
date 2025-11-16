using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModule
{
    public class ProductItemOrder
    {
        public ProductItemOrder()
        {

        }
        public ProductItemOrder(int productId, string productName, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;

    }
}
