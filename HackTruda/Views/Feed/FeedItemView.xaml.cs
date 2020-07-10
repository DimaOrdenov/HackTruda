using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HackTruda.Views.Feed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedItemView : ContentView
    {
        public FeedItemView()
        {
            InitializeComponent();
        }
    }
}