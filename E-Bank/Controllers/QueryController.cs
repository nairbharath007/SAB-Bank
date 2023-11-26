using E_Bank.Dto;
using E_Bank.Exceptions;
using E_Bank.Models;
using E_Bank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly IQueryService _querService;


        public QueryController(IQueryService queryService)
        {
            _querService = queryService;
        }



        [HttpPost("customerAskQuery"), Authorize(Roles = "Customer")]//customer Query send
        public IActionResult Post(QueryDto queryDto)
        {
            var matchedAccount = _querService.QueryRequsest(queryDto);

            if (matchedAccount != null)
            {
                return Ok(new ReturnMessage() { Message = "Query send succesfully " });
            }
            throw new UserNotFoundException("Cannot send Query internal error ");
        }



        private QueryDto ModelToDto(Query query)
        {
            return new QueryDto()
            {
                CustomerId = query.CustomerId,
                 QueryId = query.QueryId,
                QueryText = query.QueryText,
                QueryDate= query.QueryDate,


            };
        }

        [HttpGet("adminShowQuery"), Authorize(Roles = "Admin")]//admin show list of Query Requests

        public IActionResult GetRequestQuery()
        {
           List<QueryDto> query = new List<QueryDto>();
            var matched = _querService.ShowQuery();

            if (matched.Count== 0)
            {
                // return BadRequest("sorry no query is added");
                throw new UserNotFoundException("sorry no query is added");
            }
            foreach (var Data in matched)
            {
                query.Add(ModelToDto(Data));
            }
            return Ok(query);
        }


        [HttpPut("adminResponceQuery"), Authorize(Roles = "Admin")] //admin reply query
        public IActionResult Put(QueryResponceDto queryDto)
        {
            var matched= _querService.QueryResponce(queryDto);

            if(matched == 0)
            {
                throw new UserNotFoundException("Updation failed ");
            }
            return Ok(new ReturnMessage() { Message = "Replied succesfully " });
        }


        [HttpGet("")]
        public IActionResult GetAll()
        {
            List<QueryDto> result = new List<QueryDto>();
            var DataList = _querService.GetAll();

            if (DataList.Count == 0)
            {
                return BadRequest("No customer Added");
            }
            foreach (var Data in DataList)
            {
                result.Add(ModelToDto(Data));
            }
            return Ok(result);
        }

        [HttpGet("{id:int}"), Authorize(Roles = "Customer")]
        public IActionResult Get(int id)
        {
            var CustomerData = _querService.GetById(id);

            if (CustomerData != null)
            {
                return Ok(CustomerData);
            }
            throw new UserNotFoundException("Cannot find the match id");
        }

        [HttpGet("customerId/{id:int}")]
        public IActionResult GetQuery(int id)
        {
            var CustomerData = _querService.GetCustomerById(id);

            if (CustomerData.Count != 0)
            {
                return Ok(CustomerData);
            }
            throw new UserNotFoundException("Cannot find the match id");
        }

        private Query ConvertoModel(QueryDto queryDto)
        {
            return new Query()
            {
              
                CustomerId=queryDto.CustomerId,
                QueryId=queryDto.QueryId,
               // QueryStatus = queryDto.QueryStatus,
                QueryText = queryDto.QueryText,
              //  ReplyQuery = queryDto.ReplyQuery
                
            };
        }

    

      

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var matched = _querService.GetById(id);
            if (matched != null)
            {
                _querService.Delete(matched);
                return Ok(matched);
            }
            return BadRequest("cannot find id to delete");
        }



    }
}
