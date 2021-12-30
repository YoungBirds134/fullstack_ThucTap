

using Application.DTOs;
using AutoMapper;
using Infrastructure.Identity.Contexts;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Models;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<MenuDTO,Menu>().ReverseMap();
            CreateMap<LanguagesDTO,Language>().ReverseMap();
            CreateMap<Product, ProductPagingDTO>().ReverseMap();

            


            //CreateMap<CreateProductCommand, Product>();
            //CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
        }
    }
}
