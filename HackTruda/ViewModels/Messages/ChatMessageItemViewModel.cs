using System;

namespace HackTruda.ViewModels.Messages
{
    public class ChatMessageItemViewModel : BaseViewModel
    {
        private bool _isNew;
        private DateTime _messageTime;

        public ChatMessageItemViewModel(int fromUserId, int currentUserId, string text)
        {
            FromUserId = fromUserId;
            CurrentUserId = currentUserId;
            Text = text;

            MessageTime = DateTime.Now;
        }

        public int FromUserId { get; }

        public int CurrentUserId { get; }

        public string Text { get; }

        public DateTime MessageTime
        {
            get => _messageTime;
            set => SetProperty(ref _messageTime, value);
        }

        public bool IsNew
        {
            get => _isNew;
            set => SetProperty(ref _isNew, value);
        }

        public bool IsMy => FromUserId == CurrentUserId;
    }
}
