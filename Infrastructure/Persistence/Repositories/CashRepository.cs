using DomainLayer.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CashRepository(IConnectionMultiplexer connection) : ICashRepository
    {
        readonly IDatabase _database = connection.GetDatabase();

        public async Task<string?> GetAsync(string CashKey)
        {
            var CashValue = await _database.StringGetAsync(CashKey);
            return CashValue.IsNullOrEmpty ? null : CashValue.ToString();
        }

        public async Task SetAsync(string CashKey, string CashValue, TimeSpan TimeToLive)
        {
            await _database.StringSetAsync(CashKey, CashValue, TimeToLive);
                
        }
    }
}
