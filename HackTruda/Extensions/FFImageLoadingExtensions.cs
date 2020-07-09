using System;
using System.Collections.Generic;
using FFImageLoading.Forms;
using FFImageLoading.Svg.Forms;
using FFImageLoading.Transformations;
using Xamarin.Forms;

namespace HackTruda.Extensions
{
    public static class FFImageLoadingExtension
    {
        public static void SetTintColor(this SvgCachedImage svgCachedImage, Color color) =>
            svgCachedImage.ReplaceStringMap = new Dictionary<string, string>
            {
                {
                    "fill=\"#FFFFFF\"", $"fill=\"{color.GetHex()}\""
                },
            };

        public static void SetTintColor(this SvgImageSource svgImageSource, Color color) =>
            svgImageSource.ReplaceStringMap = new Dictionary<string, string>
            {
                {
                    "fill=\"#FFFFFF\"", $"fill=\"{color.GetHex()}\""
                },
            };

        public static void SetStrokeTintColor(this SvgCachedImage svgCachedImage, Color color) =>
            svgCachedImage.ReplaceStringMap = new Dictionary<string, string>
            {
                {
                    "stroke=\"#FFFFFF\"", $"stroke=\"{color.GetHex()}\""
                },
            };

        public static void SetStrokeTintColor(this SvgImageSource svgImageSource, Color color) =>
            svgImageSource.ReplaceStringMap = new Dictionary<string, string>
            {
                {
                    "stroke=\"#FFFFFF\"", $"stroke=\"{color.GetHex()}\""
                },
            };

        public static void SetTintColor(this CachedImage cachedImage, Color color) =>
            cachedImage.Transformations = new List<FFImageLoading.Work.ITransformation>
            {
                new TintTransformation
                {
                    EnableSolidColor = true,
                    HexColor = color.GetHex(),
                }
            };
    }
}
