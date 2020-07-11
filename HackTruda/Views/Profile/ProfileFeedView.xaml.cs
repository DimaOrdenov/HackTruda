using System;
using System.Collections.Generic;
using HackTruda.Extensions;
using Xamarin.Forms;

namespace HackTruda.Views.Profile
{
    public partial class ProfileFeedView : ContentView
    {
        public ProfileFeedView()
        {
            InitializeComponent();

            icMessage.SetStrokeTintColor(AppColors.Dark80);
        }
    }
}
