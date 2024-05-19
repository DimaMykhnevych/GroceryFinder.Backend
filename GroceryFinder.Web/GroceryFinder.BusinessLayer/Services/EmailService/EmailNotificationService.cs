using AutoMapper;
using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.DataLayer.Enums;
using GroceryFinder.DataLayer.Models;
using GroceryFinder.DataLayer.Repositories.EmailQueueItemRepository;
using GroceryFinder.DataLayer.Repositories.GroceryStoreRepository;
using GroceryFinder.DataLayer.Repositories.PriceUpdateSubscriptionRepository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace GroceryFinder.BusinessLayer.Services.EmailService;

public class EmailNotificationService : IHostedService, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _checkInterval = TimeSpan.FromDays(1);
    private Timer _timer = null;

    public EmailNotificationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(async o => await SendPriceUpdateEmail(o), null, TimeSpan.Zero, _checkInterval);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    private async Task SendPriceUpdateEmail(object state)
    {
        using var scope = _serviceProvider.CreateScope();
        var priceUpdateSubscriptionRepository = scope.ServiceProvider.GetRequiredService<IPriceUpdateSubscriptionRepository>();
        var groceryStoreRepository = scope.ServiceProvider.GetRequiredService<IGroceryStoreRepository>();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
        var emailQueueItemRepository = scope.ServiceProvider.GetRequiredService<IEmailQueueItemRepository>();
        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

        var subscriptions = await priceUpdateSubscriptionRepository.GetSubscriptionsWithUserAndProductInfo();

        var queueItems = await emailQueueItemRepository.GetNotSentItemsAsync();
        foreach(var queueItem in queueItems)
        {
            var emailInfo = JsonSerializer.Deserialize<EmailQueueItemInfoDto>(queueItem.EmailInfoJson);
            var requiredSubscriptions = subscriptions.Where(s => s.ProductId == emailInfo.ProductGroceryStore.ProductId);

            foreach (var subscription in requiredSubscriptions)
            {
                var store = await groceryStoreRepository.Get(emailInfo.ProductGroceryStore.GroceryStoreId);
                if (queueItem.EmailType == EmailType.ProductPriceUpdate)
                {
                    PriceUpdateEmailInfoDto priceUpdateEmailInfoDto = new()
                    {
                        Receiver = subscription.AppUser,
                        GroceryStore = mapper.Map<GroceryStoreDto>(store),
                        Product = mapper.Map<ProductDto>(subscription.Product),
                        NewPrice = emailInfo.ProductGroceryStore.Price,
                        OldPrice = emailInfo.OldPrice.Value
                    };

                    await emailService.SendPriceUpdateEmail(priceUpdateEmailInfoDto);
                }

                if (queueItem.EmailType == EmailType.ProductAddedToStore)
                {
                    ProductAddedEmailInfoDto productAddedEmailInfoDto = new()
                    {
                        Receiver = subscription.AppUser,
                        GroceryStore = mapper.Map<GroceryStoreDto>(store),
                        Product = mapper.Map<ProductDto>(subscription.Product),
                        Price = emailInfo.ProductGroceryStore.Price
                    };

                    await emailService.SendPrroductAddedEmail(productAddedEmailInfoDto);
                }
            }
        }

        await UpdateQueueItems(emailQueueItemRepository, queueItems);
    }

    private async Task UpdateQueueItems(
        IEmailQueueItemRepository emailQueueItemRepository,
        IEnumerable<EmailQueueItem> emailQueueItems)
    {
        foreach(var emailQueueItem in emailQueueItems)
        {
            emailQueueItem.IsSent = true;
            await emailQueueItemRepository.Update(emailQueueItem);
            await emailQueueItemRepository.Save();
        }
    }
}

