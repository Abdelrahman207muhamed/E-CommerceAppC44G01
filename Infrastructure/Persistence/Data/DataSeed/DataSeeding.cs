using DomainLayer.Contracts;
using DomainLayer.Models;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.OrderModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Data.DataSeed
{
    public class DataSeeding(StoreDbContext _dbContext , UserManager<ApplicationUser> _userManager , RoleManager<IdentityRole> _roleManager  ) : IDataSeeding
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
            if (!_dbContext.products.Any())
            {
                var ProductsData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                if (Products is not null && Products.Any())
                {
                   await _dbContext.products.AddRangeAsync(Products);//Local
                }
            }

            #endregion

            #region  DeliveryMethod
            if (!_dbContext.DeliveryMethods.Any())
            {
                var DeliveryData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\delivery.json");
                var Methods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryData);
                if (Methods is not null && Methods.Any())
                {
                    await _dbContext.DeliveryMethods.AddRangeAsync(Methods);//Local
                }

            }

            #endregion

            await _dbContext.SaveChangesAsync();
            #endregion


        }

        public async Task IdentityDataSeed()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "Mohamed@gmail.com",
                        DisplayName = "Mohamed Ali",
                        PhoneNumber = "123456789",
                        UserName = "MohamedAli",
                        EmailConfirmed = true
                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "Salma@gmail.com",
                        DisplayName = "Salma Ali",
                        PhoneNumber = "123456789",
                        UserName = "SalmaAli",
                        EmailConfirmed = true
                    };
                    await _userManager.CreateAsync(User01, "P@ssw0rd");
                    await _userManager.CreateAsync(User02, "P@ssw0rd");
                    //-------------------------------------------
                    //Add TO Role
                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");

                }
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
