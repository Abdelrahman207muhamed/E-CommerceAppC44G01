using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PaginatedResult<TEntity>
    {
        //كونستركتور اللي هياخد الحاجة من برااللي هو الكونترولر
        public PaginatedResult(int _PageSize, int _PageIndex, int _TotalCount, IEnumerable<TEntity> _Data)
        {
            PageIndex = _PageIndex;
            PageSize = _PageSize;
            count = _TotalCount;
            Data = _Data;
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int count { get; set; }
        public IEnumerable<TEntity> Data { get; set; }
    }
}
