
namespace Marisa.Application.CodeRandomUser.Interface
{
    public interface ICodeRandomDictionary
    {
        public void Add(string key, int valueCode);
        public bool Container(string key, int valueCode);
        public bool Container(string key);
        public void Remove(string key);
    }
}
