using ECommerceAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity // Arayüzde kullanacağımız T türünü kısıtlama kullanarak kesinlikle bir class ve BaseEntity türünden bir class olacağını bildiriyoruz. Bu sayede, IRepository arayüzünü uygulayan sınıflar, BaseEntity sınıfından türetilmiş varlıklarla çalışabilirler. Bu kısıtlama, IRepository arayüzünün Generic bir arayüz olmasını sağlar ve sadece BaseEntity sınıfından türetilmiş varlıklarla çalışmasına izin verir.
    {

        DbSet<T> Table { get; } // Dbset'de table alınır ancak herhangi bir set işlemesi yapılmaz.

    }
}
