using AutoMapper;
using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.DataLayer.Models;
using GroceryFinder.DataLayer.Repositories.ProductRepository;
using GroceryFinder.DataLayer.Repositories.UserAllergyRepository;

namespace GroceryFinder.BusinessLayer.Services.ProductService;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUserAllergyRepository _userAllergyRepository;
    private readonly IMapper _mapper;

    public ProductService(
        IProductRepository productRepository,
        IUserAllergyRepository userAllergyRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _userAllergyRepository = userAllergyRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto> AddProduct(ProductDto productDto)
    {
        Product product = _mapper.Map<Product>(productDto);
        product.Id = new Guid();
        var addedProduct = await _productRepository.Insert(product);
        await _productRepository.Save();
        return _mapper.Map<ProductDto>(addedProduct);
    }

    public async Task<IEnumerable<ProductDto>> GetAllProducts(Guid? userId)
    {
        var products = await _productRepository.GetAll();
        if (userId == null)
        {
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        var allergies = await _userAllergyRepository.GetUserAlergiesAsync(userId.Value);
        var allergenTypes = allergies.Select(a => a.AllergenType).Distinct().ToList();
        products = products.Where(p => !allergenTypes.Contains(p.AllergenType));
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> GetProduct(Guid id)
    {
        var product = await _productRepository.Get(id);
        return _mapper.Map<ProductDto>(product);
    }
}

