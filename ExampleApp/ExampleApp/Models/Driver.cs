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

    [Table("Driver")]
    public partial class Driver : ICRUD, ISaveModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Driver()
        {
            Group = new HashSet<Group>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int passport_series_number { get; set; }

        [StringLength(50)]
        public string FIO { get; set; }

        public byte? age { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Group> Group { get; set; }


        public void Create()
        {
            using (LibContext2 context = new LibContext2())
            {
                context.Driver.Add(this);
                context.SaveChanges();
            }
        }
        public void Delete()
        {
            using (LibContext2 context = new LibContext2())
            {
                var driver = context.Driver.Where(x => x.passport_series_number == this.passport_series_number).FirstOrDefault();
                context.Driver.Remove(driver);
                context.SaveChanges();
            }
        }
        public void Read()
        {
            throw new NotImplementedException();
        }
        public void Update()
        {
            using (LibContext2 context = new LibContext2())
            {
                if (StaticClass.driver != null)
                {
                    var driver = context.Driver.Where(c => c.passport_series_number == StaticClass.driver.passport_series_number).FirstOrDefault();
                    if (driver == null) return;
                    CloningPart(driver, this);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex) { MessageBox.Show("При обновлении бд произошла ошибка! "); };
                }
            }
        }



        public void SaveModel()
        {
            StaticClass.driver = new Driver();
            AllCloning(StaticClass.driver, this);
        }
        private void AllCloning(Driver d1, Driver d2)
        {
            d1.passport_series_number = d2.passport_series_number;
            d1.FIO = d2.FIO;
            d1.age = d2.age;
            d1.Group = d2.Group;

        }
        private void CloningPart(Driver d1, Driver d2)
        {
            d1.FIO = d2.FIO;
            d1.age = d2.age;
        }
    }
}
