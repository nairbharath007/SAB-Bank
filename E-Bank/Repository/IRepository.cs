using E_Bank.Data;
using E_Bank.Models;

namespace E_Bank.Repository
{
    public interface IRepository
    {
        public interface IRepository<T>
        {
            public IQueryable<T> GetAll();

            public IQueryable<T> Get();
            public void Detached(T entity);

            public int Add(T entity);

            public T Update(T entity);

            public void delete(T entity);

            public void DenielAccount(T entity);


        }
    }
}
