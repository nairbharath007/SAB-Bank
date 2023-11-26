using E_Bank.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Bank.Dto
{
    public class UserDto
    {
        // public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        // [ForeignKey("Role")]
        [Required]
        public int RoleId { get; set; }

      //public string email { get; set; }

        //public string ConfirmPassword { get; set; }

    }
}
