
using E_Bank.Dto;
using E_Bank.Models;
using E_Bank.Repository;
using Microsoft.EntityFrameworkCore;
//using System.Security.Cryptography;
using static E_Bank.Repository.IRepository;


namespace E_Bank.Services
{
    
    public class AccountService : IAccountService
    {

        private IRepository<Account> _repository;
        private IRepository<FDAccount> _fdAccountRepository;
        private IRepository<TransactionClass> _transactionClassRepository;

        public AccountService(IRepository<Account> repository,
            IRepository<TransactionClass> transactionRep,
            IRepository<FDAccount> fdAccountRepository)
        {
            _repository = repository;
            _transactionClassRepository = transactionRep;
            _fdAccountRepository = fdAccountRepository;
        }

        public PageList<TransactionClass> GetAllAccount(PageParameters pageparameters)
        {

            var records = _transactionClassRepository.Get().Where(tr => tr.IsActive).ToList();
            return PageList<TransactionClass>.ToPagedList(records, pageparameters.PageNumber, pageparameters.PageSize);
        }


        //public List<TransactionClass> GetAllAccountsName()
        //{

        //    var records = _transactionClassRepository.Get().Where(tr => tr.IsActive).ToList();
        //    return records;
        //}
        public List<Account> GetAll()
        {
            return _repository.GetAll().Where(cus => cus.IsActive)
                     .Include(acnt => acnt.Transactions.Where(acnt => acnt.IsActive == true)).ToList();

        }



        //public List<Account> GetAllRequest()
        //{
        //    return _repository.GetAll()
        //        .Where(account => account.IsActive == false)
        //        .Include(account => account.Customer)
        //        .Where(account => account.Customer.IsActive == true)
        //        .ToList();
        //}

        public List<Account> GetAllRequest()
        {
            return _repository.GetAll()
                .Where(account => account.IsActive == false)
                .Include(account => account.Customer)
                    .ThenInclude(customer => customer.Documents)
                .Where(account => account.Customer.IsActive == true)
                .ToList();
        }



        public int ActivateRequest(int accountActivateDto)
        {
          var matched  =  _repository.Get()
                          .Where(acn=>acn.AccountNumber==accountActivateDto && acn.IsActive==false)
                         .FirstOrDefault();
            if(matched==null)
            {
                throw new InvalidOperationException("Account not found");
            }

            matched.IsActive=true;

          if(  _repository.Update(matched) == null )
            {
                throw new InvalidOperationException("Account not found");
            }

            return 1;


        }


        //account filter for transaction
        public List<Account> AccountFilter(int id)
        {
           var matched= _repository.Get()
                        .Where(acn => acn.AccountNumber == id && acn.IsActive == true)
                        .Include(acn => acn.Customer)  // Include the Customer navigation property
                        .Include(acn => acn.Transactions.Where(tran => tran.IsActive == true))
                        .ToList();

            return matched;
        }




        public int Add(Account account)
            {
          var save=  _repository.GetAll().Where(acc => acc.AccountType == "Savings").FirstOrDefault();
          var current = _repository.GetAll().Where(acc => acc.AccountType == "Current").FirstOrDefault();
          var Fdd = _repository.GetAll().Where(acc => acc.AccountType == "FD").FirstOrDefault();

           
            if( save.AccountType == account.AccountType )
            {
                account.IntrestRate = save.IntrestRate;
            }
            else if(current.AccountType == account.AccountType )
            {
                account.IntrestRate=current.IntrestRate;
            }
            else if(Fdd.AccountType == account.AccountType )
            {
                account.IntrestRate = Fdd.IntrestRate;
            }
            return _repository.Add(account);

        }

        public Account GetById(int id)
        {
            var account = _repository.Get()
                .Where(acn => acn.AccountNumber == id && acn.IsActive == true)
                .Include(acn => acn.Customer)
                    .Where(acn => acn.Customer.IsActive == true)  // Add this condition for the Customer
                .FirstOrDefault();

            if (account != null)
            {
                _repository.Detached(account);
            }
            return account;
        }


        public Account Update(Account account)
        {
          return _repository.Update(account);
        }

        public void Delete(Account account)
        {
            _repository.delete(account);
            var transactionQuery=_transactionClassRepository.Get();
            foreach (var transactionClass in transactionQuery.Where(tran=>tran.AccountId==account.AccountNumber))
            {
                _transactionClassRepository.delete(transactionClass);
            }

        }

        public int UpdateInterest(AccountIntrestUpdateDto accountIntrestUpdateDto)
        {

            

          var matched= _repository.GetAll().Where(acn=>acn.AccountType==accountIntrestUpdateDto.AccountType).ToList();

            if (matched.Count == 0)
            {
                return 0;
            }

            foreach (var account in matched)
            {
                account.IntrestRate=accountIntrestUpdateDto.InterestRate;
               
                _repository.Update(account);

            }
            return 1;

        }

        public List<Account> FindAccountId(int id)
        {
            var matchedAccount = _repository.Get()
                  .Where(acn => acn.CustomerId == id && acn.AccountType == "Savings" && acn.IsActive == true
                  || acn.CustomerId == id && acn.AccountType == "Current" && acn.IsActive == true)
                  .ToList();
            if (matchedAccount == null)
            {
                return null;
            }
            return matchedAccount;
        }



        //hard deniel

         public Account GetIdDeniel(int id)
        {
            var matchedAccount = _repository.Get().Where(acn=>acn.AccountNumber==id  &&   acn.IsActive==false).FirstOrDefault();
            if (matchedAccount == null)
            {
                return null;
            }
            return matchedAccount;
        }

        public void Deniel(Account account)
        {
            _repository.DenielAccount(account);
                }



        /*=====================================================*/



        public FDAccount FindFDAccount(int id)
        {
            var fdAccount = _fdAccountRepository.Get()
                     .Where(acn => acn.FDAccountId == id && acn.IsActive)
                     .OrderBy(acn => acn.FDAccountId)
                     .FirstOrDefault();

            /*if (account != null)
            {
                _repository.Detached(account);
            }*/
            return fdAccount;

        }


        public int AddFDAccount(FDAccount fdAccount)
        {
            return _fdAccountRepository.Add(fdAccount);
        }

        public List<FDAccount> GetAllRequestedFD()
        {
            return _fdAccountRepository.GetAll()
                              .Where(fdAcn => fdAcn.IsActive == false)
                              .ToList();
        }
        //

        public int ActivateFDRequest(int fdAccountId)
        {
            var inactivatedFDAcc = _fdAccountRepository.Get()
                            .Where(fdAcn => fdAcn.FDAccountId == fdAccountId && fdAcn.IsActive == false)
                           .FirstOrDefault();
            if (inactivatedFDAcc == null)
            {
                throw new InvalidOperationException("Account not found");
            }

            inactivatedFDAcc.IsActive = true;

            var senderAcc = _repository.Get().Where(acn => acn.AccountNumber == inactivatedFDAcc.AccountId && acn.IsActive == true)
                .FirstOrDefault();

            var senderAccBal = senderAcc.AccountBalance;

            var inactivatedFDAccBalance = inactivatedFDAcc.Amount;

            senderAcc.AccountBalance = senderAccBal - inactivatedFDAccBalance;

            _repository.Update(senderAcc); //updated acc bal


            if (_fdAccountRepository.Update(inactivatedFDAcc) == null)
            {
                throw new InvalidOperationException("Account not found");
            }


            return 1;


        }
    }
}
