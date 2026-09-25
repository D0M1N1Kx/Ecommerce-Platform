using Blazored.LocalStorage;
using Ecommerce.Blazor.Client.Features.Auth;
using Ecommerce.Blazor.Client.Features.Cart;
using Ecommerce.Blazor.Client.Features.Category;
using Ecommerce.Blazor.Client.Features.Discount;
using Ecommerce.Blazor.Client.Features.Product;
using Ecommerce.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

var apiAddress = new Uri("http://localhost:5142/");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddBlazoredLocalStorage();

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

builder.Services.AddScoped<AuthState>();
builder.Services.AddScoped<CategoryState>();
builder.Services.AddScoped<DiscountState>();
builder.Services.AddScoped<ProductState>();
builder.Services.AddScoped<CartState>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Ecommerce.Blazor.Client._Imports).Assembly);

app.Run();
