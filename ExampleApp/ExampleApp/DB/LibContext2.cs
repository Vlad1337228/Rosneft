using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ExampleApp.Model2
{
    public partial class LibContext2 : DbContext
    {
        public LibContext2()
            : base("name=LibContext2")
        {
        }

        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<Group> Group { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .Property(e => e.gos_number)
                .IsFixedLength();
        }
        
    }
}
