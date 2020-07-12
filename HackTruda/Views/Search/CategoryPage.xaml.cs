using HackTruda.Extensions;
using HackTruda.ViewControls;

namespace HackTruda.Views.Search
{
    public partial class CategoryPage : BasePage
    {
        public CategoryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            icArrowBackIcon.SetStrokeTintColor(AppColors.Dark);
            icSearch.SetStrokeTintColor(AppColors.Dark);
        }
    }
}
