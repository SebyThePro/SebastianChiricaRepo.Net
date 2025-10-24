using AutoMapper;
using PMA.ProductManagement.Entities;
using PMA.ProductManagement.Dtos;

namespace PMA.ProductManagement.Resolvers
{
    public class BrandInitialsResolver : IValueResolver<Product, ProductProfileDto, string>
    {
        public string Resolve(Product source, ProductProfileDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.Brand))
            {
                return "?";
            }

            var words = source.Brand.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 1)
            {
                return words[0][0].ToString().ToUpper();
            }
            
            if (words.Length >= 2)
            {
                return $"{words[0][0].ToString().ToUpper()}{words[^1][0].ToString().ToUpper()}";
            }

            return "?";
        }
    }
}