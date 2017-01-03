using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace auctionBoard.Models
{
    public class Item: BaseEntity
    {
        public int id { get; set; }
        [Required]
        [MinLength(4)]
        public string name { get; set; }
        [Required]
        [MinLength(11)]
        public string description { get; set; }
        [Required]
        [Range(0, Double.PositiveInfinity)]
        public int startingbid { get; set; }
        [Required]
        public DateTime endat { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }
        public int sellerid { get; set; }
        public User seller { get; set; }
        public int bidderid { get; set; }
        public User bidder { get; set; }
    }
}