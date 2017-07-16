using AutoMapper;
using RememberMe.Controllers.Resource;
using RememberMe.Models;

namespace RememberMe.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Friend, FriendResource>(); 
            CreateMap<ContactDetails,ContactDetailsResource>();
            //API Resource to Domain
            CreateMap<FriendResource,Friend>(); 
        }
    }
}