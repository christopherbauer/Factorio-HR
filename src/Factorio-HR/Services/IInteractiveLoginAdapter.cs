namespace Factorio_HR.Services
{
    public interface IInteractiveLoginAdapter
    {
        void SendCommand(string command);
        bool IsConnected { get; }
        string GetCurrentOutput();
    }
}