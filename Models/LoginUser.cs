using System.ComponentModel.DataAnnotations;
using System;

namespace auctionBoard.Models
{
    public class LoginUser : BaseEntity
    {
        [Required]
        [MinLength(3)]
        public string Username { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}