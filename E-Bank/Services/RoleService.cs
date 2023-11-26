using E_Bank.Models;
using static E_Bank.Repository.IRepository;

namespace E_Bank.Services
{
    public class RoleService : IRoleService
    {
        IRepository<Role> _repository;
       

        public RoleService(IRepository<Role> repository)
        {
            _repository = repository;
           
        }

        public int Add(Role role)
         {
                return _repository.Add(role);

         }

        public void Delete(Role customer)
        {
            _repository.delete(customer);
          
        }

        public List<Role> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Role GetById(int id)
        {
            var tableName = _repository.Get();
            var DataFound = tableName.Where(rol=>rol.RoleId == id)
                  .OrderBy(cus => cus.RoleId)
                  .FirstOrDefault();
            if (DataFound != null)
            {
                _repository.Detached(DataFound);
            }
            return DataFound;
        }

        public Role Update(Role customer)
        {
            return _repository.Update(customer);
        }
    }
}
