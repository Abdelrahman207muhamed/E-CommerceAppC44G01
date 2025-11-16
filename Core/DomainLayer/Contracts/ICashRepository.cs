using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface ICashRepository
    {
        //Get 
        Task<string?> GetAsync(string CashKey);

        //Set

        Task SetAsync(string CashKey, string CashValue, TimeSpan TimeToLive);


    }
}
