using AutoMapper;
using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.DataLayer.Models;
using GroceryFinder.DataLayer.Repositories.UserAllergyRepository;

namespace GroceryFinder.BusinessLayer.Services.UserAllergyService;

public class UserAllergyService : IUserAllergyService
{
    private readonly IUserAllergyRepository _userAllergyRepository;
    private readonly IMapper _mapper;

    public UserAllergyService(IUserAllergyRepository userAllergyRepository, IMapper mapper)
    {
        _userAllergyRepository = userAllergyRepository;
        _mapper = mapper;
    }

    public async Task<UserAllergyDto> Get(Guid id)
    {
        var allergy = await _userAllergyRepository.Get(id);
        return _mapper.Map<UserAllergyDto>(allergy);
    }

    public async Task<IEnumerable<UserAllergyDto>> GetAll(Guid userId)
    {
        var allergies = await _userAllergyRepository.GetAll();
        allergies = allergies.Where(a => a.AppUserId == userId).ToList();
        return _mapper.Map<IEnumerable<UserAllergyDto>>(allergies);
    }

    public async Task<UserAllergyDto> Add(Guid userId, UserAllergyDto userAllergyDto)
    {
        var userAllergy = _mapper.Map<UserAllergy>(userAllergyDto);
        userAllergy.Id = Guid.NewGuid();
        userAllergy.AppUserId = userId;
        var addedAllergy = await _userAllergyRepository.Insert(userAllergy);
        await _userAllergyRepository.Save();
        return _mapper.Map<UserAllergyDto>(addedAllergy);
    }

    public async Task<UserAllergyDto> Update(Guid userId, UserAllergyDto userAllergyDto)
    {
        var userAllergy = _mapper.Map<UserAllergy>(userAllergyDto);
        userAllergy.AppUserId = userId;
        await _userAllergyRepository.Update(userAllergy);
        await _userAllergyRepository.Save();
        return _mapper.Map<UserAllergyDto>(userAllergy);
    }

    public async Task Delete(Guid id)
    {
        var allergy = await _userAllergyRepository.Get(id);
        _userAllergyRepository.Delete(allergy);
        await _userAllergyRepository.Save();
    }
}

