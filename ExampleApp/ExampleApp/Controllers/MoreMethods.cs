using ExampleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using ExampleApp.Model2;
using System.Data.Entity;

namespace ExampleApp.Controllers
{
    public static class MoreMethods
    {
        public static void UpdateData(DataGrid dataGridTable)
        {
            var item = dataGridTable.SelectedItem;
            var prom = (ICRUD)item;
            prom.Update();

        }
        public static void HiddenGridCreate(Grid draiver, Grid car, Grid group)
        {
            draiver.Visibility = Visibility.Hidden;
            group.Visibility = Visibility.Hidden;
            car.Visibility = Visibility.Hidden;
        }
        public static void VisibleGrid(Grid draiver, Grid car, Grid group,ComboBox box)
        {
            if (box.Text == "Машина")
            {
                draiver.Visibility = Visibility.Hidden;
                group.Visibility = Visibility.Hidden;
                car.Visibility = Visibility.Visible;
                return;
            }
            if (box.Text == "Водитель")
            {
                car.Visibility = Visibility.Hidden;
                group.Visibility = Visibility.Hidden;
                draiver.Visibility = Visibility.Visible;
                return;
            }
            if (box.Text == "Экипаж")
            {
                car.Visibility = Visibility.Hidden;
                draiver.Visibility = Visibility.Hidden;
                group.Visibility = Visibility.Visible;
                return;
            }
            MessageBox.Show("Выберите таблицу!");
        }
        

        public static void CarCreate(string VIN, string gosNumber, string doors, string brand, string model)
        {
            Car car = new Car();
            var vin = VIN;
            if (vin.ValidatorString(x => x.Length <= 17 && x.Length >= 1))
                car.VIN = vin;
            else
            {
                MessageBox.Show("Вин номер введен неверно!");
                return;
            }
            try
            {
                car.gos_number = gosNumber;
                car.count_of_doors = string.Empty == doors ? byte.Parse(0.ToString()) : byte.Parse(doors);
                car.brand = brand;
                car.model = model;
            }
            catch
            {
                MessageBox.Show("Какие-то из данных некорректны!");
                return;
            }
            try
            {
                car.Create();
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так! Возможно такой экземпляр уже есть в базе.");
            }

        }
        public static void DriverCreate( string fio, string age,TextBox box)
        {
            Driver dr = new Driver();
            int pass = 0;
            try
            {
                pass = int.Parse(box.Text);
                if (pass.ValidatorInt(x => x <= 1000000000 && x >= 99999999))
                    dr.passport_series_number = pass;
                else
                    throw new OverflowException();
            }
            catch (OverflowException ex)
            { MessageBox.Show("Номер паспорта содержит недопустимые символы! В паспорте должно быть 9 цифр!"); return; }


            if (age == string.Empty)
            {
                dr.FIO = fio;
            }
            else
            {
                try
                {
                    dr.FIO = fio;
                    dr.age = byte.Parse(age);
                }
                catch
                {
                    MessageBox.Show("Какие-то из данных некорректны!");
                    return;
                }
            }

            try { dr.Create(); }
            catch { MessageBox.Show("Что-то пошло не так! Возможно такой экземпляр уже есть в базе."); }

        }
        public static void GroupCreate(string VIN, string passport, string dateBefore, string After)
        {
            Group gr = new Group();
            var vin = VIN;
            int pass = 0;
            DateTime d1 = new DateTime(), d2 = new DateTime();
            try
            {
                pass = int.Parse(passport);
            }
            catch { MessageBox.Show("Поле паспорта имеет неверные данные!"); };

            if (vin.ValidatorString(x => x.Length <= 17 && x.Length >= 1) &&
               pass.ValidatorInt(x => x <= 1000000000 && x >= 99999999))
            {
                gr.VIN = vin;
                gr.passport_series_number = pass;
            }
            else MessageBox.Show("Вин номер или данные паспорта введены неправильно.");

            try
            {
                d1 = ValidateDate(dateBefore);
                d2 = ValidateDate(After);
                gr.start_working = d1;
                gr.end_working = d2;
            }
            catch { MessageBox.Show("Формат даты некорректен"); return; }

            try
            {
                gr.Create();
            }
            catch (Exception ex)
            { MessageBox.Show("При создании произошла ошибка. Возможно в таблицах Car и Driver нет таких первичных ключей. " + '\n' + ex.Message); }

        }
        public static DateTime ValidateDate(string d1)
        {
            DateTime date1 = new DateTime(2000, 1, 1);
            if (string.Empty == d1)
            {
                return date1;
            }
            date1 = DateTime.Parse(d1);
            return date1;
        }
        

    }
}
