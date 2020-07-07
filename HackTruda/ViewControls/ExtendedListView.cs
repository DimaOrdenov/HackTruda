using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace HackTruda.ViewControls
{
    public class ExtendedListView : ListView
    {
        public ExtendedListView()
            : base(ListViewCachingStrategy.RecycleElement)
        {
            VerticalOptions = LayoutOptions.FillAndExpand;

            ItemSelected += (sender, e) =>
            {
                if (sender is ExtendedListView listView)
                {
                    listView.SelectedItem = null;
                }
            };

            ItemTapped += (sender, e) =>
            {
                if (e.Item != null && ItemTappedCommand != null && ItemTappedCommand.CanExecute(e.Item))
                {
                    (sender as ExtendedListView)?.ItemTappedCommand?.Execute(e.Item);
                }
            };

            ItemAppearing += (sender, e) =>
            {
                IList itemsCollection = ItemsSource as IList;

                if (!(itemsCollection != null && itemsCollection.Count > 1 && itemsCollection[itemsCollection.Count - 1] == e.Item && IsLoadMoreEnabled && !IsLoadingMore && LoadMoreCommand != null))
                {
                    return;
                }

                (sender as ExtendedListView)?.LoadMoreCommand?.Execute((sender as ExtendedListView)?.LoadMoreCommandParameter);
            };
        }

        public static readonly BindableProperty ItemTappedCommandProperty = BindableProperty.Create(
            nameof(ItemTappedCommand),
            typeof(ICommand),
            typeof(ExtendedListView));

        public static readonly BindableProperty IsLoadMoreEnabledProperty = BindableProperty.Create(
            nameof(IsLoadMoreEnabled),
            typeof(bool),
            typeof(ExtendedListView));

        public static readonly BindableProperty IsLoadingMoreProperty = BindableProperty.Create(
            nameof(IsLoadingMore),
            typeof(bool),
            typeof(ExtendedListView),
            propertyChanged: (sender, oldValue, newValue) =>
            {
                if (!(sender is ExtendedListView view))
                {
                    return;
                }

                switch (newValue as bool?)
                {
                    case true:

                        ActivityIndicator footerLoadMoreIndicator = new ActivityIndicator
                        {
                            Color = view.RefreshControlColor,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                        };
                        footerLoadMoreIndicator.SetBinding(ActivityIndicator.IsRunningProperty, new Binding(nameof(IsLoadingMore)));
                        footerLoadMoreIndicator.SetBinding(IsVisibleProperty, new Binding(nameof(IsLoadingMore)));
                        footerLoadMoreIndicator.BindingContext = view;

                        view.Footer = new ContentView
                        {
                            HeightRequest = 45,
                            Padding = 8,
                            Content = footerLoadMoreIndicator,
                        };

                        break;
                    default:

                        view.Footer = null;

                        break;
                }
            });

        public static readonly BindableProperty LoadMoreCommandProperty = BindableProperty.Create(
            nameof(LoadMoreCommand),
            typeof(ICommand),
            typeof(ExtendedListView));

        public static readonly BindableProperty LoadMoreCommandParameterProperty = BindableProperty.Create(
            nameof(LoadMoreCommandParameter),
            typeof(object),
            typeof(ExtendedListView));

        public static readonly BindableProperty HasDefaultItemSelectionProperty = BindableProperty.Create(
            nameof(HasDefaultItemSelection),
            typeof(bool),
            typeof(ExtendedListView),
            true);

        public ICommand ItemTappedCommand
        {
            get => (ICommand)GetValue(ItemTappedCommandProperty);
            set => SetValue(ItemTappedCommandProperty, value);
        }

        public bool IsLoadMoreEnabled
        {
            get => (bool)GetValue(IsLoadMoreEnabledProperty);
            set => SetValue(IsLoadMoreEnabledProperty, value);
        }

        public bool IsLoadingMore
        {
            get => (bool)GetValue(IsLoadingMoreProperty);
            set => SetValue(IsLoadingMoreProperty, value);
        }

        public ICommand LoadMoreCommand
        {
            get => (ICommand)GetValue(LoadMoreCommandProperty);
            set => SetValue(LoadMoreCommandProperty, value);
        }

        public object LoadMoreCommandParameter
        {
            get => GetValue(LoadMoreCommandParameterProperty);
            set => SetValue(LoadMoreCommandParameterProperty, value);
        }

        public bool HasDefaultItemSelection
        {
            get => (bool)GetValue(HasDefaultItemSelectionProperty);
            set => SetValue(HasDefaultItemSelectionProperty, value);
        }
    }
}
