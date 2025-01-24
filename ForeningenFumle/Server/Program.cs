using ForeningenFumle.Server.DataAccess;
using ForeningenFumle.Server.Repositories.AdminRepository;
using ForeningenFumle.Server.Repositories.EventRepository;
using ForeningenFumle.Server.Services;
using ForeningenFumle.Shared.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IPasswordHasher<Person>, PasswordHasher<Person>>();
builder.Services.AddScoped<IAdminRepository, AdminRepositoryEF>();
builder.Services.AddScoped<IEventRepository, EventRepositoryEF>();

builder.Services.AddScoped<AuthenticationService>();


// Authentication Configuration
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/login";
		options.LogoutPath = "/logout";
	});

// Tilføj DbContext og konfigurér databaseforbindelsen
builder.Services.AddDbContext<FumleDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();

// Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
