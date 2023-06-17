using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MiddleWare_Examples.CustomMiddlewares
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("MyCustomMiddleware - Starts \n");
            await next(context);
            await context.Response.WriteAsync("MyCustomMiddleware - Ends \n");
        }
    }


    //Extra code for Extension Method
    public static class CustomMiddlewareExtension
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app)
        {
            var var = app.UseMiddleware<MyCustomMiddleware>();
            return var;
        }
    }
}
