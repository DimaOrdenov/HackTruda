using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using HackTruda.Containers;
using HackTruda.Definitions.Enums;
using HackTruda.Services.Interfaces;
using Xamarin.Forms;

namespace HackTruda.ViewControls.State
{
    [ContentProperty(nameof(Conditions))]
    public class StateContainer : ContentView
    {
        private readonly IDebuggerService _debuggerService;

        private ObservableCollection<StateCondition> _conditions = new ObservableCollection<StateCondition>();

        public StateContainer()
        {
            _debuggerService = IocInitializer.Container.Resolve<IDebuggerService>();

            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.FillAndExpand;
        }

        public ObservableCollection<StateCondition> Conditions
        {
            get => _conditions;
            set
            {
                _conditions = value;

                OnPropertyChanged();
            }
        }

        public static readonly BindableProperty PageStateProperty = BindableProperty.Create(
            nameof(PageState),
            typeof(PageStateType),
            typeof(StateContainer),
            PageStateType.None,
            propertyChanged: async (bindable, oldValue, newValue) =>
            {
                // Do not change content if state from or to MinorLoading
                if (!(bindable is StateContainer parent) ||
                    (PageStateType)newValue == PageStateType.MinorLoading || (PageStateType)oldValue == PageStateType.MinorLoading)
                {
                    return;
                }

                await parent.ChooseStateProperty((PageStateType)newValue);
            });

        public PageStateType PageState
        {
            get => (PageStateType)GetValue(PageStateProperty);
            set => SetValue(PageStateProperty, value);
        }

        public static void Init()
        {
            //for linker
        }

        private async Task ChooseStateProperty(PageStateType newValue)
        {
            if (Conditions == null || Conditions?.Count == 0)
            {
                return;
            }

            try
            {
                // Take last condition, if we will customise default conditions and add some new with same state
                if (_conditions?.LastOrDefault(state => state.PageState == newValue) is StateCondition stateCondition)
                {
                    if (Content != null)
                    {
                        Content.IsVisible = false;

                        await Task.Delay(30);
                    }

                    Content = stateCondition.Content ?? new ContentView();

                    Content.IsVisible = true;
                }
            }
            catch (Exception e)
            {
                _debuggerService.Log(e);
            }
        }
    }
}
