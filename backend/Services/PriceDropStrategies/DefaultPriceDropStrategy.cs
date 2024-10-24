using backend.Dto;
using backend.Model;
using Microsoft.Extensions.Options;

namespace backend.Services.PriceDropStrategies;

public class DefaultPriceDropStrategy : IPriceDropStrategy
{
    private readonly IOptionsMonitor<Settings.DefaultPriceDropSettings> _settings;

    public DefaultPriceDropStrategy(IOptionsMonitor<Settings.DefaultPriceDropSettings> settings)
    {
        _settings = settings;
    }

    public BunDto GetUpdatedBun(Bun bun)
    {
        TimeSpan timeSinceBaked = DateTime.Now - bun.BakedTime;
        int priceDropInterval = _settings.CurrentValue.PriceDropIntervalMinutes;
        int intervalsPassed = (int)(timeSinceBaked.TotalMinutes / priceDropInterval);
        DateTime nextPriceDrop = bun.BakedTime.AddMinutes(priceDropInterval * (intervalsPassed + 1));
        TimeSpan timeUntilPriceChange; 
        bool willBeThrownOut = false;
        if(nextPriceDrop < bun.DiscardTime)
        {
            timeUntilPriceChange = nextPriceDrop - DateTime.Now;
        }
        else
        {
            willBeThrownOut = true;
            timeUntilPriceChange = bun.DiscardTime - DateTime.Now;
        }

        float priceDecrease = GetPriceDecrease(bun.InitialPrice);
        float totalPriceDecrease = priceDecrease * intervalsPassed;
        float currentPrice = bun.InitialPrice - totalPriceDecrease;

        return new BunDto
        {
            Key = bun.Key,
            Type = bun.Type,
            InitialPrice = bun.InitialPrice,
            CurrentPrice = Math.Max(currentPrice, 0),
            NextPrice = Math.Max(currentPrice - priceDecrease, 0),
            WillBeThrownOut = willBeThrownOut,
            TimeUntilPriceChange = timeUntilPriceChange
        };
    }

    protected virtual float GetPriceDecrease(float initialPrice)
    {
        return initialPrice * _settings.CurrentValue.PriceDropCoef;
    }
}