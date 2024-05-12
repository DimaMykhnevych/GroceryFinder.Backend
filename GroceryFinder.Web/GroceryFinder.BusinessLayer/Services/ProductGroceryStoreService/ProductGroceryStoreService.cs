using AutoMapper;
using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.DataLayer.Models;
using GroceryFinder.DataLayer.Repositories.ProductGroceryStoreRepository;

namespace GroceryFinder.BusinessLayer.Services.ProductGroceryStoreService;

public class ProductGroceryStoreService : IProductGroceryStoreService
{
    private readonly IProductGroceryStoreRepository _productGroceryStoreRepository;
    private readonly IMapper _mapper;

    public ProductGroceryStoreService(IProductGroceryStoreRepository productGroceryStoreRepository, IMapper mapper)
    {
        _productGroceryStoreRepository = productGroceryStoreRepository;
        _mapper = mapper;
    }

    public async Task<ProductGroceryStoreDto> AddProductGroceryStore(ProductGroceryStoreDto productGroceryStoreDto)
    {
        ProductGroceryStore productGroceryStore = _mapper.Map<ProductGroceryStore>(productGroceryStoreDto);
        productGroceryStore.Id = new Guid();
        var addedProductGroceryStore = await _productGroceryStoreRepository.Insert(productGroceryStore);
        await _productGroceryStoreRepository.Save();
        return _mapper.Map<ProductGroceryStoreDto>(addedProductGroceryStore);
    }

    public async Task<IEnumerable<ProductGroceryStoreDto>> GetAllProductGroceryStores()
    {
        var productGroceryStores = await _productGroceryStoreRepository.GetAll();
        return _mapper.Map<IEnumerable<ProductGroceryStoreDto>>(productGroceryStores);
    }
}

