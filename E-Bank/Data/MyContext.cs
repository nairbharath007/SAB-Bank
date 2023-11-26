using E_Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Bank.Data
{
    public class MyContext:DbContext
    {
      public  DbSet<Account> accountsTable { get; set; }

        public DbSet<User> usersTable { get; set; }

        public DbSet<Customer> customersTable { get; set; }

        public DbSet<TransactionClass> transactionsTable { get; set; }

        public DbSet<Role> rolesTable { get; set; }

        public DbSet<Query> queryTable { get; set; }

        public DbSet<Documents> documentsTable { get; set; }

        public DbSet<Admin> adminsTable { get; set; }

        public DbSet<FDInterestRate> fDInterestRatesTable { get; set; }

        public DbSet<FDAccount> fDAccountsTable { get; set; }

        //connection established it should set on program cs
        public MyContext(DbContextOptions<MyContext> options ):base(options)
        {

        }



    }
}
