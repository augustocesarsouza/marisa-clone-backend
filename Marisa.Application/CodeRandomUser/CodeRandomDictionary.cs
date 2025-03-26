using Marisa.Application.CodeRandomUser.Interface;
using System.Collections.Concurrent;

namespace Marisa.Application.CodeRandomUser
{
    public class CodeRandomDictionary : ICodeRandomDictionary
    {
        private readonly ConcurrentDictionary<string, int> _dictionaryCode = new();
        private readonly ConcurrentDictionary<string, Timer> _timers = new();

        public void Add(string key, int valueCode)
        {
            _dictionaryCode.AddOrUpdate(key, valueCode, (key, oldValue) => valueCode);

            StartRemoveTimer(key);
        }

        public bool Container(string key, int valueCode)
        {
            return _dictionaryCode.TryGetValue(key, out var value) && value == valueCode;
        }

        public bool Container(string key)
        {
            return _dictionaryCode.ContainsKey(key);
        }

        public void Remove(string key)
        {
            if (_dictionaryCode.TryRemove(key, out _))
            {
                // Se o item foi removido, cancela o temporizador
                _timers.TryRemove(key, out var timer);
                timer?.Dispose();
            }
        }

        private void StartRemoveTimer(string key)
        {
            var timer = new Timer(RemoveItemCallback, key, TimeSpan.FromMinutes(60), Timeout.InfiniteTimeSpan);
            _timers[key] = timer;
        }

        private void RemoveItemCallback(object? state)
        {
            if (state is string key)
            {
                Remove(key);  // Chama o método Remove para remover o item do dicionário
            }
        }
    }
}
