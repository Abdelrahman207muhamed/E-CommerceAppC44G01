 
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
            #region .
            //var builder = WebApplication.CreateBuilder(args);

            //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            ////WebApi Services
            //builder.Services.AddWebApiServices();

            //builder.Services.AddInfraStructureService(builder.Configuration);

            //builder.Services.ADDJWTService(builder.Configuration);

            //builder.Services.AddCoreServices();

            //builder.Services.AddPersentationServices();


            //builder.Services.AddScoped<PictureUrlResolver>();

            //#region Angular
            //builder.Services.AddCors(options=>
            //{
            //    options.AddPolicy("AllowAll", builder =>
            //    {
            //        builder.AllowAnyHeader();
            //        builder.AllowAnyMethod();
            //        builder.AllowAnyOrigin();

            //    });

            //});

            //#endregion


            //var app = builder.Build();
            //await app.SeedDatabaseAsync();


            //app.UseCustomMiddleWareExceptions();

            //if (app.Environment.IsDevelopment())
            //{


            //    app.UseSwaggerMiddlewares();
            //}

            //app.UseHttpsRedirection();

            //app.UseStaticFiles();

            //app.UseRouting();

            //app.UseAuthentication();
            //app.UseCors("AllowAll");

            //app.UseAuthorization();

            //app.MapControllers();

            //app.Run(); 
            #endregion

            #region DI Container.
            var builder = WebApplication.CreateBuilder(args);
            // Web API services.
            builder.Services.AddWebApiServices(builder.Configuration);

            // Infrastructure Services.
            builder.Services.AddInfrasturctureServices(builder.Configuration);

            // Core Services.
            builder.Services.AddCoreServices(builder.Configuration);
            #endregion

            #region Piplines.
            var app = builder.Build();
            // Data Seeding Service (put before the first request)
            await app.SeedDatabaseAsync();
            // UseExceptionHandlingMiddleWare:
            app.UseExceptionHandlingMiddleWare();
            // Use swagger MiddleWares so i cant customize without the program be large.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
            #endregion

        }
    }
}
