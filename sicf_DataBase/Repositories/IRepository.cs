using sicf_Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int Id);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        void Update(T entity);
        Task Remove(int Id);
        void RemoveRange(List<T> entity);
        Task<List<T>> GetAll(Expression<Func<T, bool>> predicate);
        Task<int> CountRecord();
    }
}
