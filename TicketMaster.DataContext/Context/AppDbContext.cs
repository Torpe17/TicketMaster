using Microsoft.EntityFrameworkCore;
using TicketMaster.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketMaster.DataContext.Context
{
    public class AppDbContext :DbContext
    {
        public DbSet<Film> Films { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseSqlServer("Name=ConnectionStrings:TicketMasterDatabase");
    }
}
