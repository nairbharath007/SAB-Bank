using E_Bank.Data;
using E_Bank.Dto;
using E_Bank.Models;
using Microsoft.EntityFrameworkCore;
using static E_Bank.Repository.IRepository;

namespace E_Bank.Services
{
    public class CustomerService:ICustomerService
    {

        IRepository<Customer> _repository;
        IRepository<Account> _accountRepository;
        IRepository<Query> _queryRepository;
        IRepository<Documents> _documentsRepository;

        public CustomerService(IRepository<Customer> repository, IRepository<Account> account,
            IRepository<Query> queryRepository,IRepository<Documents> documentRep)
        {
            _repository = repository;
            _accountRepository = account;
            _queryRepository = queryRepository;
            _documentsRepository = documentRep;
        }


        public List<Customer> GetAll()
        {
            return _repository.GetAll().Where(cus => cus.IsActive)
                .Include(acnt => acnt.Accounts.Where(acnt => acnt.IsActive == true)).ToList();
        }

        public Customer GetById(int id)
        {
            var tableName = _repository.Get();
            var DataFound = tableName.Where(cus => cus.IsActive == true && cus.CustomerId == id)
                  .OrderBy(cus => cus.CustomerId)
                  .FirstOrDefault();
            if (DataFound != null)
            {
                _repository.Detached(DataFound);
            }
            return DataFound;
        }


        public int Add(Customer customer)
        {
            return _repository.Add(customer);

        }


        public Customer Update(Customer customer)
        {
            return _repository.Update(customer);

        }

        public void Delete(Customer customer)
        {
            _repository.delete(customer);
            var customerQuery = _accountRepository.Get();
            foreach (var item in customerQuery.Where(acn => acn.CustomerId == customer.CustomerId).ToList())
            {
                _accountRepository.delete(item);
            }
            var queryQuery=_queryRepository.Get();
            foreach (var item in queryQuery.Where(qr=>qr.CustomerId==customer.CustomerId).ToList() )
            {
                _queryRepository.delete(item);
            }
            var documentQuery=_documentsRepository.Get();
            foreach(var item2 in documentQuery.Where(doc=>doc.CustomerId==customer.CustomerId).ToList() )
            {
                _documentsRepository.delete(item2);
            }

           

        }

        public Customer ViewPassBook(int id)
        {
          return  _repository.Get().Where(pas=>pas.CustomerId==id && pas.IsActive)
                            .Include(Pass=>Pass.Accounts.Where(acn=>acn.IsActive==true )).FirstOrDefault();
                
        }


     

        public Customer FindUser(int userId)
        {
            var customerData= _repository.Get().Where(use => use.UserId == userId).FirstOrDefault();
            if (customerData != null && customerData.IsActive==false)
            {
                throw new InvalidOperationException("Admin Removed Your Account");
            }
            return customerData;
        }

        public Customer GetCustomer(int id)
        {
            return _repository.Get().Where(cus => cus.UserId == id && cus.IsActive==true).FirstOrDefault();
        }
    }
}
