using E_Bank.Models;
using static E_Bank.Repository.IRepository;

namespace E_Bank.Services
{
    public class DocService : IDocService
    {

        IRepository<Documents> _repository;


        public DocService(IRepository<Documents> repository)
        {
            _repository = repository;
           
        }




        public int Add(Documents customer)
        {
            return _repository.Add(customer);
        }

        public void Delete(Documents customer)
        {
            _repository.delete(customer);
        }

        public List<Documents> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Documents GetById(int id)
        {
            var tableName = _repository.Get();
            var DataFound = tableName.Where(ddoc => ddoc.CustomerId == id).OrderBy(doc => doc.DocumentId)
                .LastOrDefault();

            if (DataFound != null)
            {
                _repository.Detached(DataFound);
            }
            return DataFound;
        }

        public Documents Update(Documents customer)
        {
            return _repository.Update(customer);
        }
    }
}
