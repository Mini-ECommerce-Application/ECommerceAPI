using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistance.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly ECommerceAPIDbContext _context;
        public WriteRepository(ECommerceAPIDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>(); // Burada bir nevi _context.Products, _context.Categories gibi bir yapı oluşturmuş olduk. Bu sayede, T türündeki varlıkların DbSet'ini döndürür.

        // todo : unit of work pattern eklenecek.
        public async Task<bool> AddAsync(T model)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added; // Eğer ekleme işlemi başarılıysa, entityEntry.State değeri EntityState.Added olur.
        }

        public async Task<bool> AddRangeAsync(List<T> datas)
        {
            await Table.AddRangeAsync(datas);
            return true;
        }

        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted; // Eğer silme işlemi başarılıysa, entityEntry.State değeri EntityState.Deleted olur.
        }

        public bool RemoveRange(List<T> datas)
        {
            Table.RemoveRange(datas);
            return true;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            T model = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
            return Remove(model); // Kendi remove() fonksiyonumuzu çağırdık, zaten return kısmında EntityState.Deleted kontrolü yapılıyor.
        }

        public bool Update(T model)
        {
            EntityEntry<T> entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified; // Eğer güncelleme işlemi başarılıysa, entityEntry.State değeri EntityState.Modified olur.
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync(); // Asenkron bir şekilde veritabanına kaydetme işlemi yapıyoruz. Bu sayede, veritabanına yapılan değişikliklerin kalıcı hale gelmesini sağlıyoruz. 
    }
}
