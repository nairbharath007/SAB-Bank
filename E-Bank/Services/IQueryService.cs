using E_Bank.Dto;
using E_Bank.Models;

namespace E_Bank.Services
{
    public interface IQueryService
    {

        public int QueryRequsest(QueryDto queryDto);

        public List<Query> ShowQuery();

        public int QueryResponce(QueryResponceDto responceDto);


        public List<Query> GetAll();

        public Query GetById(int id);

       // public int Add(Query customer);

        public Query Update(Query customer);

        public void Delete(Query customer);


      public List<Query> GetCustomerById(int id);
    }
}
