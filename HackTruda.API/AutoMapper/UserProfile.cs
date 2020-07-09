using AutoMapper;
using HackTruda.API.Models;
using HackTruda.DataModels.Requests;
using HackTruda.DataModels.Responses;

namespace HackTruda.API.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRequest, User>();
            CreateMap<User, UserResponse>();
        }
    }
}