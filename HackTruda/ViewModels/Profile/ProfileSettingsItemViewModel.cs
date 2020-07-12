using System;
using System.Windows.Input;
using HackTruda.Definitions.Enums;

namespace HackTruda.ViewModels.Profile
{
    /// <summary>
    /// VM для айтема настройки в профиле.
    /// </summary>
    public class ProfileSettingsItemViewModel : BaseViewModel
    {
        private bool _isToggled;

        public ICommand TapCommand { get; set; }

        public ProfileSettingsItemViewModel(ProfileSettingsItemType type, string title)
        {
            Type = type;
            Title = title;
        }

        public ProfileSettingsItemType Type { get; }

        public string Title { get; }

        public bool IsToggled
        {
            get => _isToggled;
            set => SetProperty(ref _isToggled, value);
        }
    }
}
