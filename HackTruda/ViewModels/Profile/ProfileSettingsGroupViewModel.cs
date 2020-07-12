using System.Collections.Generic;

namespace HackTruda.ViewModels.Profile
{
    /// <summary>
    /// VM для группы настроек профиля.
    /// </summary>
    public class ProfileSettingsGroupViewModel : List<ProfileSettingsItemViewModel>
    {
        public ProfileSettingsGroupViewModel(string header, IEnumerable<ProfileSettingsItemViewModel> items)
        {
            Header = header;
            AddRange(items);
        }

        public string Header { get; }
    }
}
