using HackTruda.ViewControls;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using SkiaSharp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using System;

namespace HackTruda.Views.Feed
{
    public partial class FeedPage : BasePage
    {
        public FeedPage()
        {
            InitializeComponent();
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
