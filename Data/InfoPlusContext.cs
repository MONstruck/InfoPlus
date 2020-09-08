using InfoPlus.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoPlus.Data
{
    public class InfoPlusContext : DbContext
    {

        public InfoPlusContext()
        {
        }
        public InfoPlusContext(DbContextOptions<InfoPlusContext> options) : base(options)
        {
        }

        public virtual DbSet<InfomationModel> Information { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-L4P4V05;Database=InfoPlus;User Id=login;password=qwerty12;Trusted_Connection = True;MultipleActiveResultSets=true;");
            }
        }
    }
}
