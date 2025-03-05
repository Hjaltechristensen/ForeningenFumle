using ForeningenFumle.Server.DataAccess;
using ForeningenFumle.Server.Repositories.AdminRepository;
using ForeningenFumle.Server.Repositories.ChatRepository;
using ForeningenFumle.Server.Repositories.EventRepository;
using ForeningenFumle.Server.Repositories.RegistrationRepository;
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
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepositoryEF>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();



builder.Services.AddScoped<AuthenticationService>();

// Authentication Configuration
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/login";
		options.LogoutPath = "/logout";
	});
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins", policy =>
	{
		policy.AllowAnyOrigin()  // Tillader alle domæner
			  .AllowAnyHeader()  // Tillader alle headers
			  .AllowAnyMethod(); // Tillader alle HTTP-metoder
	});
});
// Tilføj DbContext og konfigurér databaseforbindelsen
builder.Services.AddDbContext<FumleDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSignalR();


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


app.UseCors("AllowAllOrigins"); // Aktivér CORS-politikken
app.UseHttpsRedirection();

// Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapHub<ChatHubRepository>("/chathub");


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
