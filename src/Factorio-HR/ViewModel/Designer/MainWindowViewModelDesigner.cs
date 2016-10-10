namespace Factorio_HR.ViewModel.Designer
{
    public class MainWindowViewModelDesigner : MainWindowViewModel
    {
        public MainWindowViewModelDesigner() : base(null)
        {
            ChatLog = @"[Chat] <ThisIsCrispy>: Hello World
[Chat] <ThisIsCrispy>: Testing chat";
            CommandText = "/ban \"ThisIsCrispy\"";
        }
    }
}