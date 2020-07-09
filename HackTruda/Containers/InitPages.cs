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

            _pageBuilder.Configure(
                new PageStack(
                    PageType.ProfileSettingsPage,
                    typeof(Views.Profile.ProfileSettingsPage),
                    typeof(ViewModels.Profile.ProfileSettingsViewModel)));

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
                        { PageType.FeedPage, null },
                        { PageType.LocationsPage, null },
                        { PageType.MessagesPage, null },
                        { PageType.ProfilePage, null },
                    })
                {
                    ChildrenPageIcons = new Dictionary<PageType, FileImageSource>
                    {
                        { PageType.FeedPage, AppImages.IcHome as FileImageSource },
                        { PageType.LocationsPage, AppImages.IcMapPin as FileImageSource },
                        { PageType.MessagesPage, AppImages.IcMail as FileImageSource },
                        { PageType.ProfilePage, AppImages.IcUser as FileImageSource },
                    },
                });
        }
    }
}
