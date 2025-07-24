using AutoMapper;
using Revv.Cars.Domain.models;
using Revv.Cars.Shared.Commands;
using System;

namespace Revv.Cars.Domain
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            // Domain <=> Shared
            CreateMap<Car, Revv.Cars.Shared.Car>().ReverseMap();

            // ✅ ADD THIS: Shared ➝ Domain (Fix for your error)
            CreateMap<Revv.Cars.Shared.Car, Car>();

            // Domain ➝ Response
            CreateMap<Car, CreateCarCommandResponse>()
                .ForMember(dest => dest.Car, opt => opt.MapFrom(src => src));

            // Request ➝ Shared
            CreateMap<CreateCarCommandRequest, Revv.Cars.Shared.Car>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Date, opt => opt.MapFrom<StringToFormattedDateResolver>());
        }
    }

    public class StringToFormattedDateResolver : IValueResolver<CreateCarCommandRequest, Revv.Cars.Shared.Car, string>
    {
        public string Resolve(CreateCarCommandRequest src, Revv.Cars.Shared.Car dest, string destMember, ResolutionContext context)
        {
            return DateTime.TryParse(src.Date, out var dt)
                ? dt.ToString("yyyy-MM-dd")
                : src.Date;
        }
    }
}
