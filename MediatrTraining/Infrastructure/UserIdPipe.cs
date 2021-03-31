using MediatR;
using Microsoft.AspNetCore.Http;
using Services;
using Services.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MediatrTraining.Infrastructure
{
    public class UserIdPipe<TIn, TOut> : IPipelineBehavior<TIn, TOut>
    {
        private HttpContext _httpContext;

        public UserIdPipe(IHttpContextAccessor accessor)
        {
            _httpContext = accessor.HttpContext;
        }

        public async Task<TOut> Handle(TIn request, CancellationToken cancellationToken, RequestHandlerDelegate<TOut> next)
        {
            //var userId =_httpContext.User.Claims
            //    .FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))
            //    .Value;

            if (request is BaseRequest br)
            {
                br.UserId = "wwop";
            }

            var result = await next();

            if (result is Response<Car> carResponse)
            {
                carResponse.Data.Name += " CHECKED";
            }

            return result;
        }
    }
}
