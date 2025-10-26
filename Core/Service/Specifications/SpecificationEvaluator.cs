using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Service.Specifications
{
    public static class SpecificationEvaluator
    {
        //Create Query 
        //_dbContext.Products.where(P=>P.Id==id).Include (P=>P.ProductType);.Include(P=>P.ProductBrand)
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> InputQuery, ISpecifications<TEntity, TKey> specififcations) where TEntity : BaseEntity<TKey>
        {
            //Sum = Sum+i;
            var Query = InputQuery;
            if (specififcations.Criteria is not null)
            {
                Query = Query.Where(specififcations.Criteria);
            }

            if (specififcations.OrderBy is not null)
            { 
                Query = Query.OrderBy(specififcations.OrderBy);
            }
            if (specififcations.OrderByDescending is not null)
            {
                Query = Query.OrderByDescending(specififcations.OrderByDescending);
            }




            if(specififcations.IncludeExpression is not null && specififcations.IncludeExpression.Count>0)
            {
                Query = specififcations.IncludeExpression.Aggregate(Query, (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));
            }
            return Query;

        }

    }
}
