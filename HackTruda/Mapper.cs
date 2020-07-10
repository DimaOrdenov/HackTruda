using AutoMapper;
using HackTruda.DataModels.Responses;
using HackTruda.Definitions.Models;

namespace HackTruda
{
    public class Mapper
    {
        public IMapper Build()
        {
            MapperConfiguration conf = GetMapperConfiguration();

            return conf.CreateMapper();
        }

        private MapperConfiguration GetMapperConfiguration() =>
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserResponse, UserModel>();
            });
    }
}
