using HackTruda.Extensions;
using HackTruda.ViewControls;

namespace HackTruda.Views.Search
{
    public partial class SearchPage : BasePage
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            icSearch.SetStrokeTintColor(AppColors.Dark);
        }
    }
}
