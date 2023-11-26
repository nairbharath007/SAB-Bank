using E_Bank.Models;

namespace E_Bank.Services
{
    public interface IUserService
    {
        public List<User> GetAll();
        public int Add(User user);

        public User GetById(int id);

        public User Update(User customer);
        public void Delete(User customer);

    }
}
