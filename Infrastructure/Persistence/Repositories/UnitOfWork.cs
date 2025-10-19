using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _Repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            // 1-Get Type Name
            var TypeName = typeof(TEntity).Name; //=>Ex: Product


            //2-Dic<string,object>  string [Key] => [Name Of Type] __ Object [Value] => [Object From Generic Repository]
            //if (_Repositories.ContainsKey(TypeName))
            //{
            //    return (IGenericRepository<TEntity, TKey>)_Repositories[TypeName];
            //}
            if (_Repositories.TryGetValue(TypeName, out object? value))
            {
                return (IGenericRepository<TEntity, TKey>)value;
            }
            else
            {
                //Create Object
                var Repo = new GenericRepository<TEntity, TKey>(_dbContext);
                //Store Object In Dic
                // _Repositories.Add(TypeName, Repo);
                _Repositories["TypeName"] = Repo;
                //Return Object
                return Repo;

            }

        }

        public async Task<int> SaveChangesAsync()
        
        => await _dbContext.SaveChangesAsync();//SaveChanges  دي جوا
                                               //ال Dbcontext
                                               //اللي بتتعامل مع الداتا بيز هي بس نفس الاسم بتاعت ال فانكشن انما دي غير دي
        
    }
}
