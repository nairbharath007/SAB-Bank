using E_Bank.Models;

namespace E_Bank.Services
{
    public interface ICustomerService
    {

        public Customer ViewPassBook(int id);
        public List<Customer> GetAll();

        public Customer GetById(int id);

        public int Add(Customer customer);

        public Customer Update(Customer customer);

        public void Delete(Customer customer);

        //one time customer reg checking email
        public Customer FindUser(int userId);

        //userid get customer details
        public Customer GetCustomer(int id);

    }
}
