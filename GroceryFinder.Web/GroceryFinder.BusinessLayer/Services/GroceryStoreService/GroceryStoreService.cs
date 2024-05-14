using AutoMapper;
using GeoCoordinatePortable;
using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.DataLayer.Builders.GroceryStoreQueryBuilder;
using GroceryFinder.DataLayer.Enums;
using GroceryFinder.DataLayer.Models;
using GroceryFinder.DataLayer.Repositories.GroceryStoreRepository;
using GroceryFinder.DataLayer.Repositories.ProductRepository;

namespace GroceryFinder.BusinessLayer.Services.GroceryStoreService;

public class GroceryStoreService : IGroceryStoreService
{
    private readonly IGroceryStoreRepository _groceryStoreRepository;
    private readonly IProductRepository _productRepository;
    private readonly IGroceryStoreQueryBuilder _groceryStoreQueryBuilder;
    private readonly IMapper _mapper;

    public GroceryStoreService(
        IGroceryStoreRepository groceryStoreRepository,
        IProductRepository productRepository,
        IGroceryStoreQueryBuilder groceryStoreQueryBuilder,
        IMapper mapper)
    {
        _groceryStoreRepository = groceryStoreRepository;
        _productRepository = productRepository;
        _groceryStoreQueryBuilder = groceryStoreQueryBuilder;
        _mapper = mapper;
    }

    public async Task<GroceryStoreDto> AddGroceryStore(GroceryStoreDto groceryStoreDto)
    {
        GroceryStore groceryStore = _mapper.Map<GroceryStore>(groceryStoreDto);
        groceryStore.Id = new Guid();
        var addedGroceryStore = await _groceryStoreRepository.Insert(groceryStore);
        await _groceryStoreRepository.Save();
        return _mapper.Map<GroceryStoreDto>(addedGroceryStore);
    }

    public async Task<IEnumerable<GroceryStoreDto>> GetAllGroceryStores()
    {
        var groceryStores = await _groceryStoreRepository.GetAll();
        return _mapper.Map<IEnumerable<GroceryStoreDto>>(groceryStores);
    }

    public async Task<FoundGroceryStoreDto> SearchGroceryStores(GroceryStoreSearchDto groceryStoreSearchDto)
    {
        var searchMode = _mapper.Map<GroceryStoreSearchMode>(groceryStoreSearchDto.GroceryStoreSearchMode);
        var stores = _groceryStoreQueryBuilder
            .SetBaseGroceryStoreInfo()
            .SetProductToSearch(groceryStoreSearchDto.ProductId)
            .Build()
            .AsEnumerable();

        stores = SetRadiusInMeters(stores, groceryStoreSearchDto.Radius, groceryStoreSearchDto.Latitude, groceryStoreSearchDto.Longitude);
        stores = SetSearchMode(stores, searchMode, groceryStoreSearchDto.ProductId, groceryStoreSearchDto.Latitude, groceryStoreSearchDto.Longitude)
            .ToList();

        var product = await _productRepository.Get(groceryStoreSearchDto.ProductId);
        var productDto = _mapper.Map<ProductDto>(product);
        if (!stores.Any())
        {
            return new FoundGroceryStoreDto() { Product = productDto, FoundStores = Array.Empty<FoundProductGroceryStoreDto>() };
        }

        var storeDtos = stores.Select(s =>
            {
                var storeDto = _mapper.Map<GroceryStoreDto>(s);
                var productPrice = s.ProductGroceryStores.First(gs => gs.ProductId == product.Id).Price;
                return new FoundProductGroceryStoreDto
                {
                    GroceryStore = storeDto,
                    Price = productPrice,
                };
            })
            .ToList();

        return new FoundGroceryStoreDto() { Product = productDto, FoundStores = storeDtos };
    }

    private static IEnumerable<GroceryStore> SetRadiusInMeters(IEnumerable<GroceryStore> stores, double? radius, double? latitude, double? longitude)
    {
        if (radius == null || latitude == null || longitude == null)
        {
            return stores;
        }

        return stores.Where(s => StoreInRadius(s, radius.Value, latitude.Value, longitude.Value));
    }

    private static IEnumerable<GroceryStore> SetSearchMode(
        IEnumerable<GroceryStore> stores,
        GroceryStoreSearchMode? searchMode,
        Guid? productId,
        double? latitude,
        double? longitude)
    {
        if (searchMode == GroceryStoreSearchMode.NearestStores && latitude != null && longitude != null)
        {
            return stores
                .Select(s => new { Store = s, Distance = CalculateDistance(s, latitude.Value, longitude.Value) })
                .OrderBy(s => s.Distance)
                .Take(10)
                .Select(s => s.Store);
        }

        if (searchMode == GroceryStoreSearchMode.LowestPrices && productId != null)
        {
            return stores
                .Select(s => new { Store = s, s.ProductGroceryStores.First(gs => gs.ProductId == productId.Value).Price })
                .OrderBy(s => s.Price)
                .Take(10)
                .Select(s => s.Store);
        }

        return stores;
    }

    private static bool StoreInRadius(GroceryStore store, double radius, double latitude, double longitude)
    {
        var distance = CalculateDistance(store, latitude, longitude);
        return distance <= radius;
    }

    private static double CalculateDistance(GroceryStore store, double latitude, double longitude)
    {
        var storeGeo = new GeoCoordinate(store.Latitude, store.Longitude);
        var givenGeo = new GeoCoordinate(latitude, longitude);

        return storeGeo.GetDistanceTo(givenGeo);
    }
}

