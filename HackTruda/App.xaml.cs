using System;
using Xamarin.Forms;
using HackTruda.Views;
using HackTruda.Services.Interfaces;
using System.Threading.Tasks;
using HackTruda.Definitions.Enums;
using HackTruda.Containers;
using Autofac;
using Microsoft.AspNetCore.SignalR.Client;

namespace HackTruda
{
    public partial class App : Application
    {
        private INavigationService _navigationService;
        private IDebuggerService _debuggerService;
        private HubConnection _hubConnection;

        public App()
        {
            InitializeComponent();

            MainPage = new ContentPage();
        }

        protected override void OnStart()
        {
            InitLocalServices();

            PushMainPage();

            //SetupHub();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void InitLocalServices()
        {
            _navigationService = IocInitializer.Container.Resolve<INavigationService>();
            _debuggerService = IocInitializer.Container.Resolve<IDebuggerService>();
            _hubConnection = IocInitializer.Container.Resolve<HubConnection>();
        }

        private void PushMainPage()
        {
            _navigationService.SetRootPage(PageType.AuthPage);
        }

        //private void SetupHub()
        //{
        //    try
        //    {
        //        Task.Run(async () =>
        //        {
        //            await _hubConnection.StartAsync();
        //        });
        //    }
        //    catch (Exception e)
        //    {
        //        _debuggerService.Log(e);
        //    }
        //}
    }
}
