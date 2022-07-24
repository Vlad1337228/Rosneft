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

    [Table("Group")]
    public partial class Group : ICRUD, ISaveModel
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(17)]
        public string VIN { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int passport_series_number { get; set; }

        public DateTime? start_working { get; set; }

        public DateTime? end_working { get; set; }

        public virtual Car Car { get; set; }

        public virtual Driver Driver { get; set; }

        public void Create()
        {
            using (LibContext2 context = new LibContext2())
            {
                context.Group.Add(this);
                context.SaveChanges();
            }
        }

        public void Delete()
        {
            using (LibContext2 context = new LibContext2())
            {
                var group = context.Group.Where(x => x.VIN.Equals(this.VIN)).FirstOrDefault();
                context.Group.Remove(group);
                context.SaveChanges();
            }
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void SaveModel()
        {
            StaticClass.group = new Group();
            Cloning(StaticClass.group, this);
        }

        public void Update()
        {
            if (StaticClass.group != null)
            {
                using (LibContext2 context = new LibContext2())
                {
                    var gr = context.Group.
                             Where(c => c.VIN == StaticClass.group.VIN &&
                                        c.passport_series_number == StaticClass.group.passport_series_number)
                             .FirstOrDefault();
                    if (gr == null) return;
                    gr.start_working = start_working;
                    gr.end_working = end_working;
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex) { MessageBox.Show("¬веденные данные некорректны! " + ex.Message); }
                }
            }
        }
        private void Cloning(Group g1, Group g2)
        {
            g1.VIN = g2.VIN;
            g1.passport_series_number = g2.passport_series_number;
            g1.start_working = g2.start_working;
            g1.end_working = g2.end_working;
            g1.Car = g2.Car;
            g1.Driver = g2.Driver;

        }
    }
}
