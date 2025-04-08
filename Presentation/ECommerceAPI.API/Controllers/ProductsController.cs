using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistance.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;

        // todo : constructor hell fix
        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet] // action fonksiyonlarımızda http aksiyonlarını kullanmadığımız takdirde swagger'da hata alıyoruz.
        public async Task Get()
        {
            await _productWriteRepository.AddRangeAsync(new()
            {
                new () { Id = Guid.NewGuid(), Name = "Product 1", Price = 100, Stock = 10, CreatedDate = DateTime.UtcNow  },
                new () { Id = Guid.NewGuid(), Name = "Product 2", Price = 200, Stock = 20 , CreatedDate = DateTime.UtcNow },
                new () { Id = Guid.NewGuid(), Name = "Product 3", Price = 300, Stock = 30, CreatedDate = DateTime.UtcNow  },
            });
            await _productWriteRepository.SaveAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id);

            return Ok(product);
        }
    }
}
