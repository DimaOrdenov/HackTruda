using AutoMapper;
using HackTruda.API.Models;
using HackTruda.DataModels.Requests;
using HackTruda.DataModels.Responses;

namespace HackTruda.API.AutoMapper
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostRequest, Post>();
            CreateMap<Post, PostResponse>();
        }
    }
}