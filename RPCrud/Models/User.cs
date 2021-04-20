using System;
using System.ComponentModel.DataAnnotations;

namespace RPCrud.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        
        [DataType(DataType.Text), StringLength(60, MinimumLength = 3), Required]
        public string Login { get; set; }

        
        [DataType(DataType.Password), StringLength(60, MinimumLength = 3), Required]
        public string Password { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Country { get; set; }

        
        [DataType(DataType.Date), Display(Name = "Date of birth"), Required]
        public DateTime DateOfBirth { get; set; }

        [Required, Display(Name = "agree with policy")]
        public bool CheckedAgreement { get; set; }
    }
}
