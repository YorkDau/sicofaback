using Microsoft.EntityFrameworkCore;
using sicf_Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace sicf_DataBase.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {

        protected DbSet<T> entities;

        public BaseRepository(DbContext dbContext)
        {
            this.entities = dbContext.Set<T>();
        }

        public async Task<List<T>> GetAll()
        {

            return await entities.ToListAsync();
        }

        public async Task<T> GetById(int Id)
        {

            return await entities.FindAsync(Id);
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {

            return await entities.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task Add(T entity)
        {

            await entities.AddAsync(entity);
        }

        public async Task Remove(int Id)
        {
            T entity = await GetById(Id);
            this.entities.Remove(entity);
        }

        public void RemoveRange(List<T> entity)
        {
            this.entities.RemoveRange(entity);
        }




        public void Update(T entity)
        {
           
                entities.Update(entity);
            
           
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate)
        {

            return await entities.Where(predicate).ToListAsync();
        }

        public async Task<int> CountRecord()
        {

            return await entities.CountAsync();
        }
    }
}
