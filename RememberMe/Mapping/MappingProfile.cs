using AutoMapper;
using RememberMe.Controllers.Resource;
using RememberMe.Core.Models;

namespace RememberMe.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Friend, FriendResource>(); 
            CreateMap<Friend, SaveFriendResource>(); 
            CreateMap<ContactDetails,ContactDetailsResource>();
            //API Resource to Domain
            CreateMap<SaveFriendResource,Friend>().ForMember(f => f.Id, opt => opt.Ignore()); 
            CreateMap<ContactDetailsResource,ContactDetails>(); 
        }
    }
}