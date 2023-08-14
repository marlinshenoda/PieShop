

using AutoMapper;
using PieShop.Core.Models;
using PieShop.ViewModel;

namespace PieShop.Web.AutoMapper
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            CreateMap<ApplicationUser, AddUserViewModel>().ReverseMap();
         
               
        }
    }
}
