using Microsoft.EntityFrameworkCore;
using PasswordManager.Models;

var builder = WebApplication.CreateBuilder(args);

// ƒобавление контекста данных в качестве сервиса
string connection = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.Run();