using System.Collections.Generic;

namespace HackTruda.ViewModels.Profile
{
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
