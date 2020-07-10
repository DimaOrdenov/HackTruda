using System;
using Android.Content;
using HackTruda.Droid.CustomRenderers;
using HackTruda.ViewControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BaseEntry), typeof(BaseEntryRenderer))]
namespace HackTruda.Droid.CustomRenderers
{
    public class BaseEntryRenderer : EntryRenderer
    {
        public BaseEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null || Control == null)
            {
                return;
            }

            Control.Background = null;
        }
    }
}
