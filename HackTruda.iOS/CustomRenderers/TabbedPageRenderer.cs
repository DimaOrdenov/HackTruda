using HackTruda.iOS.CustomRenderers;
using HackTruda.ViewControls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BaseTabbedPage), typeof(TabbedPageRenderer))]
namespace HackTruda.iOS.CustomRenderers
{
    public class TabbedPageRenderer : TabbedRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            TabBar.StandardAppearance.ShadowColor = UIColor.Clear;
            TabBar.StandardAppearance.ShadowImage = new UIImage();
        }
    }
}
