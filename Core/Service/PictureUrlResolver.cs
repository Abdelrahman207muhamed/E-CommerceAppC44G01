using AutoMapper;
using AutoMapper.Execution;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PictureUrlResolver(IConfiguration Configuration) : IValueResolver<Product, ProductDto, string>
    {
        //https://localhost:7144/{src.PictureUrl}
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }
            else 
            {
                var Url = $"{Configuration.GetSection("URLS")["BaseUrl"]}{source.PictureUrl}";

                return Url;
            
            }

        }
    }
}
