using System;
using HackTruda.iOS.CustomRenderers;
using HackTruda.ViewControls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedNavigationPage), typeof(ExtendedNavigationPageRenderer))]
namespace HackTruda.iOS.CustomRenderers
{
    public class ExtendedNavigationPageRenderer : NavigationRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NavigationBar.StandardAppearance.ShadowColor = UIColor.Clear;
            NavigationBar.StandardAppearance.ShadowImage = new UIImage();
        }
    }
}
