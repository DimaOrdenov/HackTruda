using HackTruda.ViewControls;
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
    }
}