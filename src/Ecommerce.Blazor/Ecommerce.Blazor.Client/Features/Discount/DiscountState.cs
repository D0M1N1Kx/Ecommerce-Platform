using Ecommerce.Shared.DTOs.Discount.Responses;

namespace Ecommerce.Blazor.Client.Features.Discount;

public class DiscountState
{
    private readonly IDiscountApiService _discountApi;

    public event Action? OnChange;
    public List<DiscountResponse> Discounts { get; private set; } = [];

    public DiscountState(IDiscountApiService discountApi)
    {
        _discountApi = discountApi;
    }

    public async Task LoadDiscountsAsync()
    {
        Discounts = await _discountApi.GetAllAsync();
        NotifyStateChanged();
    }

    public async Task CreateDiscountAsync(string name, decimal percentage, DateTime? validUntil = null)
    {
        await _discountApi.CreateAsync(name, percentage, validUntil);
        await LoadDiscountsAsync();
    }
    
    private void NotifyStateChanged() => OnChange?.Invoke();
}