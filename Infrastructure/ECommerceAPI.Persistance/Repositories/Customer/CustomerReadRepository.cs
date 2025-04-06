using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistance.Repositories
{
    // CustomerReadRepository sınıfı, ReadRepository sınıfından türetilmiştir. Bu sayede, CustomerReadRepository sınıfı, ReadRepository sınıfının tüm özelliklerini ve metodlarını kullanabilir. Ayrıca, ICustomerReadRepository arayüzünü implement ederek, ICustomerReadRepository arayüzündeki tüm metodları da implement etmesi gerekmektedir.
    public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
    {
        // Constructor Injection ile ECommerceAPIDbContext sınıfını alıyoruz. Bu sayede, ECommerceAPIDbContext sınıfının DbSet'lerini kullanarak, veritabanı işlemlerini gerçekleştirebiliriz.
        public CustomerReadRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
