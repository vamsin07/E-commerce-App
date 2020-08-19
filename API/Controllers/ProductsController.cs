
using System.Collections.Generic;

using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;

using Microsoft.AspNetCore.Mvc;
using API.Dtos;
using System.Linq;
using AutoMapper;

namespace API.Controllers
{


  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {

    private readonly IGenericRepository<ProductBrand> _productBrandRepo;
    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IMapper _mapper;

    private readonly IGenericRepository<ProductType> _productTypeRepo;
    public ProductsController(IGenericRepository<Product> productsRepo,
    IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType>
    productTypeRepo, IMapper mapper)
    {
      _mapper = mapper;
      _productBrandRepo = productBrandRepo;
      _productsRepo = productsRepo;
      _productTypeRepo = productTypeRepo;



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
      return Ok(await _productBrandRepo.ListAllAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
      return Ok(await _productTypeRepo.ListAllAsync());
    }



  }


}