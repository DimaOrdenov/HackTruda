using System;
using FFImageLoading.Svg.Forms;
using HackTruda.Definitions.Enums;
using HackTruda.Extensions;

namespace HackTruda.ViewModels.Authorization
{
    public class SocialItemViewModel : BaseViewModel
    {
        public SocialItemViewModel(SocialAuthType type)
        {
            Type = type;
        }

        public SocialAuthType Type { get; }

        public string Scheme => Type.GetEnumDescription();

        public SvgImageSource Image
        {
            get
            {
                switch (Type)
                {
                    case SocialAuthType.Vk:
                        return AppSvgImages.IcVk;
                    case SocialAuthType.Ok:
                        return AppSvgImages.IcOk;
                    case SocialAuthType.Fb:
                        return AppSvgImages.IcFb;
                    case SocialAuthType.Google:
                        return AppSvgImages.IcGoogle;
                }

                return null;
            }
        }
    }
}
