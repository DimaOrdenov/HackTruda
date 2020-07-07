using System;
using Xamarin.Forms;
using HackTruda.Views;
using HackTruda.Services.Interfaces;
using System.Threading.Tasks;
using HackTruda.Definitions.Enums;
using HackTruda.Containers;
using Autofac;

namespace HackTruda
{
    public partial class App : Application
    {
        private INavigationService _navigationService;

        public App()
        {
            InitializeComponent();

            MainPage = new ContentPage();
        }

        protected override void OnStart()
        {
            _navigationService = IocInitializer.Container.Resolve<INavigationService>();

            PushMainPage();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void PushMainPage()
        {
            _navigationService.SetRootPage(TabbedPageType.MainPage);
        }
    }
}
