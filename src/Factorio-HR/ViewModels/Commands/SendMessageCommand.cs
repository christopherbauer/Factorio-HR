using System;
using System.Windows.Input;
using Factorio_HR.Services;

namespace Factorio_HR.ViewModels.Commands
{
    public class SendMessageCommand : ICommand
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly IInteractiveLoginAdapter _interactiveLoginAdapter;

        public SendMessageCommand(MainWindowViewModel mainWindowViewModel, IInteractiveLoginAdapter interactiveLoginAdapter)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _interactiveLoginAdapter = interactiveLoginAdapter;
        }

        public bool CanExecute(object parameter)
        {
            return _interactiveLoginAdapter.IsConnected;
        }

        public void Execute(object parameter)
        {
            _interactiveLoginAdapter.SendCommand(_mainWindowViewModel.CommandText);
        }

        public event EventHandler CanExecuteChanged;
    }
}