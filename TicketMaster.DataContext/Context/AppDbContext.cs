using Microsoft.EntityFrameworkCore;
using TicketMaster.DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace TicketMaster.DataContext.Context
{
    public class AppDbContext :DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        { }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //   => optionsBuilder.UseSqlServer("Name=ConnectionStrings:GergoDatabase");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(a => a.UserId);
        }
    }
}
