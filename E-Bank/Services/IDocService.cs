using E_Bank.Models;

namespace E_Bank.Services
{
    public interface IDocService
    {
        public List<Documents> GetAll();

        public Documents GetById(int id);

        public int Add(Documents customer);

        public Documents Update(Documents customer);

        public void Delete(Documents customer);
    }
}
