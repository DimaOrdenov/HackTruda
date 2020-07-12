using System;
using System.Windows.Input;
using FFImageLoading.Svg.Forms;
using HackTruda.DataModels.Responses;
using HackTruda.Extensions;
using Xamarin.Forms;

namespace HackTruda.ViewModels.Feed
{
    /// <summary>
    /// VM для поста в Ленте.
    /// </summary>
    public class FeedItemViewModel : BaseViewModel
    {
        private bool _isLiked;
        private int _likesCount;

        public ICommand LikeTapCommand { get; }

        public FeedItemViewModel(FeedResponse feed)
        {
            Feed = feed;
            _likesCount = feed.LikedCount;
            LikeTapCommand = new Command(() =>
            {
                IsLiked = !_isLiked;

                if (IsLiked)
                {
                    LikesCount = LikesCount + 1;

                }
                else
                {
                    LikesCount = LikesCount - 1;
                }
            });
        }

        public FeedResponse Feed { get; }

        public int LikesCount
        {
            get => _likesCount;
            set => SetProperty(ref _likesCount, value);
        }

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
