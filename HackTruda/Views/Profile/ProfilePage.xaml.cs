﻿using HackTruda.Extensions;
using HackTruda.ViewControls;

namespace HackTruda.Views.Profile
{
    public partial class ProfilePage : BasePage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            icIdea.SetTintColor(AppColors.Primary);
        }
    }
}
