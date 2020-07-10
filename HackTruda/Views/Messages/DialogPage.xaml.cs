using System.Linq;
using System.Threading.Tasks;
using HackTruda.ViewControls;
using Xamarin.Forms;

namespace HackTruda.Views.Messages
{
    public partial class DialogPage : BasePage
    {
        public DialogPage()
        {
            InitializeComponent();
        }

        private async void StackLayoutChildAdded(object sender, ElementEventArgs e)
        {
            if (chatMessagesStackLayout.Children.LastOrDefault() != null)
            {
                Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(
                    async () =>
                    {
                        // Special delay for iOS
                        await Task.Delay(10);

                        await chatMessagesScrollView.ScrollToAsync(chatMessagesStackLayout, ScrollToPosition.End, false);
                    });
            }
        }
    }
}
