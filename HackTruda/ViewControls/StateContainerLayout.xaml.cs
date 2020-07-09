using System.Collections.ObjectModel;
using HackTruda.Definitions.Enums;
using HackTruda.ViewControls.State;
using HackTruda.ViewModels;
using Xamarin.Forms;

namespace HackTruda.ViewControls
{
    [ContentProperty(nameof(PageContent))]
    public partial class StateContainerLayout : BaseLayout
    {
        public StateContainerLayout()
        {
            InitializeComponent();

            SetBinding(PageStateProperty, new Binding(nameof(PageViewModel.State)));
        }

        public ObservableCollection<StateCondition> Conditions
        {
            get => stateContainer?.Conditions;
            set
            {
                if (stateContainer == null)
                {
                    return;
                }

                stateContainer.Conditions = value;
            }
        }

        public static readonly BindableProperty PageStateProperty = BindableProperty.Create(
            nameof(PageState),
            typeof(PageStateType),
            typeof(StateContainerLayout),
            propertyChanged: (sender, oldValue, newValue) =>
            {
                if (!(sender is StateContainerLayout view))
                {
                    return;
                }

                view.stateContainer.PageState = (PageStateType)newValue;
            });

        public View PageContent
        {
            get => pageContentLayout.Content;
            set => pageContentLayout.Content = value;
        }

        public PageStateType PageState
        {
            get => (PageStateType)GetValue(PageStateProperty);
            set => SetValue(PageStateProperty, value);
        }
    }
}
