using AutoMapper;
using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.DataLayer.Models;
using GroceryFinder.DataLayer.Repositories.GroceryStoreRepository;

namespace GroceryFinder.BusinessLayer.Services.GroceryStoreService;

public class GroceryStoreService : IGroceryStoreService
{
    private readonly IGroceryStoreRepository _groceryStoreRepository;
    private readonly IMapper _mapper;

    public GroceryStoreService(IGroceryStoreRepository groceryStoreRepository, IMapper mapper)
    {
        _groceryStoreRepository = groceryStoreRepository;
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
}

