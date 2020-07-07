using HackTruda.Definitions.Enums;
using HackTruda.Definitions.PageStacks;
using System.Collections.Generic;
using Xamarin.Forms;

namespace HackTruda.Containers
{
    public partial class IocInitializer
    {
        private static void InitPages()
        {
            _pageBuilder.Configure(
                new PageStack(
                    PageType.FeedPage,
                    typeof(Views.Feed.FeedPage),
                    typeof(ViewModels.Feed.FeedViewModel)));

            _pageBuilder.Configure(
                new PageStack(
                    PageType.LocationsPage,
                    typeof(Views.Locations.LocationsPage),
                    typeof(ViewModels.Locations.LocationsViewModel)));

            _pageBuilder.Configure(
                new PageStack(
                    PageType.MessagesPage,
                    typeof(Views.Messages.MessagesPage),
                    typeof(ViewModels.Messages.MessagesViewModel)));

            _pageBuilder.Configure(
                new PageStack(
                    PageType.ProfilePage,
                    typeof(Views.Profile.ProfilePage),
                    typeof(ViewModels.Profile.ProfileViewModel)));

            _pageBuilder.ConfigureTabbed(
                new TabbedPageStack(
                    TabbedPageType.MainPage,
                    typeof(Views.MainPage),
                    typeof(ViewModels.MainViewModel),
                    new List<PageType>
                    {
                        PageType.FeedPage,
                        PageType.LocationsPage,
                        PageType.MessagesPage,
                        PageType.ProfilePage,
                    },
                    new Dictionary<PageType, string>
                    {
                        { PageType.FeedPage, "Лента" },
                        { PageType.LocationsPage, "Локации" },
                        { PageType.MessagesPage, "Сообщения" },
                        { PageType.ProfilePage, "Профиль" },
                    })
                {
                    ChildrenPageIcons = new Dictionary<PageType, FileImageSource>
                    {
                        { PageType.FeedPage, AppImages.IcIcon as FileImageSource },
                        { PageType.LocationsPage, AppImages.IcIcon as FileImageSource },
                        { PageType.MessagesPage, AppImages.IcIcon as FileImageSource },
                        { PageType.ProfilePage, AppImages.IcIcon as FileImageSource },
                    },
                });
        }
    }
}
