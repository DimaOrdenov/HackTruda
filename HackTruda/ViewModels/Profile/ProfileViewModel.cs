using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FFImageLoading.Forms;
using HackTruda.BusinessLogic.Interfaces;
using HackTruda.DataModels.Responses;
using HackTruda.Definitions;
using HackTruda.Definitions.Enums;
using HackTruda.Extensions;
using HackTruda.Services.Interfaces;
using Xamarin.Forms;

namespace HackTruda.ViewModels.Profile
{
    /// <summary>
    /// VM для страницы Профиль
    /// </summary>
    public class ProfileViewModel : PageViewModel
    {
        private readonly IUsersLogic _usersLogic;
        private readonly IPostsLogic _postsLogic;

        private ObservableCollection<ProfileFeedItemViewModel> _feeds;
        private UserResponse _user;
        private ObservableCollection<ProfileFeedItemViewModel> _stories;

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

        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public ObservableCollection<ProfileFeedItemViewModel> Feeds
        {
            get => _feeds;
            set => SetProperty(ref _feeds, value);
        }

        public ObservableCollection<ProfileFeedItemViewModel> Stories
        {
            get => _stories;
            set => SetProperty(ref _stories, value);
        }


        public override async Task OnAppearing()
        {
            if (PageDidAppear)
            {
                return;
            }

            State = PageStateType.Loading;
            var feed = await LoadFeed();
           
            Stories = new ObservableCollection<ProfileFeedItemViewModel>(feed.Where(x => x.Feed.IsStory == true).ToList());
            Feeds = new ObservableCollection<ProfileFeedItemViewModel>(feed.Where(x => x.Feed.IsStory == false).ToList());

            State = PageStateType.Default;

            await base.OnAppearing();
        }

        private async Task<IEnumerable<ProfileFeedItemViewModel>> LoadFeed()
        {
            IEnumerable<ProfileFeedItemViewModel> result = null;

            await ExceptionHandler.PerformCatchableTask(
                new ViewModelPerformableAction(
                    async () =>
                    {
                        User = await _usersLogic.Get("2", CancellationToken);

                        result =
                            (await _postsLogic.GetUserPosts(2, CancellationToken))
                            .ToList()
                            .OrderByDescending(x => x.Date)
                            .Select(x => new ProfileFeedItemViewModel(x));
                    }));

            return result ?? new List<ProfileFeedItemViewModel>();
        }
    }
}
