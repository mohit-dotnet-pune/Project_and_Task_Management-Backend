namespace Project___Task_Management_Backend.Middleware
{
    public class JwtVerificationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtVerificationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();
            Console.WriteLine(context.ToString());
            // Allow public routes
            if (path.StartsWith("/api/auth"))
            {
                await _next(context);
                return;
            }

            // Check if user is authenticated
            if (!context.User.Identity?.IsAuthenticated ?? false)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized: Login required");
                return;
            }

            // Check token expiration
            var exp = context.User.FindFirst("exp")?.Value;
            if (exp != null)
            {
                var expiryDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp)).UtcDateTime;

                if (expiryDate < DateTime.UtcNow)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token expired");
                    return;
                }
            }

            await _next(context);
        }
    }

}
