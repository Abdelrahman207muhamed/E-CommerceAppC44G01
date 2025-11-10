using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParams
    {
        private const int DefaultPageSize = 5; //في حالة ان الشخص مدخليش اي سايز اصلا هعتبر ان احنا ف الصفحة الواحدة بنعرض 5 منتجات
        private const int MaxPageSize = 10;// لو الشخص تجاوز الماكس مش هقبلها 
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortingOptions  sortingOptions { get; set; }
        public string? SreachValue { get; set; }
        public int PageIndex { get; set; } = 1;

        private int pagesize = DefaultPageSize;
        public int PageSize 
        {
            get { return pagesize; }
            set { pagesize = value > MaxPageSize?MaxPageSize:value; } //لو الفاليو اللي انا باخدها من برا كانت اكبر من الماكس بيدج سايز هناخد الماكس لو لا هناخد الفاليو 
        }


    }
}
