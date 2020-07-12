using Xamarin.Forms;

namespace HackTruda.ViewModels.Onboarding
{
    /// <summary>
    /// VM для айтема онбоардинга.
    /// </summary>
    public class OnboardingItemViewModel : BaseViewModel
    {
        public OnboardingItemViewModel(string text, ImageSource image)
        {
            Text = text;
            Image = image;
        }

        public string Text { get; }

        public ImageSource Image { get; }
    }
}
