using Marisa.Domain.Entities;
using Marisa.Domain.InfoErrors;

namespace Marisa.Infra.Data.UtilityExternal.Interface
{
    public interface ISendEmailUser
    {
        public Task<InfoErrors> SendEmail(User user);
        public InfoErrors SendCodeRandom(User user, int code);
    }
}
