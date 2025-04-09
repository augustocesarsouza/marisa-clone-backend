using Marisa.Domain.Entities;
using Marisa.Domain.InfoErrors;

namespace Marisa.Infra.Data.UtilityExternal.Interface
{
    public interface ISendEmailBrevo
    {
        public InfoErrors SendEmail(User user, string url);
        public InfoErrors SendCode(User user, int codeRandon);
        public InfoErrors SendUrlChangePassword(User user, string? token);
    }
}
