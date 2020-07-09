using System.Linq;
using HackTruda.ViewControls;
using HackTruda.Views.Feed;
using HackTruda.Views.Messages;
using HackTruda.Views.Notifications;
using HackTruda.Views.Profile;
using HackTruda.Views.Search;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace HackTruda.Views
{
    public partial class MainPage : BaseTabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            NavigationPage page = CurrentPage as NavigationPage;
            Page currentNavPage = page.Navigation?.NavigationStack?.FirstOrDefault();

            SetIcon(page, true);

            foreach (NavigationPage navPage in Children.Select(x => x as NavigationPage)
                                                       .Where(p => p.Navigation?.NavigationStack?.FirstOrDefault().GetType() != currentNavPage.GetType())
                                                       .ToList())
            {
                SetIcon(navPage, false);
            }
        }

        private void SetIcon(NavigationPage page, bool isActive)
        {
            Page currentNavPage = page.Navigation?.NavigationStack?.FirstOrDefault();

            if (currentNavPage is FeedPage)
            {
                page.IconImageSource = isActive ? AppImages.IcHomeActive : AppImages.IcHome;
            }
            else if (currentNavPage is MessagesPage)
            {
                page.IconImageSource = isActive ? AppImages.IcMailActive : AppImages.IcMail;
            }
            else if (currentNavPage is SearchPage)
            {
                page.IconImageSource = isActive ? AppImages.IcSearchActive : AppImages.IcSearch;
            }
            else if (currentNavPage is NotificationsPage)
            {
                page.IconImageSource = isActive ? AppImages.IcBellActive : AppImages.IcBell;
            }
            else if (currentNavPage is ProfilePage)
            {
                page.IconImageSource = isActive ? AppImages.IcUserActive : AppImages.IcUser;
            }
        }
    }
}