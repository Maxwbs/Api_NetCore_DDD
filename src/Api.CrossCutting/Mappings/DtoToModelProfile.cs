using Api.Domain.Dtos.User;
using Api.Domain.Models.UserModels;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<UserModel, UserDto>()
                    .ReverseMap();
            
            CreateMap<UserModel, UserDtoCreate>()
                    .ReverseMap();

            CreateMap<UserModel, UserDtoUpdate>()
                    .ReverseMap();
        }
    }
}
