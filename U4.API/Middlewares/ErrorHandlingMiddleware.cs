
using U4.Domain.Exeptions;

namespace U4.API.Middlewares
{
    public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundExeption notfound)
            {
                logger.LogWarning(notfound, notfound.Message);
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notfound.Message);
            }
            catch (ForbidExeption forbid)
            {
                logger.LogWarning(forbid, forbid.Message);
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(forbid.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("something went wrong");
            }

        }
    }
}
