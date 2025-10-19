using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Data.DataSeed
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {

            var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            if (PendingMigrations.Any()) // لو في migration  واحدة مش معمولة Applying  علي الداتابيز بيروح ينفذها علي طول
            { 
                _dbContext.Database.Migrate();
            }

            #region Seeding Data

            #region Product Brand
            if (!_dbContext.ProductBrands.Any())
            {
                var BrandsData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                var Brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(BrandsData);
                if (Brands is not null && Brands.Any())
                {
                   await _dbContext.ProductBrands.AddRangeAsync(Brands);//Local
                }
            }

            #endregion

            #region Product Type
            if (!_dbContext.ProductTypes.Any())
            {
                var TypesData =   File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                var Types = await JsonSerializer.DeserializeAsync<List<ProductType>>(TypesData);
                if (Types is not null && Types.Any())
                {
                   await _dbContext.ProductTypes.AddRangeAsync(Types);//Local
                }
            }

            #endregion

            #region Product 
            if (!_dbContext.ProductTypes.Any())
            {
                var ProductsData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                if (Products is not null && Products.Any())
                {
                   await _dbContext.products.AddRangeAsync(Products);//Local
                }
            }

            #endregion

           await _dbContext.SaveChangesAsync();
            #endregion


        }
    }
}
