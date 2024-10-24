using backend.Enums;
using backend.Model;
using backend.Repository;
using backend.Settings;
using Microsoft.Extensions.Options;

namespace backend.Services;

public class BakingService
{
    private static readonly Random _random = new Random();

    private readonly IRepository<Bun> _bunRepository;
    private readonly IOptionsMonitor<List<BunSettings>> _bunSettingList;
    private readonly ILogger<ShelfUpdateService> _logger;

    public BakingService(IRepository<Bun> bunRepository, IOptionsMonitor<List<BunSettings>> bunSettingList, ILogger<ShelfUpdateService> logger)
    {
        _bunRepository = bunRepository;
        _bunSettingList = bunSettingList;
        _logger = logger;
    }

    public async Task Bake(int count)
    {
        var bunTypes = Enum.GetValues<BunType>();
        var now = DateTime.Now;

        await _bunRepository.CreateAsync(Enumerable.Range(0, count).Select(_ =>
        {
            var bunType = bunTypes[_random.Next(bunTypes.Length)];
            var bunSettings = _bunSettingList.CurrentValue.Single(b => b.Type == bunType);

            var bun = new Bun
            {
                Key = Guid.NewGuid(),
                Type = bunType,
                InitialPrice = bunSettings.Price,
                BakedTime = now,
                StaleTime = now.AddHours(bunSettings.FreshHours),
                DiscardTime = now.AddHours(bunSettings.ShelfLifeHours)
            };

            _logger.LogInformation($"BUN BAKED: {bun.ToString()}");

            return bun;
        }));
    }
}