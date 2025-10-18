using AutoMapper;
using Guardian.Api.Domain;
using Guardian.Api.DTOs;

namespace Guardian.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();

            CreateMap<BettingPlatform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, BettingPlatform>();

            CreateMap<PixTransfer, TransferReadDto>();
            CreateMap<TransferCreateDto, PixTransfer>();

            CreateMap<Alert, AlertReadDto>();
            CreateMap<AlertCreateDto, Alert>();
        }
    }
}
