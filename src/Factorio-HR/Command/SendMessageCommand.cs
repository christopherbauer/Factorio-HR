using System;
using System.Windows.Input;
using Factorio_HR.ViewModel.Designer;

namespace Factorio_HR.Command
{
    public class SendMessageCommand : ICommand
    {
        private readonly IInteractiveLoginAdapter _interactiveLoginAdapter;

        public SendMessageCommand(IInteractiveLoginAdapter interactiveLoginAdapter)
        {
            _interactiveLoginAdapter = interactiveLoginAdapter;
        }

        public bool CanExecute(object parameter)
        {
            return _interactiveLoginAdapter.IsConnected;
        }

        public void Execute(object parameter)
        {
            var message = (string) parameter;
            _interactiveLoginAdapter.SendCommand(message);
        }

        public event EventHandler CanExecuteChanged;
    }
}