using AutoMapper;
using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.DataLayer.Models;
using GroceryFinder.DataLayer.Repositories.ProductRepository;

namespace GroceryFinder.BusinessLayer.Services.ProductService;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
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

    public async Task<IEnumerable<ProductDto>> GetAllProducts()
    {
        var products = await _productRepository.GetAll();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> GetProduct(Guid id)
    {
        var product = await _productRepository.Get(id);
        return _mapper.Map<ProductDto>(product);
    }
}

