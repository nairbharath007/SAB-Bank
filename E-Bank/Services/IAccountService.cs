using E_Bank.Dto;
using E_Bank.Models;
using E_Bank.Repository;

namespace E_Bank.Services
{
    public interface IAccountService
    {
        public List<Account> GetAll();

        public List<Account> GetAllRequest();

        public int ActivateRequest(int id);

        public List<Account> AccountFilter(int id);//account flter for transaction

        public Account GetById(int id);

        public int Add(Account account);

        public Account Update(Account account);

        public void Delete(Account account);

        public int UpdateInterest(AccountIntrestUpdateDto accountIntrestUpdateDto);//update account intrest

        public List<Account> FindAccountId(int id);//customerId fetch account id

        public PageList<TransactionClass> GetAllAccount(PageParameters pageparameters);//pagination
                                                                                       //public List<TransactionClass> GetAllAccountsName();
        public Account GetIdDeniel(int id);

        public void Deniel(Account account);






        public FDAccount FindFDAccount(int id);
        public int AddFDAccount(FDAccount fdAccount);
        public List<FDAccount> GetAllRequestedFD();
        public int ActivateFDRequest(int fdAccountId);

        public Account FetchFDAccountByAccountId(int id);
    }
}
