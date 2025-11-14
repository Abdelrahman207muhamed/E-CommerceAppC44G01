 
using DomainLayer.Contracts;
using E_CommerceAppC44G01.CustomMiddelWare;
using E_CommerceAppC44G01.Extentions;
using E_CommerceAppC44G01.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Persistence.Data;
using Persistence.Data.DataSeed;
using Persistence.Repositories;
using Service;
using ServiceAbstraction;
using Shared.ErroModels;
using StackExchange.Redis;

namespace E_CommerceAppC44G01
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            //WebApi Services
            builder.Services.AddWebApiServices();

            builder.Services.AddInfraStructureService(builder.Configuration);

            builder.Services.ADDJWTService(builder.Configuration);

            builder.Services.AddCoreServices();
            
            builder.Services.AddPersentationServices();


            builder.Services.AddScoped<PictureUrlResolver>();
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection"));
            });

            var app = builder.Build();
            await app.SeedDbAsync();

          
            app.UseCustomMiddleWareExceptions();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            

        }
    }
}
