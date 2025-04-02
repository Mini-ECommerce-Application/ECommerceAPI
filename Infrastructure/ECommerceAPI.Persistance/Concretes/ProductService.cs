using ECommerceAPI.Application.Absractions;
using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistance.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
            => new() // Target type özelliği sayesinde bir değişkenin tipini belirtmek zorunda kalmadan, değişkenin tipini belirleyebiliriz.
            { 
                new () { Id = Guid.NewGuid(), Name = "Product 1", Price = 100, Stock = 100, CreatedDate = DateTime.Now },
                new () { Id = Guid.NewGuid(), Name = "Product 2", Price = 200, Stock = 100, CreatedDate = DateTime.Now },
                new () { Id = Guid.NewGuid(), Name = "Product 3", Price = 300, Stock = 100, CreatedDate = DateTime.Now },
                new () { Id = Guid.NewGuid(), Name = "Product 4", Price = 400, Stock = 100, CreatedDate = DateTime.Now },
                new () { Id = Guid.NewGuid(), Name = "Product 5", Price = 500, Stock = 100, CreatedDate = DateTime.Now },
            }; 
    }
}
