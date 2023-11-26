using E_Bank.Dto;
using E_Bank.Exceptions;
using E_Bank.Models;
using E_Bank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace E_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        private AccountDto ModelToDto(Account account)
        {
            return new AccountDto()
            {
                AccountNumber = account.AccountNumber,
                AccountBalance=account.AccountBalance,
                AccountType = account.AccountType,
                IntrestRate = account.IntrestRate,
                IsActive = account.IsActive,
                OpenningDate = account.OpenningDate,
                CustomerId = account.CustomerId,
                TransactionsCount = account.Transactions !=null ? account.Transactions.Count() :0
            };
        }


        private FDAccountDto ModelToDto(FDAccount fDAccount)
        {
            return new FDAccountDto()
            {
                FDAccountId = fDAccount.FDAccountId,
                AccountId = fDAccount.AccountId,
                Amount = fDAccount.Amount,
                Duration = fDAccount.Duration,
                ROI = fDAccount.ROI,
                IsActive = fDAccount.IsActive,
                MaturityDate = fDAccount.MaturityDate,
                MaturityAmount = fDAccount.MaturityAmount,
                InterestReturns = fDAccount.InterestReturns,



            };
        }

        [HttpGet("activeAccounts")]//admin view all active account, Authorize(Roles = "Admin")
        public IActionResult GetAll()
        {
            List<AccountDto> result = new List<AccountDto>();
            var DataList = _accountService.GetAll();

            if (DataList.Count == 0)
            {
                throw new UserNotFoundException("Cannot find any Account ");
               // return BadRequest("No customer Added");
            }
            foreach (var Data in DataList)
            {
                result.Add(ModelToDto(Data));
            }
            return Ok(result);
        }


        [HttpGet("accounRequest")] //admin view all Not active account
        public IActionResult GetRequest()
        {
            //List<AccountDto> result = new List<AccountDto>();
            var DataList = _accountService.GetAllRequest();

            if (DataList.Count == 0)
            {
                throw new UserNotFoundException("Cannot find any Account ");
            }
           
            return Ok(DataList);
        }






        //used for admin activate account
        [HttpGet("activeId/{id:int}")]   //, Authorize(Roles = "Admin")
        public IActionResult ActivateAccount(int id)
        {
          var result= _accountService.ActivateRequest(id);
            if (result != null)
            {
              return  Ok(new ReturnMessage() { Message = "Account Request send succesfully " });
            }
            throw new UserNotFoundException("Cannot find the match id");

        }

        [HttpGet("TransactionFilter/{id:int}")] //admin search using id get specific customer 
        public IActionResult GetAccountTransactions(int id)
        {
            var matched = _accountService.AccountFilter(id);

            if (matched != null)
            {
                return Ok(matched);
            }
            throw new UserNotFoundException("Cannot find the match id");

        }


        [HttpGet("{id:int}")] //admin search using id get specific customer 
        public IActionResult Get(int id)
        {
            var matched = _accountService.GetById(id);

            if (matched != null)
            {
                return Ok(matched);
            }
            throw new UserNotFoundException("Cannot find the match id");

        }

        private Account DtoToModel(AccountDto account)
        {
            return new Account()
            {
                
                AccountBalance = account.AccountBalance,
                AccountType = account.AccountType,
                IntrestRate =0,
                IsActive =false,
                OpenningDate = DateTime.Now,
                CustomerId = account.CustomerId,
                

            };
        }


        private FDAccount DtoToModel(FDAccountDto fDAccountDto)
        {
            return new FDAccount()
            {

                /*FDAccountId = fDAccountDto.FDAccountId,*/
                AccountId = fDAccountDto.AccountId,
                Duration = fDAccountDto.Duration,
                IsActive = false,
                Amount = fDAccountDto.Amount,
                OpeningDate = DateTime.Now,
                ROI = fDAccountDto.ROI,
                MaturityAmount = fDAccountDto.MaturityAmount,
                MaturityDate = fDAccountDto.MaturityDate,
                InterestReturns = fDAccountDto.InterestReturns,

            };
        }



        [HttpPost("customerAccountRequest")]
        public IActionResult Post([FromBody]AccountDto accountDto)
        {
            //needed to check savings and customerid same exist error return
            var Converted=DtoToModel(accountDto);
            var sucess=  _accountService.Add(Converted);
            if(sucess !=null)
            {
              return  Ok(new ReturnMessage() { Message = "Account Request send succesfully " });
            }
            return BadRequest("Account creating error");
        }



        [HttpPut("")]
        public IActionResult Put(AccountDto accountDto)
        {
            var matched=_accountService.GetById(accountDto.AccountNumber);
            if (matched != null)
            {
                var account = DtoToModel(accountDto);
                _accountService.Update(account);
                return Ok(accountDto);
            }
            throw new UserNotFoundException("Cannot find any Account ");
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var matched = _accountService.GetIdDeniel(id);
            if (matched != null)
            {
                _accountService.Deniel(matched);
                return Ok(matched);
            }
            throw new UserNotFoundException("Cannot find any Account ");
        }






        //new added
        [HttpPost("AccountIntrestUpdate"), Authorize(Roles = "Admin")]
        public IActionResult updateIntrest([FromBody]AccountIntrestUpdateDto accountIntrestUpdateDto)
        {

            var status = _accountService.UpdateInterest(accountIntrestUpdateDto);

            if(status==0)
            {
                throw new UserNotFoundException("Cannot find any Account to update Interest ");
            }
            return Ok(new ReturnMessage() { Message = " succesfully interest updated" });



           
        }



        [HttpGet("pagination")]
        public IActionResult GetAllAccount([FromQuery] PageParameters pageParameters)
        {

            var banks = _accountService.GetAllAccount(pageParameters);

            var metaData = new
            {
                banks.TotalCount,
                banks.PageSize,
                banks.CurrentPage,
                // banks.TotalPages,
                banks.HasNext,
                banks.HasPrevious,

            };

            Response.Headers.Add("x-Pagination", JsonConvert.SerializeObject(metaData));
            var result = banks;
            return Ok(result);
        }


        //customer id passed and matched account find



        [HttpGet("customerIdAccountIdget/{id:int}")]
        public IActionResult FindAccountId(int id)
        {
          var matchedAccountId=  _accountService.FindAccountId(id);

            if (matchedAccountId!=null)
            { return Ok(matchedAccountId); }

            throw new UserNotFoundException("Cannot find any AccountId so no account added");
        }




        /*=========================================================================*/

        //Show all Fd accounts, for admin
        [HttpGet("getFDAccounts/{id:int}")]
        public IActionResult FindFDAccountId(int id)
        {
            var matchedAccountId = _accountService.FindFDAccount(id);

            if (matchedAccountId != null)
            { return Ok(matchedAccountId); }

            throw new UserNotFoundException("Cannot find any FD AccountId so no account added");
        }

        [HttpPost("requestFdAccounts")]

        public IActionResult AddFDAccount([FromBody] FDAccountDto fdAccountDto)
        {
            var Converted = DtoToModel(fdAccountDto);
            var sucess = _accountService.AddFDAccount(Converted);
            if (sucess != null)
            {
                return Ok(new ReturnMessage() { Message = "FDAccount Request send succesfully " });
            }
            return BadRequest("FDAccount creating error");
        }

        [HttpGet("getAllRequestedFD")]
        public IActionResult showRequetedFD()
        {
            List<FDAccountDto> result = new List<FDAccountDto>();
            var DataList = _accountService.GetAllRequestedFD();

            if (DataList.Count == 0)
            {
                throw new UserNotFoundException("Cannot find any Account ");
                // return BadRequest("No customer Added");
            }
            foreach (var Data in DataList)
            {
                result.Add(ModelToDto(Data));
            }
            return Ok(result);
        }

        [HttpGet("activateFDAccount/{id:int}")]
        public IActionResult approveFDAccount(int id)
        {
            var matchedAccountId = _accountService.ActivateFDRequest(id);

            if (matchedAccountId != null)
            {
                return Ok(matchedAccountId);
            }

            throw new UserNotFoundException("Cannot find any FD AccountId so no account added");
        }

    }
}
