using Factorio_HR.Services;

namespace Factorio_HR.ViewModels.DesignerViewModels
{
    public class MainWindowViewModelDesigner : MainWindowViewModel
    {
        public MainWindowViewModelDesigner() : base(null, new MessageBus())
        {
            ChatLog = @"[Chat] <ThisIsCrispy>: Hello World
[Chat] <ThisIsCrispy>: Testing chat";
            CommandText = "/ban \"ThisIsCrispy\"";
        }
    }
}