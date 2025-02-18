using ForeningenFumle.Client;
using ForeningenFumle.Client.Services.AdminServices;
using ForeningenFumle.Client.Services.AuthServices;
using ForeningenFumle.Client.Services.EventServices;
using ForeningenFumle.Client.Services.MemberServices;
using ForeningenFumle.Client.Services.RegistrationServices;
using ForeningenFumle.Client.Shared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using ForeningenFumle.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string baseAddress = "https://foreningenfumleserver.azurewebsites.net/";

// Tilføj HttpClient for API-kommunikation
builder.Services.AddHttpClient<IAdminService, AdminService>(client =>
{
	client.BaseAddress = new Uri(baseAddress);
});

builder.Services.AddHttpClient<IMemberService, MemberService>(client =>
{
	client.BaseAddress = new Uri(baseAddress);
});

builder.Services.AddHttpClient<IEventService, EventService>(client =>
{
	client.BaseAddress = new Uri(baseAddress);
});

builder.Services.AddHttpClient<IRegistrationService, RegistrationService>(client =>
{
	client.BaseAddress = new Uri(baseAddress);
});

builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
{
	client.BaseAddress = new Uri(baseAddress);
});
builder.Services.AddScoped<UserState>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<LogoutTimeService>();




builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
