using AutoMapper;
using PMA.ProductManagement.Enums;
using PMA.ProductManagement.Entities;
using PMA.ProductManagement.Dtos;

namespace PMA.ProductManagement.Resolvers
{
    public class CategoryDisplayResolver : IValueResolver<Product, ProductProfileDto, string>
    {
        public string Resolve(Product source, ProductProfileDto destination, string destMember, ResolutionContext context)
        {
            return source.Category switch
            {
                ProductCategory.Electronics => "Electronics & Technology",
                ProductCategory.Clothing => "Clothing & Fashion",
                ProductCategory.Books => "Books & Media",
                ProductCategory.Home => "Home & Garden",
                _ => "Uncategorized",
            };
        }
    }
}