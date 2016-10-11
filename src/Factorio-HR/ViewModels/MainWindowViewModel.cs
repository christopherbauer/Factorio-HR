using System;
using System.Windows.Input;
using Factorio_HR.Services;
using Factorio_HR.Services.Message;
using Factorio_HR.ViewModels.Commands;

namespace Factorio_HR.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IInteractiveLoginAdapter _interactiveLoginAdapter;
        private readonly IMessageBus _messageBus;

        public MainWindowViewModel(IInteractiveLoginAdapter interactiveLoginAdapter, IMessageBus messageBus)
        {
            _interactiveLoginAdapter = interactiveLoginAdapter;
            _messageBus = messageBus;
            SendCommandCommand = new SendMessageCommand(this, _interactiveLoginAdapter);
            InitMessageBus();
        }


        private void InitMessageBus()
        {
            _messageBus.RegisterForCallback<ChatMessage>(ReadAndAppendToChatLog);
        }


        private void ReadAndAppendToChatLog(IMessageType messageType)
        {
            if (messageType.GetType() != typeof(ChatMessage))
            {
                throw new ArgumentException("Unsupported message type");
            }
            var message = (ChatMessage) messageType;
            if (message.Type == ChatMessageType.Error)
            {
                throw new AggregateException(_interactiveLoginAdapter.GetCurrentOutput());
            }
            var currentOutput = _interactiveLoginAdapter.GetCurrentOutput();
            if (!string.IsNullOrEmpty(currentOutput))
            {
                ChatLog = string.Concat(ChatLog, Environment.NewLine, currentOutput);
            }
        }

        private string _chatLog;

        public string ChatLog
        {
            get { return _chatLog; }
            set { SetValue(ref _chatLog, value); }
        }

        public string CommandText { get; set; }
        public ICommand SendCommandCommand { get; set; }
    }
}