using HackTruda.ViewModels.Messages;
using Xamarin.Forms;

namespace HackTruda.Views.Messages
{
    public class DialogMessageTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate _sentMessageTemplate;
        private readonly DataTemplate _receivedMessageTemplate;

        public DialogMessageTemplateSelector()
        {
            _sentMessageTemplate = new DataTemplate(typeof(SentMessageItemView));
            _receivedMessageTemplate = new DataTemplate(typeof(ReceivedMessageItemView));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (!(item is ChatMessageItemViewModel viewModel))
            {
                return null;
            }

            return viewModel.IsMy ? _sentMessageTemplate : _receivedMessageTemplate;
        }
    }
}
