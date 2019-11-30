using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SAD.Model;

namespace SAD
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardRoom> CardRoom { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CardRoom>().HasOne(cr => cr.Card)
                .WithMany(c => c.AllowedRooms)
                .HasForeignKey(cr => cr.CardId);
            builder.Entity<CardRoom>().HasKey(cr => new { cr.CardId, cr.RoomId });
        }
    }
}
