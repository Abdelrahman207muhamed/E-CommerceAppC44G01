
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

namespace E_CommerceAppC44G01
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            
            #region Configure Services
            builder.Services.AddInfraStructureService(builder.Configuration);
            builder.Services.AddCoreServices();
            builder.Services.AddPersentationServices();
            builder.Services.AddScoped<PictureUrlResolver>();
         
            #endregion

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Build
            var app = builder.Build();

            #endregion


            #region MiddleWares
             app.UseCustomMiddleWareExceptions();
            await app.SeedDbAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            #endregion

            
        }
    }
}
