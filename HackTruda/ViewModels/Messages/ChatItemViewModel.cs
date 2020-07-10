using System;
using System.Collections.Generic;
using System.Linq;
using HackTruda.Definitions.Models;

namespace HackTruda.ViewModels.Messages
{
    public class ChatItemViewModel : BaseViewModel
    {
        private IEnumerable<ChatMessageItemViewModel> _messages;

        public ChatItemViewModel(UserModel user)
        {
            User = user;
        }

        public UserModel User { get; }

        public IEnumerable<ChatMessageItemViewModel> Messages
        {
            get => _messages;
            set
            {
                SetProperty(ref _messages, value);

                OnPropertyChanged(nameof(LastMessage));
                OnPropertyChanged(nameof(NewMessagesCount));
            }
        }

        public ChatMessageItemViewModel LastMessage => Messages?.LastOrDefault();

        public int NewMessagesCount => Messages?.Count(x => x.IsNew) ?? 0;
    }
}
