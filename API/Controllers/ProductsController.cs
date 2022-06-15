using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        


        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        //Se hace la injeccion de dependencias del repositorio el cual tiene la relacion con el  DbContext
        public ProductsController( IGenericRepository<Product> productsRepo, 
        IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo, IMapper mapper )
        {
            this._mapper = mapper;
            this._productTypeRepo = productTypeRepo;
            this._productBrandRepo = productBrandRepo;
            this._productsRepo = productsRepo;
           
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productsRepo.ListAsync(spec);
            return _mapper.Map<List<ProductToReturnDto>>(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
           var product =  await _productsRepo.GetEntityWithSpec(spec);
           return _mapper.Map<ProductToReturnDto>(product);
           
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> getProductBrands()
        {
            var brands =  await _productBrandRepo.ListAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductBrand>>> getProductTypes()
        {
            var types =  await _productTypeRepo.ListAllAsync();
            return Ok(types);
        }
        
    }
}