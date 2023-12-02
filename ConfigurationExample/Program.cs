using ConfigurationExample;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Services.AddControllersWithViews();

//supply an object if WeatherApiOptions (with 'weatherapi' section) as a service
builder.Services.Configure<WeatherApiOptions>(builder.Configuration.GetSection("weatherapi"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
// app.UseEndpoints(endpoints =>
// {
//     endpoints.Map("/config", async context =>
//     {
//         await context.Response.WriteAsync(app.Configuration["MyKey"]+ "\n");
//         await context.Response.WriteAsync(app.Configuration.GetValue<string>("MyKey") + "\n");
//         await context.Response.WriteAsync(app.Configuration.GetValue<int>("x", 10) + "\n");
//     });
// });
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();