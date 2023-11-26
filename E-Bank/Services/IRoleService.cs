using E_Bank.Models;

namespace E_Bank.Services
{
    public interface IRoleService
    {
        public int Add(Role role);
        public List<Role> GetAll();

        public Role GetById(int id);

        public Role Update(Role customer);

        public void Delete(Role customer);
    }
}
