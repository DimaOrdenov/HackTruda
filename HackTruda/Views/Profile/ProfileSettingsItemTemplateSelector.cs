using HackTruda.Definitions.Enums;
using HackTruda.ViewModels.Profile;
using Xamarin.Forms;

namespace HackTruda.Views.Profile
{
    public class ProfileSettingsItemTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate _navigationItem;

        private readonly DataTemplate _switchItem;

        public ProfileSettingsItemTemplateSelector()
        {
            _navigationItem = new DataTemplate(typeof(ProfileSettingsItemNavigationView));
            _switchItem = new DataTemplate(typeof(ProfileSettingsItemSwitchView));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (!(item is ProfileSettingsItemViewModel viewModel))
            {
                return null;
            }

            return viewModel.Type == ProfileSettingsItemType.Navigation ? _navigationItem : _switchItem;
        }
    }
}
