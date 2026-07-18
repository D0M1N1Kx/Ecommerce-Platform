using Blazored.LocalStorage;
using Ecommerce.Blazor.Client.Auth;
using Ecommerce.Blazor.Client.Features.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<AuthState>();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddTransient<AuthTokenHandler>();

builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
