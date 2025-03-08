using brevo_csharp.Model;
using Marisa.Domain.InfoErrors;

namespace Marisa.Infra.Data.UtilityExternal.Interface
{
    public interface ITransactionalEmailApiUti
    {
        public InfoErrors SendTransacEmailWrapper(SendSmtpEmail sendSmtpEmail);
    }
}
