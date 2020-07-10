using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using FFImageLoading.Forms;
using HackTruda.BusinessLogic.Interfaces;
using HackTruda.DataModels.Responses;
using HackTruda.Definitions;
using HackTruda.Definitions.Enums;
using HackTruda.Extensions;
using HackTruda.Services.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HackTruda.ViewModels.Profile
{
    public class ProfileViewModel : PageViewModel
    {
        private readonly IUsersLogic _usersLogic;
        private readonly IPostsLogic _postsLogic;

        private ObservableCollection<FeedResponse> _feeds;

        public ICommand ChooseAvatarCommand { get; }

        public ICommand CountryTapCommand { get; }

        public ICommand OpenSectionCommand { get; }

        public ICommand FabTapCommand { get; }

        public ProfileViewModel(
            INavigationService navigationService,
            IDialogService dialogService,
            IDebuggerService debuggerService,
            IUsersLogic usersLogic,
            IPostsLogic postsLogic)
            : base(navigationService, dialogService, debuggerService)
        {
            State = PageStateType.Default;

            _usersLogic = usersLogic;
            _postsLogic = postsLogic;

            ChooseAvatarCommand = BuildPageVmCommand(() => DialogService.DisplayAlert(null, "Выбираю фото", "Ок"));

            CountryTapCommand = BuildPageVmCommand(() => DialogService.DisplayAlert(null, "Ищу по стране", "Ок"));

            OpenSectionCommand = BuildPageVmCommand<string>((i) =>
            {
                string msg = "лайки";

                switch (i)
                {
                    case "1":
                        msg = "друзей";
                        break;
                    case "2":
                        msg = "подписки";
                        break;
                }

                return DialogService.DisplayAlert(null, $"Открываю {msg}", "Ок");
            });

            FabTapCommand = BuildPageVmCommand(() => DialogService.DisplayAlert(null, "Создаю пост", "Ок"));

            CachedImage icSettings = new CachedImage
            {
                Source = AppImages.IcSettings,
            };

            icSettings.SetTintColor(Color.Black);

            ToolbarItems = new ObservableCollection<ToolbarItem>
            {
                new ToolbarItem
                {
                    IconImageSource = icSettings.Source,
                    Command = BuildPageVmCommand(() => NavigationService.NavigateAsync(PageType.ProfileSettingsPage)),
                }
            };
        }

        public ObservableCollection<FeedResponse> Feeds
        {
            get => _feeds;
            set => SetProperty(ref _feeds, value);
        }

        public override async Task OnAppearing()
        {
            if (PageDidAppear)
            {
                return;
            }

            State = PageStateType.Loading;

            Feeds = new ObservableCollection<FeedResponse>(await LoadFeed());

            State = PageStateType.Default;

            await base.OnAppearing();
        }

        private async Task<IEnumerable<FeedResponse>> LoadFeed()
        {
            IEnumerable<FeedResponse> result = null;

            await ExceptionHandler.PerformCatchableTask(
                new ViewModelPerformableAction(
                    async () =>
                    {
                        result = await _postsLogic.GetFeed(2, 1, CancellationToken);
                    }));

            return result ?? new List<FeedResponse>();
        }
    }
}
