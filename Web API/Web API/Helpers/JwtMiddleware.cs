using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Web_API.Authenticate;

namespace Web_API.Helpers
{
    public class JwtMiddleware 
    {
        private readonly RequestDelegate _next;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(JwtMiddleware));

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            _log4net.Info("Context" + context);
            await _next.Invoke(context);
        }
    }
}
