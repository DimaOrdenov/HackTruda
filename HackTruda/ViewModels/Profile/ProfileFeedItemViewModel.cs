using System;
using HackTruda.DataModels.Responses;
using HackTruda.ViewModels.Feed;

namespace HackTruda.ViewModels.Profile
{
    /// <summary>
    /// VM для поста в Профиле.
    /// </summary>
    public class ProfileFeedItemViewModel : FeedItemViewModel
    {
        public ProfileFeedItemViewModel(FeedResponse feed)
            : base(feed)
        {
        }
    }
}
