using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    //Abstract => May Implement And Inheritance
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public BaseSpecifications(Expression<Func<TEntity, bool>> CriteriaExpression)
        { 
            Criteria = CriteriaExpression;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; } = [];

        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> OrderByAsc)
        => OrderBy = OrderByAsc;


        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        #region Pagnation
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPaginated { get ; set ; }
       //Total Count = 40
       //Page Size = 10
       //Page Index = 2
       //10,10,10,10             10        //2
        protected void ApplyPagination(int PageSize, int PageIndex)
        { 
            IsPaginated= true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize; //1*10 = 10      
        
        }
        #endregion
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> OrderByDesc)
         => OrderByDescending = OrderByDesc;

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpression.Add(includeExpression);
        }

    }
}
