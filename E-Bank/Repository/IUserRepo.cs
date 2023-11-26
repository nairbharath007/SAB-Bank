using E_Bank.Dto;
using E_Bank.Models;

namespace E_Bank.Repository
{
    public interface IUserRepo
    {

        public User FindUser(string username);

        public string AddUser(UserDto userDto);

        public string GetRoleName(User user);
    }
}
