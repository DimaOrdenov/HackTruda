using FFImageLoading.Svg.Forms;
using Xamarin.Essentials;

namespace HackTruda
{
    public static class AppSvgImages
    {
        public static SvgImageSource IcSettings => SvgImageSource.FromFile("ic_settings.svg");

        public static SvgImageSource IcCamera => SvgImageSource.FromFile("ic_camera.svg");

        public static SvgImageSource IcIdea => SvgImageSource.FromFile("ic_idea.svg");

        public static SvgImageSource IcArrowLeft => SvgImageSource.FromFile("ic_arrow_left.svg");

        public static SvgImageSource IcChevronRight => SvgImageSource.FromFile("ic_chevron_right.svg");

        public static SvgImageSource IcSend => SvgImageSource.FromFile("ic_send.svg");

        public static SvgImageSource IcMessageCircle => SvgImageSource.FromFile("ic_message_circle.svg");

        public static SvgImageSource IcPlus => SvgImageSource.FromFile("ic_plus.svg");

        public static SvgImageSource IcHeartInactive => SvgImageSource.FromFile("ic_heart_inactive.svg");

        public static SvgImageSource IcHeartActive => SvgImageSource.FromFile("ic_heart_active.svg");

        public static SvgImageSource IcVk => SvgImageSource.FromFile("ic_vk.svg");

        public static SvgImageSource IcOk => SvgImageSource.FromFile("ic_ok.svg");

        public static SvgImageSource IcFb => SvgImageSource.FromFile("ic_fb.svg");

        public static SvgImageSource IcGoogle => SvgImageSource.FromFile("ic_google.svg");

        public static SvgImageSource IcEye => SvgImageSource.FromFile("ic_eye.svg");

        public static SvgImageSource IcEyeOff => SvgImageSource.FromFile("ic_eye_off.svg");

        // Android broken svg
        public static SvgImageSource IcSearch => DeviceInfo.Platform == DevicePlatform.iOS ? SvgImageSource.FromFile("ic_search.svg") : SvgImageSource.FromFile("ic_search_icon.svg");

        public static SvgImageSource IcChevronBottom => SvgImageSource.FromFile("ic_chevron_bottom.svg");
    }
}