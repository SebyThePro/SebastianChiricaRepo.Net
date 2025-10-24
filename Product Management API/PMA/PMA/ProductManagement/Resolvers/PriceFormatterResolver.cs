using AutoMapper;
using System.Globalization;
using PMA.ProductManagement.Entities;
using PMA.ProductManagement.Dtos;

namespace PMA.ProductManagement.Resolvers
{
    public class PriceFormatterResolver : IValueResolver<Product, ProductProfileDto, string>
    {
        public string Resolve(Product source, ProductProfileDto destination, string destMember, ResolutionContext context)
        {
            return source.Price.ToString("C2", CultureInfo.GetCultureInfo("en-US")); 
        }
    }
}