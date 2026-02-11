using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.RequestParameters;
using ECommerceAPI.Application.ViewModels.Products;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistance.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
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
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).Skip(pagination.Page * pagination.Size).Take(pagination.Size); // Pagination işleminde Skip ve Take kullanarak sayfalama yapıyoruz. Skip ile atlanacak kayıt sayısını, Take ile de alınacak kayıt sayısını belirtiyoruz. Örneğin skip kısmında page 3 ve size 10 ise, 3*10=30 kayıt atlanacak ve sonraki 10 kayıt alınacak. Böylece 4. sayfa verileri getirilmiş olacak.

            return Ok(new
            {
                totalCount,
                products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Stock = model.Stock,
                Price = model.Price
            });

            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);

            product.Name = model.Name;
            product.Stock = model.Stock;
            product.Price = model.Price;

            await _productWriteRepository.SaveAsync();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {

            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}
