using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity :BaseEntity<TKey>
    {
        //GetAll
        Task<IEnumerable<TEntity>> GetAllAsync();

        //GetById
        Task<TEntity?> GetByIdAsync(TKey id);

        //Add
        Task AddAsync(TEntity entity);

        //Update
        void Update(TEntity entity);
        
        //Delete
        void Remove(TEntity entity);

        #region With Specifications
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications);
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications);
        Task<int> CountAsync(ISpecifications<TEntity, TKey> Specifications);
        #endregion
    }
}
