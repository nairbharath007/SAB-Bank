using E_Bank.Dto;
using E_Bank.Exceptions;
using E_Bank.Models;
using E_Bank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace E_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;


        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpGet("passbook")] 
        public IActionResult PassBook(int id)
        {
           var matched= _customerService.ViewPassBook(id);

            if (matched != null)
            {
                return Ok(matched);
            }

            throw new UserNotFoundException("Cannot find the match id");

        }




        private CustomerDto ModelToDto(Customer customer)
        {
            return new CustomerDto()
            {
               CustomerId = customer.CustomerId,
               FirstName = customer.FirstName,
               LastName = customer.LastName,
               Email = customer.Email,
               IsActive = customer.IsActive,
               CountAccounts = customer.Accounts!=null? customer.Accounts.Count():0,
               UserId = customer.UserId,
               Gender = customer.Gender,
               ContactDetails = customer.ContactDetails,
               State = customer.State,
              
            };
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            List<CustomerDto> result = new List<CustomerDto>();
          var DataList=  _customerService.GetAll();

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

        [HttpGet("{id:int}")]//, Authorize(Roles = "Admin")
        public IActionResult Get(int id)
        {
            var CustomerData = _customerService.GetById(id);

            if(CustomerData != null)
            {
                return Ok(CustomerData);
            }
            throw new UserNotFoundException("Cannot find the match id");
           // return BadRequest("Your search Id NotFound");
        }


        private Customer ConvertoModel(CustomerDto customerDto)
        {
            return new Customer()
            {
                CustomerId=customerDto.CustomerId,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                
                Email = customerDto.Email,
                IsActive = customerDto.IsActive = true,
                UserId= customerDto.UserId,
                Gender=customerDto.Gender,
                ContactDetails = customerDto.ContactDetails,
                State=customerDto.State,


            };
        }

        [HttpPost("")]
        public IActionResult Post(CustomerDto customerDto) 
        {
            var customer = ConvertoModel(customerDto);
            var status= _customerService.Add(customer);

            if(status !=null) 
            {
                return Ok("Customer account created successfully. ");
            }
            return BadRequest("Cannot add Customer. Server error. ");
        }

        [HttpPut]

        public IActionResult Put(CustomerDto customerDto)
        {
            var Customer=_customerService.GetById(customerDto.CustomerId);

            if(Customer != null)
            {
                var modified=ConvertoModel(customerDto);
                _customerService.Update(modified);
                return Ok(modified);
            }
            throw new UserNotFoundException("Cannot find the match id for update");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
           var matched= _customerService.GetById(id);
            if(matched != null)
            {
                _customerService.Delete(matched);
                return Ok(matched);
            }
            throw new UserNotFoundException("Cannot find the match id");
        }





        [HttpPost("customerRegister")]//3 register

        public IActionResult Register(CustomerDto customerDto)
        {
            //checking existing user first registering
            try
            {
                var existingUser = _customerService.FindUser(customerDto.UserId);

                if (existingUser == null)
                {
                    var customer = ConvertoModel(customerDto);
                    var status = _customerService.Add(customer);
                    
                        return Ok(new ReturnMessage() { Message = "Customer Details Registered successfully. " });
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ReturnMessage() { Message = ex.Message });
            }
            throw new UserNotFoundException(" Customer is alredy Registerd");
        }




        [HttpGet("customerDataFetch/{id:int}")]
        public IActionResult CustomerDataFetch(int id)
        {
            var matchedCustomer = _customerService.GetCustomer(id);
            if (matchedCustomer == null)
            {
                throw new UserNotFoundException("No Customer Found");
            }

            return Ok(matchedCustomer);
        }








    }
}
