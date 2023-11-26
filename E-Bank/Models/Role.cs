using System.ComponentModel.DataAnnotations;

namespace E_Bank.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public List<User> User { get; set; }
    }
}
