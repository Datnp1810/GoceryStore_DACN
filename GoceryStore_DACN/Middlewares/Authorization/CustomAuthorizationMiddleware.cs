using GoceryStore_DACN.Exceptions;

namespace GoceryStore_DACN.Middlewares.Authorization
{
    public class CustomAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ForbiddenAccessException)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new
                {
                    StatusCode = 403,
                    Message = "Bạn không có quyền truy cập tài nguyên này"
                });
            }
        }
    }
}
