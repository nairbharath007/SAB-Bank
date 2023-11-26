using E_Bank.Models;
using static E_Bank.Repository.IRepository;

namespace E_Bank.Services
{
    public class UserService : IUserService
    {
        IRepository<User> _repository;
        IRepository<Customer> _customerRepository;

        public UserService(IRepository<User> repository,IRepository<Customer> customer)
        {
            _repository = repository;
            _customerRepository = customer;
        }

        public List<User> GetAll()
        {
            return _repository.GetAll().ToList();
               
        }

        public User GetById(int id)
        {
            var tableName = _repository.Get();
            var DataFound = tableName.Where(cus=> cus.UserId == id).FirstOrDefault();
            if (DataFound != null)
            {
                _repository.Detached(DataFound);
            }
            return DataFound;
        }





        public int Add(User user)
        {
            return _repository.Add(user);

        }


        public User Update(User customer)
        {
            return _repository.Update(customer);

        }

        public void Delete(User customer)
        {
            _repository.delete(customer);
            var customerQuery = _customerRepository.Get();
            foreach (var item in customerQuery.Where(acn => acn.UserId == customer.UserId).ToList())
            {
                _customerRepository.delete(item);
            }

        }
    }
}
