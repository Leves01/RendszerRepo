using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RendszerRepo.Services;
using RendszerRepo.Web;
using RendszerRepo.Web.Services;
using RendszerRepo.Web.Services.Contracts;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using RendszerRepo.Web.AuthProviders;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7129/") });
builder.Services.AddScoped<FEIPartService, FEPartService>();
builder.Services.AddScoped<FEIUserService, FEUserService>();
builder.Services.AddScoped<FEIStorageService, FEStorageService>();
builder.Services.AddScoped<FEIProjectService, FEProjectService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

var serviceProvider = builder.Build().Services;

var localStorage = serviceProvider.GetRequiredService<ILocalStorageService>();

await localStorage.RemoveItemAsync("AppSettings:Token");


await builder.Build().RunAsync();
