using AutoMapper;
using Marisa.Application.DTOs;
using Marisa.Domain.Entities;

namespace Marisa.Application.Mappings
{
    public class DomainToDtoMapping : Profile
    {
        public DomainToDtoMapping() 
        {
            CreateMap<User, UserDTO>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !(srcMember is string str && string.IsNullOrEmpty(str))
                ));

            CreateMap<Address, AddressDTO>()
                .ForMember(dest => dest.UserDTO, opt => opt.MapFrom(src => src.User))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !(srcMember is string str && string.IsNullOrEmpty(str))
                ));
        }
    }
}
