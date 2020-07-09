using Autofac;
using HackTruda.BusinessLogic;
using RestSharp;

namespace HackTruda.Containers
{
    public partial class IocInitializer
    {
        private static void InitBusinessLogicServices()
        {
            _builder.Register(c => new RestClient(Config.BaseApiUrl))
                .As<IRestClient>()
                .SingleInstance();

            _builder.RegisterType<UserContext>()
                    .AsSelf()
                    .SingleInstance();

            //_builder.RegisterType<UsersService>()
            //        .As<IUsersService>()
            //        .SingleInstance();
        }
    }
}
