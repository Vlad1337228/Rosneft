namespace ExampleApp.Model2
{
    using ExampleApp.Interfaces;
    using ExampleApp.StaticClasses;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Windows;

    [Table("Car")]
    public partial class Car : ICRUD, ISaveModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Car()
        {
            Group = new HashSet<Group>();
        }

        [Key]
        [StringLength(17)]
        public string VIN { get; set; }

        [StringLength(8)]
        public string gos_number { get; set; }

        public byte? count_of_doors { get; set; }

        [StringLength(50)]
        public string brand { get; set; }

        [StringLength(50)]
        public string model { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Group> Group { get; set; }


        public void Create()
        {
            using (LibContext2 context = new LibContext2())
            {
                context.Car.Add(this);
                context.SaveChanges();
            }
        }
        public void Delete()
        {
            using (LibContext2 context = new LibContext2())
            {
                var car = context.Car.Where(x => x.VIN.Equals(this.VIN)).FirstOrDefault();
                context.Car.Remove(car);
                context.SaveChanges();
            }
        }

        public void Read()
        {
            throw new NotImplementedException();
        }
        public void Update()
        {
            if (StaticClass.car != null)
            {
                using (LibContext2 context = new LibContext2())
                {
                    var car = context.Car.Where(c => c.VIN == StaticClass.car.VIN).FirstOrDefault();
                    if (car == null) return;
                    CloningPart(car, this);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex) { MessageBox.Show("Ошибка в обновлении бд. " + ex.Message); }
                }
            }
        }

        public void SaveModel()
        {
            StaticClass.car = new Car();
            AllCloning(StaticClass.car, this);

        }

        private void CloningPart(Car c1, Car c2)
        {
            c1.gos_number = c2.gos_number;
            c1.count_of_doors = c2.count_of_doors;
            c1.model = c2.model;
            c1.brand = c2.brand;
        }

        private void AllCloning(Car c1, Car c2)
        {
            c1.VIN = c2.VIN;
            c1.gos_number = c2.gos_number;
            c1.count_of_doors = c2.count_of_doors;
            c1.model = c2.model;
            c1.brand = c2.brand;
            c1.Group = c2.Group;
        }


    }
}
