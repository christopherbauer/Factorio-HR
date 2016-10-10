using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Factorio_HR.ViewModel.Designer
{
    public interface IInteractiveLoginAdapter
    {
        void SendCommand(string command);
        bool IsConnected { get; }
        string GetCurrentOutput();
    }

    public class PLinkAdapter : IInteractiveLoginAdapter
    {
        private readonly Process _plinkProcess;
        private readonly StreamWriter _streamWriter;
        private readonly StreamReader _streamReader;
        private readonly StreamReader _errorStreamReader;
        private readonly ISettingService _settingService;

        public PLinkAdapter(ISettingService settingService)
        {
            _settingService = settingService;
            _plinkProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                }
            };

            _plinkProcess.Start();

            //_plinkProcess.ErrorDataReceived += (sender, args) => { throw new Exception(sender.ToString()); };
            //_plinkProcess.OutputDataReceived += (sender, args) => { throw new Exception(sender.ToString()); };
            _streamWriter = _plinkProcess.StandardInput;
            _streamReader = _plinkProcess.StandardOutput;
            _errorStreamReader = _plinkProcess.StandardError;

            InitializeConnection();
        }

        private void InitializeConnection()
        {
            SendCommand(string.Format("external/plink.exe {0}@{1} -pw {2} -ssh -p 22",
                _settingService.GetSetting<string>("AdminUserName"), _settingService.GetSetting<string>("ServerIP"),
                _settingService.GetSetting<string>("AdminUserPassword")));
        }

        public void SendCommand(string command)
        {
            _streamWriter.WriteLine(command);
            _streamWriter.Flush();
            Console.WriteLine(GetCurrentOutput());
        }

        public string GetCurrentOutput()
        {
            var output = _streamReader.ReadToEnd();
            _plinkProcess.WaitForExit();
            return output;
        }

        public bool IsConnected
        {
            get
            {
                return !_plinkProcess.HasExited;
            }
        }
    }
}