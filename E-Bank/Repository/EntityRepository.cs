using E_Bank.Data;
using Microsoft.EntityFrameworkCore;
using static E_Bank.Repository.IRepository;

namespace E_Bank.Repository
{
   
        public class EntityRepository<T> : IRepository<T> where T : class
        {
            private MyContext _context;
            private DbSet<T> _tableName;

            public EntityRepository(MyContext context)
            {
                _context = context;
                _tableName = _context.Set<T>();
            }

            public IQueryable<T> GetAll()
            {
                return _tableName.AsQueryable();
            }

            public IQueryable<T> Get()
            {
                return _tableName.AsQueryable();
            }

            public void Detached(T entity)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            public int Add(T entity)
            {
                _tableName.Add(entity);
                return _context.SaveChanges();//1 return
            }

            public T Update(T entity)
            {
                _tableName.Update(entity);
                _context.SaveChanges();
                return entity;
            }

            public void delete(T entity)
            {
                _context.Entry(entity).State = EntityState.Modified;
                var isActivated = entity.GetType().GetProperty("IsActive");
                if (isActivated != null)
                {
                    isActivated.SetValue(entity, false);
                    _tableName.Update(entity);
                }
                else
                {
                    _tableName.Remove(entity);
                }
                _context.SaveChanges();
            }


        public void DenielAccount(T entity)
        {
            _tableName.Remove(entity);
            _context.SaveChanges();
        }

    }
}
