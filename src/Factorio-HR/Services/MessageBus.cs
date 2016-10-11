using System;
using System.Collections.Generic;
using Factorio_HR.Services.Message;

namespace Factorio_HR.Services
{
    public class MessageBus : IMessageBus
    {
        private readonly Dictionary<Type, IDictionary<Guid, Action<IMessageType>>> _callbacks;

        public MessageBus()
        {
            _callbacks = new Dictionary<Type, IDictionary<Guid, Action<IMessageType>>>();
        }

        public void RegisterForCallback<T>(Action<IMessageType> action) where T : IMessageType
        {
            if (!_callbacks.ContainsKey(typeof(T)))
            {
                _callbacks.Add(typeof(T), new Dictionary<Guid, Action<IMessageType>>());
            }
            _callbacks[typeof(T)].Add(Guid.NewGuid(), action);
        }

        public void Message<T>(T message) where T : IMessageType
        {
            if (_callbacks.ContainsKey(typeof(T)))
            {
                foreach (var callback in _callbacks[typeof(T)])
                {
                    callback.Value(message);
                }
            }
        }
    }
}