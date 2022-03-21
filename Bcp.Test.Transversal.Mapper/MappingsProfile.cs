using System;
using AutoMapper;
using Bcp.Test.Domain.Entity;
using Bcp.Test.Application.DTO;

namespace Bcp.Test.Transversal.Mapper
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<TipoCambioResponse, TipoCambioResponseDto>().ReverseMap();
            CreateMap<Users, UsersDto>().ReverseMap();
            CreateMap<TipoCambioFijo, TipoCambioFijoDto>().ReverseMap();
        }
    }
}
