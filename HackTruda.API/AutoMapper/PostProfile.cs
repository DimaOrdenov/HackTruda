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
            CreateMap<Post, FeedResponse>()
                .ForMember(feedResponse => feedResponse.LastName, expr => expr.MapFrom(post => post.User.LastName))
                .ForMember(feedResponse => feedResponse.FirstName, expr => expr.MapFrom(post => post.User.FirstName))
                .ForMember(feedResponse => feedResponse.UserImage, expr => expr.MapFrom(post => post.User.Image));
        }
    }
}