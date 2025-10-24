using AutoMapper;
using PMA.ProductManagement.Dtos;
using PMA.ProductManagement.Entities;
using PMA.ProductManagement.Resolvers;

namespace PMA.ProductManagement.Mapping
{
    public class AdvancedProductMappingProfile : Profile
    {
        public AdvancedProductMappingProfile()
        {
            CreateMap<CreateProductProfileRequest, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsAvailable, opt => opt.Ignore())
                .ForMember("UpdatedAt", opt => opt.Ignore()); 
            
            CreateMap<Product, ProductProfileDto>()
                .ForMember(dest => dest.CategoryDisplayName, opt => opt.MapFrom<CategoryDisplayResolver>())
                .ForMember(dest => dest.FormattedPrice, opt => opt.MapFrom<PriceFormatterResolver>())
                .ForMember(dest => dest.ProductAge, opt => opt.MapFrom<ProductAgeResolver>())
                .ForMember(dest => dest.BrandInitials, opt => opt.MapFrom<BrandInitialsResolver>())
                .ForMember(dest => dest.AvailabilityStatus, opt => opt.MapFrom<AvailabilityStatusResolver>())
                
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable));
        }
    }
}