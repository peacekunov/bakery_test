using backend.Dto;
using backend.Hubs;
using backend.Model;
using backend.Repository;
using backend.Services.PriceDropStrategies;
using backend.Settings;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace backend.Services;

public class ShelfUpdateService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IRepository<Bun> _bunRepository;
    private readonly IOptionsMonitor<ShelfSettings> _shelfSettings;
    private readonly IOptionsMonitor<List<BunSettings>> _bunSettingList;
    private readonly IHubContext<ShelfHub> _hubContext;
    private readonly ILogger<ShelfUpdateService> _logger;

    public ShelfUpdateService(
        IServiceProvider serviceProvider, 
        IRepository<Bun> bunRepository, 
        IOptionsMonitor<ShelfSettings> shelfSettings,
        IOptionsMonitor<List<BunSettings>> bunSettingList, 
        IHubContext<ShelfHub> hubContext,
        ILogger<ShelfUpdateService> logger)
    {
        _serviceProvider = serviceProvider;
        _bunRepository = bunRepository;
        _shelfSettings = shelfSettings;
        _bunSettingList = bunSettingList;
        _hubContext = hubContext;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _bunRepository.DeleteManyAsync(b => b.DiscardTime <= DateTime.Now);

            var buns = await _bunRepository.GetAllAsync();
            var result = new List<BunDto>();
            foreach(var bun in buns)
            {
                var bunSettings = _bunSettingList.CurrentValue.Single(b => b.Type == bun.Type);
                var priceDropeStrategy = _serviceProvider.GetRequiredKeyedService<IPriceDropStrategy>(bunSettings.PriceDropStrategy);
                var dto = priceDropeStrategy.GetUpdatedBun(bun);
                result.Add(dto);

                _logger.LogInformation(dto.ToString());
            }

            await _hubContext.Clients.All.SendAsync(_shelfSettings.CurrentValue.UpdateMessage, result);

            await Task.Delay(TimeSpan.FromSeconds(_shelfSettings.CurrentValue.UpdateIntervalSec), stoppingToken);
        }
    }
}