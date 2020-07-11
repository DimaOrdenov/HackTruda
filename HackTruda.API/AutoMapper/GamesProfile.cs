using AutoMapper;
using HackTruda.API.Models;
using HackTruda.DataModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackTruda.API.AutoMapper
{
    public class GamesProfile : Profile
    {
        public GamesProfile()
        {
            CreateMap<Games, GamesResponse>();
        }
    }
}
