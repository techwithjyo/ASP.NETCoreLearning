using MiddleWare_Examples.CustomMiddlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<MyCustomMiddleware>();
//builder.Services.AddTransient<HelloCustomMiddleware>();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");


//UseWhen is used to enable conditional access to Middleware
app.UseWhen(
    context => context.Request.Query.ContainsKey("username"),
    app =>{
        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Hello From Middleware Branch \n");
            await next();
        });
    });


app.Use(async (context, next) =>
{
    // Do work that can write to the Response.
    await context.Response.WriteAsync("Hello From Run Middleware1 \n");
    // Do logging or other work that doesn't write to the Response.
    await next(context);
});

//app.UseMiddleware<MyCustomMiddleware>();

//Replacement of the above line, Just for making use of UseMy() kind of method like UseAuthentication, UseRazorPages
app.UseMyCustomMiddleware();



app.Use(async (context, next) =>
{
    // Do work that can write to the Response.
    await context.Response.WriteAsync("Hello From Run Middleware2 \n");
    // Do logging or other work that doesn't write to the Response.
    await next(context);
});

app.UseHelloCustomMiddleware();
//Terminating Middleware
app.Run(async (context) => {
    await context.Response.WriteAsync("Hello From Run Middleware3 \n");
});

app.Run();
