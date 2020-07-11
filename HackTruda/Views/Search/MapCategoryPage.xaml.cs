using HackTruda.ViewControls;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HackTruda.Views.Search
{
    public partial class MapCategoryPage : BasePage
    {
        public MapCategoryPage()
        {
            InitializeComponent();

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                AbsoluteLayout.SetLayoutBounds(filtersScroll, Rectangle.FromLTRB(0, 0, 0.85, -1));
            }
        }
    }
}
