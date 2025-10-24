using AutoMapper;
using PMA.ProductManagement.Entities;
using PMA.ProductManagement.Dtos;

namespace PMA.ProductManagement.Resolvers
{
    public class ProductAgeResolver : IValueResolver<Product, ProductProfileDto, string>
    {
        public string Resolve(Product source, ProductProfileDto destination, string destMember, ResolutionContext context)
        {
            var ageInDays = (DateTime.UtcNow.Date - source.ReleaseDate.Date).TotalDays;

            return ageInDays switch
            {
                var d when d < 0 => "Future Release",
                var d when d < 30 => "New Release",
                var d when d < 365 => $"{Math.Floor(d / 30)} months old",
                var d when d < 1825 => $"{Math.Floor(d / 365)} years old",
                var d when d >= 1825 => "Classic",
                _ => "Age Undetermined"
            };
        }
    }
}