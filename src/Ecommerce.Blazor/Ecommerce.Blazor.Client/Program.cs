using Blazored.LocalStorage;
using Ecommerce.Blazor.Client.Auth;
using Ecommerce.Blazor.Client.Features.Auth;
using Ecommerce.Blazor.Client.Features.Cart;
using Ecommerce.Blazor.Client.Features.Category;
using Ecommerce.Blazor.Client.Features.Discount;
using Ecommerce.Blazor.Client.Features.Product;
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

builder.Services.AddHttpClient<IDiscountApiService, DiscountApiService>(client =>
{
    client.BaseAddress = apiAddress;
});

builder.Services.AddHttpClient<IProductApiService, ProductApiService>(client =>
{
    client.BaseAddress = apiAddress;
});

builder.Services.AddHttpClient<ICartApiService, CartApiService>(client =>
{
    client.BaseAddress = apiAddress;
});

// -- STATE MANAGEMENT --
builder.Services.AddScoped<AuthState>();
builder.Services.AddScoped<CategoryState>();
builder.Services.AddScoped<DiscountState>();
builder.Services.AddScoped<ProductState>();
builder.Services.AddScoped<CartState>();

// -- AUTHENTICATION & AUTHORIZATION --
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddTransient<AuthTokenHandler>();

// -- THIRD PARTY PACKAGES --
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
