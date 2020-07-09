using HackTruda.Extensions;
using HackTruda.ViewControls;

namespace HackTruda.Views.Profile
{
    public partial class ProfilePage : BasePage
    {
        public ProfilePage()
        {
            InitializeComponent();

            icIdea.SetTintColor(AppColors.Primary);
        }
    }
}
