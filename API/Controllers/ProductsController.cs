using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using CORE.Entities;
using CORE.Interfaces;
using CORE.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IGenericRepository<ProductType> _typesRepo;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> productsRepo, 
            IGenericRepository<ProductBrand> brandsRepo,
            IGenericRepository<ProductType> typesRepo, IMapper mapper)
        {
            _productsRepo = productsRepo;
            _brandsRepo = brandsRepo;
            _typesRepo = typesRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            
            var products = await _productsRepo.ListAsync(spec);
            return Ok(_mapper
                .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            
            var product = await _productsRepo.GetEntityWithSpec(spec);

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            
            return Ok(await _brandsRepo.ListAllAsync());
        }
        
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            
            return Ok(await _typesRepo.ListAllAsync());
        }
    }
}