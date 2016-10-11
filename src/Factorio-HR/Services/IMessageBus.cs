using System;
using Factorio_HR.Services.Message;

namespace Factorio_HR.Services
{
    public interface IMessageBus
    {
        void RegisterForCallback<T>(Action<IMessageType> action) where T : IMessageType;
        void Message<T>(T message) where T : IMessageType;
    }
}