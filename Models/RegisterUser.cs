using System.ComponentModel.DataAnnotations;
using System;

namespace auctionBoard.Models
{
    public class RegisterUser : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [RegularExpression("^[a-zA-Z]+$")]
        public string Firstname { get; set; }
        [Required]
        [MinLength(2)]
        [RegularExpression("^[a-zA-Z]+$")]
        public string Lastname { get; set; }
        [Required]
        [MinLength(3)]
        public string Username { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation must match!!!")]
        public string Passwordconfirmation { get; set;}
    }
}