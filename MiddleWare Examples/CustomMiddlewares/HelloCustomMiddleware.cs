using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MiddleWare_Examples.CustomMiddlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class HelloCustomMiddleware
    {
        private readonly RequestDelegate _next;
        static readonly HttpClient client = new HttpClient();

        public HelloCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var values = httpContext.Request.Headers;
            //values.Add("X-Custom-Env", "TEST");

            ModifyHeaders(httpContext);
            //before logic
            if (httpContext.Request.Query.ContainsKey("firstname") &&
                httpContext.Request.Query.ContainsKey("lastname"))
            {
                string fullName = httpContext.Request.Query["firstname"] + " " +
                     httpContext.Request.Query["lastname"] + "\n";
                await httpContext.Response.WriteAsync(fullName);
            }
            await _next(httpContext);
            //after logic
            //client.DefaultRequestHeaders.Add("X-Custom-Env", "TEST");
            ModifyHeaders(httpContext);
        }

        private void ModifyHeaders(HttpContext httpContext)
        {
            httpContext.Request.Headers.Remove("Upgrade-Insecure-Requests");
            //client.DefaultRequestHeaders.Remove("Upgrade-Insecure-Requests");
            client.DefaultRequestHeaders.Add("X-Custom-Env", "TEST");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HelloCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseHelloCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HelloCustomMiddleware>();
        }
    }
}
