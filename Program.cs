using FastFood.Service;
using FastFood.Service.Interface;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Runtime.Serialization;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Add Cookie Authentication 



string? cookieAuth = builder.Configuration["CookieAuth:Name"];
if (cookieAuth != null)
{
    builder.Services.AddAuthentication(cookieAuth).AddCookie(cookieAuth, options =>
    {
        options.Cookie.Name = cookieAuth;
       // options.LoginPath = "/Customer/Login";

    });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("Customer", policy => policy.RequireClaim("CustID"));
    });
}




// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IAESService, AESService>();
builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
