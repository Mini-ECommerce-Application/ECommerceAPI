using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistance.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity // BaseEntity kullanmamızın sebebi , tüm entity'lerimizin BaseEntity sınıfından türetilmiş olmasıdır. Bu sayede, ReadRepository sınıfı sadece BaseEntity sınıfından türetilmiş varlıklarla çalışabilir. Bu kısıtlama, ReadRepository sınıfının Generic bir sınıf olmasını sağlar ve sadece BaseEntity sınıfından türetilmiş varlıklarla çalışmasına izin verir.
    {
        private readonly ECommerceAPIDbContext _context;
        public ReadRepository(ECommerceAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>(); // DbSet<T> türünde bir property oluşturduk. Bu property, DbContext sınıfından gelen Set<T>() metodunu kullanarak, T türündeki varlıkların DbSet'ini döndürür. Bu sayede, T türündeki Entity'leri kullanarak ilgili işlemleri gerçekleştirebiliriz.

        public IQueryable<T> GetAll(bool tracking = true)
        {
            // tracking, veritabanındaki değişikliklerin izlenip izlenmeyeceğini belirler. Eğer tracking false ise, veritabanındaki değişiklikler izlenmez ve performans artışı sağlanır.
            var query = Table.AsQueryable();
            if (!tracking)
                query.AsNoTracking();
            return query;
        }


        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);
            if (!tracking)
                query.AsNoTracking();
            return query;
        }


        // GetSingleAsync() Metodumuz Task türü döndürüyor, bu nedenle await ve async anahtar kelimelerini kullanıyoruz. Bu metot, verilen şartı sağlayan tek bir varlığı asenkron olarak döndürür. Eğer şartı sağlayan birden fazla varlık varsa, ilkini döndürür. Eğer hiç varlık yoksa null döner.
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }

        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        //=> await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id)); // Reflection kullanmaktansa Markered Pattern'i kullanarak BaseEntity kısıtlaması uyguladık. Id'yi Guid olarak parse ettik.
        //=> await Table.FindAsync(Guid.Parse(id));
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id)); // Queryable olduğu için Marker pattern kullanıyoruz.
        }
    }
}
