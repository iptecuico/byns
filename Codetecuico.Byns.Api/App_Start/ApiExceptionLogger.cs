using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace Codetecuico.Byns.Api
{
    public class ApiExceptionLogger : ExceptionLogger
    {
        public async override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            //EventLogger.Exception(context.Exception);
            Debug.WriteLine(context.Exception);
            await base.LogAsync(context, cancellationToken);
        }
    }
}