

using AutoMapper;
using Domain.Entities;
using Telegram.Bot.Types;

namespace Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, AppUser>();
        }
    }
}
