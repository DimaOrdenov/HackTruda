using FFImageLoading.Forms;
using HackTruda.BusinessLogic.Interfaces;
using HackTruda.DataModels.Responses;
using HackTruda.Definitions;
using HackTruda.Definitions.Enums;
using HackTruda.Extensions;
using HackTruda.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HackTruda.ViewModels.Feed
{
    public class FeedViewModel : PageViewModel
    {
        private readonly IPostsLogic _postsLogic;

        public ICommand RefreshCommand => new Command(async () => await RefreshItemsAsync());
        bool isRefreshing;
        private ObservableCollection<FeedResponse> items;

        public FeedViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService, IPostsLogic postsLogic)
            : base(navigationService, dialogService, debuggerService)
        {
            _postsLogic = postsLogic;

            IcMore = new CachedImage
            {
                Source = AppImages.IcMoreVertical,
            };

            IcMore.SetTintColor(Color.Black);
        }

        public ObservableCollection<FeedResponse> Items
        {
            get => items;
            set => SetProperty(ref items, value);
        }

        public CachedImage IcMore { get; }

        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }

        public override async Task OnAppearing()
        {
            if (PageDidAppear)
            {
                return;
            }

            State = PageStateType.Loading;

            Items = new ObservableCollection<FeedResponse>(await LoadFeed());

            State = PageStateType.Default;

            await base.OnAppearing();
        }

        private async Task<IEnumerable<FeedResponse>> LoadFeed()
        {
            IEnumerable<FeedResponse> result = null;
            await ExceptionHandler.PerformCatchableTask(
            new ViewModelPerformableAction(
                async () =>
                {
                    result = await _postsLogic.GetFeed(2, 1, CancellationToken);
                }));

            return result ?? new List<FeedResponse>();
        }

        private async Task RefreshItemsAsync()
        {
            IsRefreshing = true;
            Items = new ObservableCollection<FeedResponse>(await LoadFeed());
            IsRefreshing = false;
        }
    }
}
