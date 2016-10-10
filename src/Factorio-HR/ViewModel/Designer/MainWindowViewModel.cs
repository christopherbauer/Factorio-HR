using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Factorio_HR.Command;

namespace Factorio_HR.ViewModel.Designer
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IInteractiveLoginAdapter _interactiveLoginAdapter;

        public MainWindowViewModel(IInteractiveLoginAdapter interactiveLoginAdapter)
        {
            _interactiveLoginAdapter = interactiveLoginAdapter;
            SendCommandCommand = new SendMessageCommand(_interactiveLoginAdapter);
            InitUpdateLoop();
        }

        private void InitUpdateLoop()
        {
            Task.Run(() => ReadAndAppendToChatLog());
        }

        private async void ReadAndAppendToChatLog()
        {
            var currentOutput = _interactiveLoginAdapter.GetCurrentOutput();
            if (!string.IsNullOrEmpty(currentOutput))
            {
                ChatLog = string.Concat(ChatLog, currentOutput);
            }
            await Task.Delay(TimeSpan.FromSeconds(5));
            Task.Run(() => ReadAndAppendToChatLog());
        }

        private string _chatLog;
        public string ChatLog
        {
            get { return _chatLog; }
            set
            {
                SetValue(ref _chatLog, value);
            }
        }

        public string CommandText { get; set; }
        public ICommand SendCommandCommand { get; set; }
    }
}