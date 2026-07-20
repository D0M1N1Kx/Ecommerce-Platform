using Blazored.LocalStorage;
using Ecommerce.Blazor.Client.Auth;
using Ecommerce.Blazor.Client.Features.Auth;
using Ecommerce.Blazor.Client.Features.Category;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var apiAddress = new Uri("http://localhost:5142/");

// -- HTTP CLIENT REGISTRATIONS --
builder.Services.AddHttpClient<IAuthApiService, AuthApiService>(client =>
{
    client.BaseAddress = apiAddress;
});

builder.Services.AddHttpClient<ICategoryApiService, CategoryApiService>(client =>
{
    client.BaseAddress = apiAddress;
});

// -- STATE MANAGEMENT --
builder.Services.AddScoped<AuthState>();
builder.Services.AddScoped<CategoryState>();

// -- AUTHENTICATION & AUTHORIZATION --
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddTransient<AuthTokenHandler>();

// -- THIRD PARTY PACKAGES --
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
