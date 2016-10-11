using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Factorio_HR.Services.Message;

namespace Factorio_HR.Services
{
    public class CmdAdapter : IInteractiveLoginAdapter
    {
        private readonly Process _plinkProcess;
        private readonly StreamWriter _streamWriter;
        private readonly ISettingService _settingService;
        private readonly IMessageBus _messageBus;
        private List<string> _newInput = new List<string>();

        public CmdAdapter(ISettingService settingService, IMessageBus messageBus)
        {
            _settingService = settingService;
            _messageBus = messageBus;
            _plinkProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                },
                EnableRaisingEvents = true
            };

            _plinkProcess.Start();
            _plinkProcess.BeginErrorReadLine();
            _plinkProcess.BeginOutputReadLine();
            _plinkProcess.ErrorDataReceived += OnErrorDataReceived;
            _plinkProcess.OutputDataReceived += OnOutputDataReceived;

            _streamWriter = _plinkProcess.StandardInput;
            InitializeConnection();
        }

        private void OnErrorDataReceived(object sender, DataReceivedEventArgs args)
        {
            _newInput.Add(args.Data);
            _messageBus.Message(new ChatMessage { Type = ChatMessageType.Error});
        }

        private void OnOutputDataReceived(object sender, DataReceivedEventArgs args)
        {
            _newInput.Add(args.Data);
            _messageBus.Message(new ChatMessage {Type = ChatMessageType.DataReceived });
    }

        private void InitializeConnection()
        {
            var command = string.Format(@"external\plink.exe {0}@{1} -pw {2} -ssh -P 22",
                _settingService.GetSetting<string>("AdminUserName"), _settingService.GetSetting<string>("ServerIP"),
                _settingService.GetSetting<string>("AdminUserPassword"));

            SendCommand(command);
        }

        public void SendCommand(string command)
        {
            _streamWriter.WriteLine(command);
            _streamWriter.Flush();
            _messageBus.Message(new ChatMessage());
        }
        

        public bool IsConnected
        {
            get
            {
                return !_plinkProcess.HasExited;
            }
        }

        private readonly object _lockObject = new object();
        public string GetCurrentOutput()
        {
            string currentOutput;
            lock (_lockObject)
            {
                currentOutput = string.Join(Environment.NewLine, _newInput);
                _newInput = new List<string>();
            }
            return currentOutput;
        }
    }
}