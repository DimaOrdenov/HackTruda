using FFImageLoading.Svg.Forms;
using System.Windows.Input;
using Xamarin.Forms;
using HackTruda.Extensions;

namespace HackTruda.Views.Common
{
    public partial class ChatEditorView : ContentView
    {
        public ChatEditorView()
        {
            InitializeComponent();

            SetSendMessageUI();
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
           nameof(Text),
           typeof(string),
           typeof(ChatEditorView),
           defaultBindingMode: BindingMode.TwoWay,
           propertyChanged: (sender, oldValue, newValue) =>
           {
               ChatEditorView view = (ChatEditorView)sender;
               view.SetSendMessageUI();
           });

        public static readonly BindableProperty SendMessageCommandProperty = BindableProperty.Create(
           nameof(SendMessageCommand),
           typeof(ICommand),
           typeof(ChatEditorView),
           propertyChanged: (sender, oldValue, newValue) =>
           {
               ChatEditorView view = (ChatEditorView)sender;

               XamEffects.TouchEffect.SetColor(
                   view.sendMessageIconBlock,
                   newValue != null ? AppColors.RippleEffectWhite : Color.Transparent);

               view.SetSendMessageUI();
           });

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(ChatEditorView));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public ICommand SendMessageCommand
        {
            get => (ICommand)GetValue(SendMessageCommandProperty);
            set => SetValue(SendMessageCommandProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        private void SetSendMessageUI()
        {
            bool canSend = !(string.IsNullOrEmpty(Text) || SendMessageCommand == null);

            SvgImageSource icon = AppSvgImages.IcSend;
            icon.SetStrokeTintColor(canSend ? AppColors.Primary : AppColors.Grey);

            sendMessageIcon.Source = icon;

            sendMessageIconBlock.InputTransparent = !canSend;
        }
    }
}
