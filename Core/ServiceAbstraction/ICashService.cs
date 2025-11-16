using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface ICashService
    {
        Task<string?> GetAsync(string CashKey);
        Task SetAsync(string CashKey, object CashValue, TimeSpan TimeToLive);

    }
}
