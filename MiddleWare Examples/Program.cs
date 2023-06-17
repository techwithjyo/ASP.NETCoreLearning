using MiddleWare_Examples.CustomMiddlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseMiddleware<MyCustomMiddleware>();

app.Use(async (context, next) =>
{
    // Do work that can write to the Response.
    await context.Response.WriteAsync("Hello From Run Middleware1 \n");
    // Do logging or other work that doesn't write to the Response.
    await next(context);
});

app.Use(async (context, next) =>
{
    // Do work that can write to the Response.
    await context.Response.WriteAsync("Hello From Run Middleware2 \n");
    // Do logging or other work that doesn't write to the Response.
    await next(context);
});


//Terminating Middleware
app.Run(async (context) => {
    await context.Response.WriteAsync("Hello From Run Middleware3 \n");
});

app.Run();
