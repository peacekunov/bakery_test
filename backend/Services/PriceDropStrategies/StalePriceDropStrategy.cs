using backend.Dto;
using backend.Model;
using Microsoft.Extensions.Options;

namespace backend.Services.PriceDropStrategies;

public class StalePriceDropStrategy : IPriceDropStrategy
{
    private readonly IOptionsMonitor<Settings.StalePriceDropSettings> _settings;

    public StalePriceDropStrategy(IOptionsMonitor<Settings.StalePriceDropSettings> settings)
    {
        _settings = settings;
    }

    public BunDto GetUpdatedBun(Bun bun)
    {
        float currentPrice = bun.InitialPrice;
        float nextPrice = Math.Max(bun.InitialPrice * _settings.CurrentValue.PriceDropCoef, 0);
        TimeSpan timeUntilPriceChange;
        bool willBeThrownOut = false;

        if(DateTime.Now >= bun.StaleTime)
        {
            willBeThrownOut = true;
            currentPrice = nextPrice;
            timeUntilPriceChange = bun.DiscardTime - DateTime.Now;
        }
        else
        {
            timeUntilPriceChange = bun.StaleTime - DateTime.Now;
        }
 
        return new BunDto
        {
            Key = bun.Key,
            Type = bun.Type,
            InitialPrice = bun.InitialPrice,
            CurrentPrice = currentPrice,
            NextPrice = nextPrice,
            WillBeThrownOut = willBeThrownOut,
            TimeUntilPriceChange = timeUntilPriceChange
        };
    }
}