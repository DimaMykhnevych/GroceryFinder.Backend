using AutoMapper;
using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.DataLayer.Enums;
using GroceryFinder.DataLayer.Models;
using GroceryFinder.DataLayer.Repositories.EmailQueueItemRepository;
using GroceryFinder.DataLayer.Repositories.ProductGroceryStoreRepository;
using System.Text.Json;

namespace GroceryFinder.BusinessLayer.Services.ProductGroceryStoreService;

public class ProductGroceryStoreService : IProductGroceryStoreService
{
    private readonly IProductGroceryStoreRepository _productGroceryStoreRepository;
    private readonly IEmailQueueItemRepository _emailQueueItemRepository;
    private readonly IMapper _mapper;

    public ProductGroceryStoreService(
        IProductGroceryStoreRepository productGroceryStoreRepository,
        IEmailQueueItemRepository emailQueueItemRepository,
        IMapper mapper)
    {
        _productGroceryStoreRepository = productGroceryStoreRepository;
        _emailQueueItemRepository = emailQueueItemRepository;
        _mapper = mapper;
    }

    public async Task<ProductGroceryStoreDto> AddProductGroceryStore(ProductGroceryStoreDto productGroceryStoreDto)
    {
        ProductGroceryStore productGroceryStore = _mapper.Map<ProductGroceryStore>(productGroceryStoreDto);
        productGroceryStore.Id = new Guid();
        var addedProductGroceryStore = await _productGroceryStoreRepository.Insert(productGroceryStore);
        await _productGroceryStoreRepository.Save();

        var addedStore = _mapper.Map<ProductGroceryStoreDto>(addedProductGroceryStore);

        await AddPriceUpdateEmailsToQueue(addedStore, null, EmailType.ProductAddedToStore);

        return addedStore;
    }

    public async Task<IEnumerable<ProductGroceryStoreDto>> GetAllProductGroceryStores()
    {
        var productGroceryStores = await _productGroceryStoreRepository.GetAll();
        return _mapper.Map<IEnumerable<ProductGroceryStoreDto>>(productGroceryStores);
    }

    public async Task<ProductGroceryStoreDto> UpdateProductGroceryStore(ProductGroceryStoreDto productGroceryStoreDto)
    {
        var existingStore = await _productGroceryStoreRepository.GetProductGroceryStoreAsNoTracking(productGroceryStoreDto.Id);
        ProductGroceryStore productGroceryStore = _mapper.Map<ProductGroceryStore>(productGroceryStoreDto);
        await _productGroceryStoreRepository.Update(productGroceryStore);
        await _productGroceryStoreRepository.Save();

        var updatedStore = _mapper.Map<ProductGroceryStoreDto>(productGroceryStore);

        await AddPriceUpdateEmailsToQueue(updatedStore, existingStore.Price, EmailType.ProductPriceUpdate);

        return updatedStore;
    }

    private async Task AddPriceUpdateEmailsToQueue(ProductGroceryStoreDto productGroceryStore, double? oldPrice, EmailType emailType)
    {
        EmailQueueItem item = new()
        {
            Id = Guid.NewGuid(),
            EmailType = emailType,
            IsSent = false,
            EmailInfoJson = JsonSerializer.Serialize(
                new EmailQueueItemInfoDto
                {
                    ProductGroceryStore = productGroceryStore,
                    OldPrice = oldPrice
                }),
        };

        await _emailQueueItemRepository.Insert(item);
        await _emailQueueItemRepository.Save();
    }
}

