using System;
using System.Collections.Generic;
using HackTruda.Extensions;
using Xamarin.Forms;

namespace HackTruda.Views.Profile
{
    public partial class ProfileSettingsItemNavigationView : ContentView
    {
        public ProfileSettingsItemNavigationView()
        {
            InitializeComponent();

            icChevron.SetStrokeTintColor(AppColors.Dark80);
        }
    }
}
