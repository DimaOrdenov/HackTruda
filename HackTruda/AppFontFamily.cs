using System;
using Xamarin.Essentials;

namespace HackTruda
{
    public static class AppFontFamily
    {
        public static string NunitoBold =>
            DeviceInfo.Platform == DevicePlatform.iOS ? "Nunito-Bold" : "Nunito-Bold.ttf#Nunito-Bold";

        public static string NunitoSemiBold =>
            DeviceInfo.Platform == DevicePlatform.iOS ? "Nunito-SemiBold" : "Nunito-SemiBold.ttf#Nunito-SemiBold";

        public static string NunitoRegular =>
            DeviceInfo.Platform == DevicePlatform.iOS ? "Nunito-Regular" : "Nunito-Regular.ttf#Nunito-Regular";

        public static string NunitoExtraBold =>
            DeviceInfo.Platform == DevicePlatform.iOS ? "Nunito-ExtraBold" : "Nunito-ExtraBold.ttf#Nunito-ExtraBold";
    }
}
