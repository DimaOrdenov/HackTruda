using System;
using Android.Content;
using HackTruda.Droid.CustomRenderers;
using HackTruda.ViewControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

//[assembly: ExportRenderer(typeof(ShadowFrame), typeof(ShadowFrameRender))]
namespace HackTruda.Droid.CustomRenderers
{
    public class ShadowFrameRender : Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer
    {
        public ShadowFrameRender(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (!(e.NewElement is ShadowFrame shadowFrame))
            {
                return;
            }

            if (shadowFrame.HasShadow)
            {
                CardElevation = shadowFrame.ShadowBlur;

                try
                {

                    SetOutlineAmbientShadowColor(shadowFrame.ShadowColor.ToAndroid());
                    SetOutlineSpotShadowColor(shadowFrame.ShadowColor.ToAndroid());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Frame Error: {ex}");
                }
            }
        }
    }
}
