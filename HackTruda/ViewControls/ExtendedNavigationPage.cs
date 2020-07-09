using HackTruda.ViewModels;
using Xamarin.Forms;

namespace HackTruda.ViewControls
{
    public class ExtendedNavigationPage : Xamarin.Forms.NavigationPage
    {
        public ExtendedNavigationPage()
        {
            Init();
        }

        public ExtendedNavigationPage(Xamarin.Forms.Page rootPage)
            : base(rootPage)
        {
            Init();
        }

        private void Init()
        {
            Popped += async (object sender, NavigationEventArgs e) =>
            {
                if (e.Page == null)
                {
                    return;
                }

                var viewModel = e.Page.BindingContext as PageViewModel;

                if (viewModel != null)
                {
                    await viewModel.OnPopped();
                }

                e.Page.BindingContext = null;

                if (e.Page.ToolbarItems != null)
                {
                    e.Page.ToolbarItems.Clear();
                }
            };

            Pushed += async (object sender, NavigationEventArgs e) =>
            {
                if (e.Page == null)
                {
                    return;
                }

                var viewModel = e.Page.BindingContext as PageViewModel;

                if (viewModel != null)
                {
                    await viewModel.OnPushed();
                }
            };
        }
    }
}
