using backend.Settings;
using Microsoft.Extensions.Options;

namespace backend.Services.PriceDropStrategies;

public class DoublePriceDropStrategy : DefaultPriceDropStrategy
{
    public DoublePriceDropStrategy(IOptionsMonitor<DefaultPriceDropSettings> settings) : base(settings)
    {
    }

    protected override float GetPriceDecrease(float initialPrice)
    {
        return base.GetPriceDecrease(initialPrice) * 2;
    }
}