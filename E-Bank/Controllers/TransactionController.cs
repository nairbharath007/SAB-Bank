using E_Bank.Dto;
using E_Bank.Exceptions;
using E_Bank.Models;
using E_Bank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace E_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this._transactionService = transactionService;
        }


        //deposite

        [HttpPost("deposit"), Authorize(Roles = "Customer")]
        public IActionResult DepositeAmount([FromBody]TransactionDto transactionDto)
        {
            var result = _transactionService.Deposite(transactionDto);

            if(result == 1)
            {
                return Ok(new ReturnMessage() { Message = "Deposited succesfully " });
            }

            return BadRequest("server error please try again after some time");
        }


        [HttpPost("Withdrawal"), Authorize(Roles = "Customer")]  
        public IActionResult WithdrawAmount([FromBody]TransactionDto transactionDto)
        {
            try
            {
                var result = _transactionService.Withdraw(transactionDto);
                if (result == 1)
                {
                    return Ok(new ReturnMessage() { Message = "Withdraw succesfully " });
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ReturnMessage() { Message = ex.Message });
            }
            return BadRequest("Bank Server error please try again after some time");
        }



        [HttpPost("TransferAmount")]
        public IActionResult TransferAmount([FromBody] TransactionDto transferDto)
        {
            try
            {
                var result = _transactionService.TransferAmount(transferDto);
                if (result == 1)
                {
                    return Ok(new ReturnMessage() { Message = "Amount Transferred successfully" });
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ReturnMessage() { Message = ex.Message });
            }

            throw new UserNotFoundException("Bank Server error please try again after some time ");



        }



        [HttpPost("DateFilter")]
        public IActionResult GetDate([FromBody]DateDto dateDto)
        {
            List<TransactionDto> transactionList = new List<TransactionDto>();

            var transactions = _transactionService.GetByDate(dateDto);

            if (transactions != null)
            {
                foreach (var transaction in transactions)
                {
                    var conTransaction = ModelToDto(transaction);
                    transactionList.Add(conTransaction);
                }
                return Ok(transactionList);

            }
            throw new UserNotFoundException("Cannot find any Transaction from given account Number");
        }

        [HttpGet("{date:DateTime}")]
        public IActionResult GetDate(DateTime date)
        {
            List<TransactionDto> transactionList = new List<TransactionDto>();

            var transactions = _transactionService.GetBysingleDate(date);

            if (transactions != null)
            {
                foreach (var transaction in transactions)
                {
                    var conTransaction = ModelToDto(transaction);
                    transactionList.Add(conTransaction);
                }
                return Ok(transactionList);

            }
            throw new UserNotFoundException("Cannot find any Transaction");
        }






        [HttpGet("showAllTransaction")]
        public IActionResult GetAll()
        {
            List<TransactionDto> transactionList = new List<TransactionDto>();

            var balance = _transactionService.GetAll();

            if (balance.Count == 0)
            {
                return BadRequest("No Transactions on Today");

            }
            foreach (var transaction in balance)
            {
                var conTransaction = ModelToDto(transaction);
                transactionList.Add(conTransaction);
            }
            return Ok(transactionList);

        }

        










        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var matched = _transactionService.GetById(id);
            if (matched != null)
            {
                _transactionService.Delete(matched);
                return Ok(matched);
            }
            return BadRequest("No match found to delete");
        }



        private TransactionClass ConvertoModel(TransactionDto transaction)
        {
            return new TransactionClass()
            {
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.TransactionType,
                TransactionAmount = transaction.TransactionAmount,
                Description = transaction.Description,
                State = "Success",
                IsActive = true,
                TransactionDate = DateTime.Now,
                AccountId = transaction.AccountId,

            };
        }


        private TransactionDto ModelToDto(TransactionClass transaction)
        {
            return new TransactionDto()
            {
                AccountId = transaction.AccountId,
                TransactionType = transaction.TransactionType,
                TransactionAmount = transaction.TransactionAmount,
                Description = transaction.Description,
               //State = transaction.State,
              //  IsActive = transaction.IsActive,
                TransactionDate = transaction.TransactionDate,
                ReceiverId = transaction.ReceiverId,
                TransactionId = transaction.TransactionId,
            };
        }



     

    }
}
