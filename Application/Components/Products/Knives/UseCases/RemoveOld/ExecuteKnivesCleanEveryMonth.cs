using Application.Components.Products.UseCases.RemoveOld;
using Domain.Component.Product.Knife;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.Components.Products.Knives.UseCases.RemoveOld;

public class ExecuteKnivesCleanEveryMonth : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly TimeSpan _interval = TimeSpan.FromDays(30);
    private readonly ILogger<RemoveOldUnusedKnivesService> _logger;

    public ExecuteKnivesCleanEveryMonth(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<RemoveOldUnusedKnivesService> logger
    )
    {
        this._serviceScopeFactory = serviceScopeFactory;
        this._logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var removeKnivesService = scope.ServiceProvider.GetService<IRemoveOldProduct<Knife>>();
            
            try
            {
                _logger.LogInformation("Removing old unused products at {time}", DateTime.Now);
                if (removeKnivesService is null)
                    throw new AggregateException("RemoveKnivesService not found");
                await removeKnivesService.RemoveOldUnusedProducts();
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error while removing old unused products");
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}