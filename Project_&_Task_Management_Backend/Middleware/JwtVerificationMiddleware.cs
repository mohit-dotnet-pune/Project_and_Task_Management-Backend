using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;

using System.Text;

using System.Security.Claims;

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

            // Allow public routes

            if (path.StartsWith("/api/auth"))

            {

                await _next(context);

                return;

            }

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))

            {

                context.Response.StatusCode = 401;

                await context.Response.WriteAsync("Unauthorized: No token provided");

                return;

            }

            try

            {

                // Read from environment variables

                var key = Environment.GetEnvironmentVariable("JWT_KEY");

                var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");

                var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))

                    throw new Exception("JWT configuration missing in environment variables");

                var keyBytes = Encoding.UTF8.GetBytes(key.Trim());

                var tokenHandler = new JwtSecurityTokenHandler();

                var validatedToken = tokenHandler.ValidateToken(token, new TokenValidationParameters

                {

                    ValidateIssuer = true,

                    ValidateAudience = true,

                    ValidIssuer = issuer,

                    ValidAudience = audience,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),

                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken securityToken);

                var jwtToken = (JwtSecurityToken)securityToken;

                // Populate HttpContext.Items for GetUserId()

                var claimsDict = jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value);

                context.Items["User"] = claimsDict;

                // Populate context.User for [Authorize] and Roles

                var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");

                context.User = new ClaimsPrincipal(identity);

                await _next(context);

            }

            catch (SecurityTokenExpiredException)

            {

                context.Response.StatusCode = 401;

                await context.Response.WriteAsync("Token expired");

            }

            catch (Exception ex)

            {

                context.Response.StatusCode = 401;

                await context.Response.WriteAsync("Invalid token: " + ex.Message);

            }

        }

    }

}

