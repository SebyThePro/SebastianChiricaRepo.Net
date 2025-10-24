using AutoMapper;
using PMA.ProductManagement.Entities;
using PMA.ProductManagement.Dtos;

namespace PMA.ProductManagement.Resolvers
{
    public class AvailabilityStatusResolver : IValueResolver<Product, ProductProfileDto, string>
    {
        public string Resolve(Product source, ProductProfileDto destination, string destMember, ResolutionContext context)
        {
            if (!source.IsAvailable)
            {
                return "Out of Stock"; 
            }
            
            return source.StockQuantity switch
            {
                1 => "Last Item",
                var stock when stock <= 5 => "Limited Stock",
                var stock when stock > 5 => "In Stock",
                _ => "In Stock"
            };
        }
    }
}