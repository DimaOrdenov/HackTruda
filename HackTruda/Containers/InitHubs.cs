using System;
using Autofac;
using Microsoft.AspNetCore.SignalR.Client;

namespace HackTruda.Containers
{
    public partial class IocInitializer
    {
        private static void InitHubs()
        {
            _builder
                .Register(c =>
                    new HubConnectionBuilder()
                        .WithUrl($"{Config.BaseApiUrl}chat")
                        .WithAutomaticReconnect()
                        .Build())
                .AsSelf()
                .SingleInstance();
        }
    }
}
