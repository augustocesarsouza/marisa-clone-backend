using Marisa.Application.CodeRandomUser.Interface;
using System.Collections.Concurrent;

namespace Marisa.Application.CodeRandomUser
{
    public class QuantityAttemptChangePasswordDictionary : IQuantityAttemptChangePasswordDictionary
    {
        private readonly ConcurrentDictionary<string, int> _dictionaryNumber = new();
        private readonly ConcurrentDictionary<string, Timer> _timers = new();
        private readonly ConcurrentDictionary<string, DateTime> _expirationTimes = new();
        private readonly int timeToDateTime = 10;

        public void Add(string key, int valueCode)
        {
            _dictionaryNumber.AddOrUpdate(key, valueCode, (key, oldValue) => valueCode);
        }

        public void AddTimer(string key, int valueCode)
        {
            _dictionaryNumber.AddOrUpdate(key, valueCode, (key, oldValue) => valueCode);

            DateTime expirationTime = DateTime.UtcNow.AddSeconds(this.timeToDateTime);
            _expirationTimes[key] = expirationTime;
            
            StartRemoveTimer(key);
        }

        public bool Container(string key)
        {
            return _dictionaryNumber.ContainsKey(key);
        }

        public (int value, TimeSpan? timeLeft) GetKey(string key)
        {
            int value = 0;
            TimeSpan? timeLeft = null;

            if (_dictionaryNumber.TryGetValue(key, out int storedValue))
            {
                value = storedValue;
            }

            if (_expirationTimes.TryGetValue(key, out DateTime expirationTime))
            {
                timeLeft = expirationTime - DateTime.UtcNow;
                if (timeLeft.Value.TotalSeconds <= 0)
                {
                    timeLeft = TimeSpan.Zero;
                }
            }

            return (value, timeLeft);
        }

        public void Remove(string key)
        {
            if (_dictionaryNumber.TryRemove(key, out _))
            {
                // Se o item foi removido, cancela o temporizador
                _timers.TryRemove(key, out var timer);
                timer?.Dispose();
            }
        }

        private void StartRemoveTimer(string key)
        {
            //var timer = new Timer(RemoveItemCallback, key, TimeSpan.FromMinutes(60), Timeout.InfiniteTimeSpan);
            var timer = new Timer(RemoveItemCallback, key, TimeSpan.FromSeconds(this.timeToDateTime), Timeout.InfiniteTimeSpan);
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
