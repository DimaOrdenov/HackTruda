using HackTruda.Extensions;
using HackTruda.ViewControls;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms.PancakeView;

namespace HackTruda.Views.Feed
{
    public partial class FeedPage : BasePage
    {
        public FeedPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            icAddStory.SetStrokeTintColor(AppColors.Primary);
        }

        private void SKCanvasViewPaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKCanvas canvas = e.Surface.Canvas;
            SKImageInfo bounds = e.Info;

            canvas.Clear();

            canvas.DrawRoundRect(
                new SKRoundRect(
                    new SKRect(0, 0, bounds.Width, bounds.Height),
                    15),
                new SKPaint
                {
                    Color = AppColors.White.ToSKColor(),
                });
        }
    }
}
