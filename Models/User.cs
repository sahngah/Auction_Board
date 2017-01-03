using System;
using System.Collections.Generic;
namespace auctionBoard.Models
{
    public abstract class BaseEntity {}
    public class User: BaseEntity
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }
        public int wallet { get; set; }
        public List<Item> createditems { get; set; }
        public List<Item> biddeditems { get; set; }
        public User()
        {
            createditems = new List<Item>();
            biddeditems = new List<Item>();
        }

    }
}