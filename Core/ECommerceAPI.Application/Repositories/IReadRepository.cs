using ECommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true); // Burada asenkron kullanmamamızın sebebi, IQueryable olarak veritabanına erişip sorgu gönderiyoruz.
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true); // Expression<Func<T, bool>> methodu verilen şarta uygun olan verileri geri döndürür.
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true); // Expression<Func<T, bool>> methodu verilen şarta uygun olan TEK bir veriyi geri döndürür. Name convention olarak GetSingleAsync yazdık ve Task kullanarak asenkron bir işlem yaptık.
        Task<T> GetByIdAsync(string id, bool tracking = true);
    }
}
