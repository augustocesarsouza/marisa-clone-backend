using Marisa.Domain.Entities;
using Marisa.Domain.InfoErrors;

namespace Marisa.Domain.Authentication
{
    public interface ITokenGeneratorUser
    {
        InfoErrors<TokenOutValue> Generator(User user);
        InfoErrors<TokenOutValue> GeneratorTokenUrlChangeEmail(User user);
    }
}
