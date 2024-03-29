﻿using Autofac;
using HackTruda.BusinessLogic;
using HackTruda.BusinessLogic.Interfaces;
using RestSharp;

namespace HackTruda.Containers
{
    public partial class IocInitializer
    {
        /// <summary>
        /// Регистрация BL.
        /// </summary>
        private static void InitBusinessLogicServices()
        {
            _builder.Register(c => new RestClient(Config.BaseApiUrl + "api/"))
                .As<IRestClient>()
                .SingleInstance();

            _builder.RegisterType<UserContext>()
                    .AsSelf()
                    .SingleInstance();

            _builder.RegisterType<UsersLogic>()
                    .As<IUsersLogic>()
                    .SingleInstance();

            _builder.RegisterType<PostsLogic>()
                    .As<IPostsLogic>()
                    .SingleInstance();

            _builder.RegisterType<AuthLogic>()
                  .As<IAuthLogic>()
                  .SingleInstance();

            _builder.RegisterType<GamesLogic>()
                  .As<IGamesLogic>()
                  .SingleInstance();
        }
    }
}
