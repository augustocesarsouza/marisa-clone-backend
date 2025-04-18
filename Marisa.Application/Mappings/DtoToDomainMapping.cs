using AutoMapper;
using Marisa.Application.DTOs;
using Marisa.Domain.Entities;

namespace Marisa.Application.Mappings
{
    public class DtoToDomainMapping : Profile
    {
        public DtoToDomainMapping() 
        {
            CreateMap<UserDTO, User>();
            CreateMap<AddressDTO, Address>();
        }
    }
}
