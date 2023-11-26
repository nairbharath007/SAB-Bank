using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Bank.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }


        public Role Role { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }



        public Admin Admin { get; set; }

        public Customer Customer { get; set; }

    }
}