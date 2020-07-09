using HackTruda.iOS.CustomRenderers;
using HackTruda.ViewControls;
using HackTruda.ViewModels;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BasePage), typeof(BasePageRenderer))]
namespace HackTruda.iOS.CustomRenderers
{
    public class BasePageRenderer : PageRenderer
    {
        public BasePageRenderer()
        {
        }

        public override void WillMoveToParentViewController(UIViewController parent)
        {
            if (parent != null)
            {
                parent.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
            }

            base.WillMoveToParentViewController(parent);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (Element is BasePage basePage &&
                basePage.BindingContext is PageViewModel pageViewModel)
            {
                SetSwipeBackGesture(pageViewModel.HasNavigationBarBackButton);
            }
        }

        private void SetSwipeBackGesture(bool enabled)
        {
            ViewController.NavigationController.InteractivePopGestureRecognizer.Enabled = enabled;
            ViewController.NavigationController.InteractivePopGestureRecognizer.Delegate = enabled ? new UIGestureRecognizerDelegate() : null;
        }
    }
}
