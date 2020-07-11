using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using HackTruda.Droid.CustomRenderers;
using HackTruda.ViewControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ShadowFrame), typeof(ShadowFrameRender))]
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
                //float shadowRadius =
                //    shadowFrame.CornerRadius > 0 ?
                //    shadowFrame.CornerRadius :
                //    ((shadowFrame.Content as Frame)?.CornerRadius ?? 0);

                //GradientDrawable shape = new GradientDrawable();
                //shape.SetShape(ShapeType.Rectangle);
                //shape.SetCornerRadius(shadowRadius);
                //shape.SetColor(Context.GetColorStateList(Resource.Color.colorShadow));

                //Control.SetBackground(shape);
            }
        }
    }
}
