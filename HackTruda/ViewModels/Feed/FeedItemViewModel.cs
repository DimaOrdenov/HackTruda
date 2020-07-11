using System;
using System.Windows.Input;
using FFImageLoading.Svg.Forms;
using HackTruda.DataModels.Responses;
using HackTruda.Extensions;
using Xamarin.Forms;

namespace HackTruda.ViewModels.Feed
{
    public class FeedItemViewModel : BaseViewModel
    {
        private bool _isLiked;

        public ICommand LikeTapCommand { get; }

        public FeedItemViewModel(FeedResponse feed)
        {
            Feed = feed;

            LikeTapCommand = new Command(() => IsLiked = !_isLiked);
        }

        public FeedResponse Feed { get; }

        public bool IsLiked
        {
            get => _isLiked;
            set
            {
                SetProperty(ref _isLiked, value);

                OnPropertyChanged(nameof(HeartImage));
            }
        }

        public SvgImageSource HeartImage
        {
            get
            {
                if (_isLiked)
                {
                    return AppSvgImages.IcHeartActive;
                }

                SvgImageSource icHeart = AppSvgImages.IcHeartInactive;
                icHeart.SetStrokeTintColor(AppColors.Dark80);

                return icHeart;
            }
        }
    }
}
