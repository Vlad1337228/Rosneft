using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ExampleApp
{
    public partial class LibraryModel : DbContext
    {
        public LibraryModel()
            : base("name=LibModel")
        {
        }

        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<Group> Group { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasOptional(e => e.Group)
                .WithRequired(e => e.Car);
        }
    }
}
