using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandsRepository;
        private readonly IGenericRepository<ProductType> _productTypesRepository;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productsRepository, 
                                  IGenericRepository<ProductBrand> productBrandsRepository, 
                                  IGenericRepository<ProductType> productTypesRepository,
                                  IMapper mapper) 
        {
            _productsRepository = productsRepository;
            _productBrandsRepository= productBrandsRepository;
            _productTypesRepository= productTypesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productsRepository.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productsRepository.GetEntityWithSpec(spec);

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brands = await _productBrandsRepository.ListAllAsync();

            return Ok(brands);
        }

        [HttpGet("brands/{id}")]
        public async Task<ActionResult<ProductBrand>> GetProductBrand(int id)
        {
            var brand = await _productBrandsRepository.GetByIdAsync(id);

            return Ok(brand);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var types = await _productTypesRepository.ListAllAsync();

            return Ok(types);
        }

        [HttpGet("types/{id}")]
        public async Task<ActionResult<ProductType>> GetProductType(int id)
        {
            var type = await _productTypesRepository.GetByIdAsync(id);

            return Ok(type);
        }
    }
}
