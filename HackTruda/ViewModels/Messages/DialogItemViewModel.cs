using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HackTruda.Definitions.Models;

namespace HackTruda.ViewModels.Messages
{
    public class DialogItemViewModel : BaseViewModel
    {
        private ObservableCollection<ChatMessageItemViewModel> _messages = new ObservableCollection<ChatMessageItemViewModel>();

        public ICommand TapCommand { get; set; }

        public DialogItemViewModel(UserModel user)
        {
            User = user;
        }

        public UserModel User { get; }

        public ObservableCollection<ChatMessageItemViewModel> Messages
        {
            get => _messages;
            set
            {
                SetProperty(ref _messages, value);

                OnPropertyChanged(nameof(LastMessage));
                OnPropertyChanged(nameof(NewMessagesCount));

                SetMessagesListener();
            }
        }

        public ChatMessageItemViewModel LastMessage => Messages?.LastOrDefault();

        public int NewMessagesCount => Messages?.Count(x => x.IsNew) ?? 0;

        private void SetMessagesListener()
        {
            _messages.CollectionChanged -= MessagesCollectionChanged;
            _messages.CollectionChanged += MessagesCollectionChanged;
        }

        private void MessagesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(LastMessage));
            OnPropertyChanged(nameof(NewMessagesCount));
        }
    }
}
