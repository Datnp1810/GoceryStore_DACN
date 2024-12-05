namespace GoceryStore_DACN.Middlewares.Authentication
{
    public class CustomAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedAccessException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new
                {
                    StatusCode = 401,
                    Message = "Bạn chưa đăng nhập hoặc token không hợp lệ"
                });
            }
        }
    }
}
