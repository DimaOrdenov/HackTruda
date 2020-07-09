using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using HackTruda.ViewControls;
using HackTruda.iOS.CustomRenderers;

[assembly: ExportRenderer(typeof(BaseEntry), typeof(BaseEntryRenderer))]
namespace HackTruda.iOS.CustomRenderers
{
    public class BaseEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null || Control == null)
            {
                return;
            }

            Control.BorderStyle = UITextBorderStyle.None;

            if (!(Element is BaseEntry view))
            {
                return;
            }

            UIToolbar toolbar = new UIToolbar();
            toolbar.SizeToFit();

            toolbar.Items = new[]
            {
                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate { Control.ResignFirstResponder(); })
            };

            Control.InputAccessoryView = toolbar;
        }
    }
}
