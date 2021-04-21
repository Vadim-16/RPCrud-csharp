using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPCrud.Models
{
    [Table("users", Schema = "rpcrud")]
    public class User
    {
        [Key]
        public int ID { get; set; }

        
        [DataType(DataType.Text), StringLength(60, MinimumLength = 3), Required]
        public string Login { get; set; }

        [RegularExpression(@"^(?=(.*[A-Z]){2,})(?=.*[0-9])(?=(.*[!@#\?$\/%^&*()\-[\]\,\<\>__+.]){3,}).{8,}$", ErrorMessage = "Password must contain" +
            " at least 2 uppercase characters, at least 3 special characters, at least 1 number and be at least 8 characters long")]
        [DataType(DataType.Password), StringLength(60, MinimumLength = 8), Required]
        public string Password { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Country { get; set; }

        
        [DataType(DataType.Date), Display(Name = "Date of birth"), Required]
        public DateTime DateOfBirth { get; set; }

        [Required, Display(Name = "Agree with policy")]
        public bool CheckedAgreement { get; set; }
    }
}
