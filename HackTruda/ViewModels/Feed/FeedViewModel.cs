using FFImageLoading.Forms;
using HackTruda.BusinessLogic.Interfaces;
using HackTruda.DataModels.Responses;
using HackTruda.Definitions;
using HackTruda.Definitions.Enums;
using HackTruda.Services.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HackTruda.ViewModels.Feed
{
    public class FeedViewModel : PageViewModel
    {
        public ICommand RefreshCommand => new Command(async () => await RefreshItemsAsync());
        private readonly IPostsLogic _postsLogic;
        private bool isRefreshing;

        public FeedViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService, IPostsLogic postsLogic)
            : base(navigationService, dialogService, debuggerService)
        {
            _postsLogic = postsLogic;
        }

        public ObservableCollection<FeedResponse> Items { get; private set; }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
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