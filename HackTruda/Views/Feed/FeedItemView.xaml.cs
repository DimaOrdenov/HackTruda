using HackTruda.Extensions;
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

            imageMore.Source = AppImages.IcMoreVertical;
            imageMore.SetTintColor(Color.Black);
        }
    }
}