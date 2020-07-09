using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Support.Design.Widget;
using HackTruda.Droid.CustomRenderers;
using HackTruda.ViewControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

//[assembly: ExportRenderer(typeof(BaseTabbedPage), typeof(TabbedPageRenderer))]
namespace HackTruda.Droid.CustomRenderers
{
    public class TabbedPageRenderer : Xamarin.Forms.Platform.Android.AppCompat.TabbedPageRenderer
    {
        public TabbedPageRenderer(Context context)
            : base(context)
        {
        }
    }
}
