using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TechnicalReport_Data.Models.FE;

namespace TechnicalReport_Data
{
    public class AppDBContext : DbContext
    {

    }

    public class FE_context : DbContext
    {
        public FE_context()
        {

        }

        public FE_context(DbContextOptions<FE_context> options) : base(options)
        {

        }
        public DbSet<FE_Context> Availability_raw { get; set; }
         public DbSet<FE_Quality> wafer_broken { get; set; }
         public DbSet<FE_Efficiency> WaferProduced { get; set; }
         public DbSet<ProcessDowntimes> ProcessDowntime { get; set; }
         public DbSet<Eq_list> Eq_id { get; set; }
        //public DbSet<Short_Eq_id> Short_Eq_Id { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("");
            }
        }
    }
}
