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
            // Feed
            _pageBuilder.Configure(
                new PageStack(
                    PageType.FeedPage,
                    typeof(Views.Feed.FeedPage),
                    typeof(ViewModels.Feed.FeedViewModel)));

            // Messages
            _pageBuilder.Configure(
                new PageStack(
                    PageType.MessagesPage,
                    typeof(Views.Messages.MessagesPage),
                    typeof(ViewModels.Messages.MessagesViewModel)));

            // Search
            _pageBuilder.Configure(
                new PageStack(
                    PageType.SearchPage,
                    typeof(Views.Search.SearchPage),
                    typeof(ViewModels.Search.SearchViewModel)));

            // Notifications
            _pageBuilder.Configure(
                new PageStack(
                    PageType.NotificationsPage,
                    typeof(Views.Notifications.NotificationsPage),
                    typeof(ViewModels.Notifications.NotificationsViewModel)));

            // Profile
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

            // Tabbed pages
            _pageBuilder.ConfigureTabbed(
                new TabbedPageStack(
                    TabbedPageType.MainPage,
                    typeof(Views.MainPage),
                    typeof(ViewModels.MainViewModel),
                    new List<PageType>
                    {
                        PageType.FeedPage,
                        PageType.MessagesPage,
                        PageType.SearchPage,
                        PageType.NotificationsPage,
                        PageType.ProfilePage,
                    },
                    new Dictionary<PageType, string>
                    {
                        { PageType.FeedPage, null },
                        { PageType.MessagesPage, null },
                        { PageType.SearchPage, null },
                        { PageType.NotificationsPage, null },
                        { PageType.ProfilePage, null },
                    })
                {
                    ChildrenPageIcons = new Dictionary<PageType, FileImageSource>
                    {
                        { PageType.FeedPage, AppImages.IcHome as FileImageSource },
                        { PageType.MessagesPage, AppImages.IcMail as FileImageSource },
                        { PageType.SearchPage, AppImages.IcSearch as FileImageSource },
                        { PageType.NotificationsPage, AppImages.IcBell as FileImageSource },
                        { PageType.ProfilePage, AppImages.IcUser as FileImageSource },
                    },
                });
        }
    }
}
