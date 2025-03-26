namespace Marisa.Application.CodeRandomUser.Interface
{
    public interface IQuantityAttemptChangePasswordDictionary
    {
        public void Add(string key, int valueCode);
        public void AddTimer(string key, int valueCode);
        public bool Container(string key);
        public (int value, TimeSpan? timeLeft) GetKey(string key);
        public void Remove(string key);
    }
}