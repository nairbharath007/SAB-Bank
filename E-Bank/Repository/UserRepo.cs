using E_Bank.Data;
using E_Bank.Dto;
using E_Bank.Models;

namespace E_Bank.Repository
{
    public class UserRepo:IUserRepo
    {
        MyContext _context;

        public UserRepo(MyContext context)
        {
            _context = context;
        }

        public string AddUser(UserDto userDto)
        {
            string passwordHash=BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            _context.usersTable.Add(new User()
            {
                UserName = userDto.UserName,
                Password=passwordHash,
                RoleId = userDto.RoleId,

            });
            _context.SaveChanges();
            return userDto.UserName;
        }


        public User FindUser(string username)
        {
            return _context.usersTable.Where(use => use.UserName == username).FirstOrDefault();
        }

        //after token setting

        public string GetRoleName(User user)
        {
            return _context.rolesTable.Where(rol => rol.RoleId == user.RoleId).FirstOrDefault().RoleName;
        }

        
        //public int GetRoleUserId(User user)
        //{
        //    return _context.usersTable.Where(us=>us.UserId==user)
        //}

    }
}
