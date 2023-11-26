using E_Bank.Dto;
using E_Bank.Models;
using static E_Bank.Repository.IRepository;

namespace E_Bank.Services
{
    public class QueryService : IQueryService
    {


      IRepository<Query> _repository;
       

        public QueryService(IRepository<Query> repository)
        {
            _repository = repository;
        }


        public int QueryRequsest(QueryDto queryDto)
        {

            Query query = new Query()
            {
                CustomerId = queryDto.CustomerId,
                QueryDate = DateTime.Now,
                QueryStatus = false,
                QueryText = queryDto.QueryText,
                ReplyDate = DateTime.Now,
                ReplyQuery = ""
            };
            var success=  _repository.Add(query);

            if(success != null)
            {
                return 1;
            }
            return 0;
        }

        public List<Query> ShowQuery()
        {

         var  matchedList=  _repository.Get().Where(qu=>qu.QueryStatus==false).ToList();

            return matchedList;

        }

        public int QueryResponce(QueryResponceDto responceDto)
        {
          var matched=  _repository.Get()
                            .Where(qu => qu.CustomerId == responceDto.CustomerId && qu.QueryStatus == false)
                            .FirstOrDefault();
            if(matched!=null)
            {
                matched.ReplyQuery = responceDto.ReplyQuery;
                matched.QueryStatus = true;

                _repository.Update(matched);
                return 1;
            }
            return 0;
        }





        public void Delete(Query customer)
        {
            _repository.delete(customer);
           

        }

        public List<Query> GetAll()
        {
            return _repository.GetAll().Where(qu=>qu.QueryStatus==true)
                .ToList();
        }

        public Query GetById(int id)
        {
            var tableName = _repository.Get();
            var DataFound = tableName.Where(qu=>qu.QueryId == id).OrderBy(qu=>qu.QueryId)
                .FirstOrDefault();

            if (DataFound != null)
            {
                _repository.Detached(DataFound);
            }
            return DataFound;
        }

        public Query Update(Query customer)
        {
            return _repository.Update(customer);
        }

        public List<Query> GetCustomerById(int id)
        {
          return  _repository.Get().Where(qu => qu.CustomerId == id && qu.QueryStatus == true).ToList();
        }
    }
}
