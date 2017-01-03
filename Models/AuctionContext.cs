using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;
namespace auctionBoard.Models
{
    public class AuctionContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            base.OnModelCreating(ModelBuilder);
            ModelBuilder.Entity<Item>().HasOne(u => u.seller).WithMany(i => i.createditems);
            ModelBuilder.Entity<Item>().HasOne(u => u.bidder).WithMany(i => i.biddeditems);
        }
        public AuctionContext(DbContextOptions<AuctionContext> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }

    }
}