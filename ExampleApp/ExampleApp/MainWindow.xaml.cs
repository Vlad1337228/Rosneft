using ExampleApp.Controllers;
using ExampleApp.Interfaces;
using ExampleApp.Model2;
using ExampleApp.StaticClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExampleApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void ReadDB()
        {
            var dbSet = CheckTargetTable();
            if (dbSet == null)
            {
                MessageBox.Show("Выберите таблицу!");
                return;
            }
            var data = dbSet.ToListAsync();
            DataGridTable.ItemsSource = data.Result ;
        }
        private DbSet CheckTargetTable()
        {
            LibContext2 lb = new LibContext2();
            if (ComboBoxTables.Text == "Машина") return lb.Car;
            if (ComboBoxTables.Text == "Водитель") return lb.Driver;
            if (ComboBoxTables.Text == "Экипаж") return lb.Group;
            return null;
        }
        private void DeleteDataDB()
        {
            var p = DataGridTable.SelectedItem;
            if (p == null) { MessageBox.Show("Выберите элемент!");  return; }

            try
            {
                ICRUD prom = (ICRUD)p;
                prom.Delete();
            }
            catch
            {
                MessageBox.Show("Выбрана некорректная строчка!");
            }
        }

        //события на кнопки
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           MoreMethods.HiddenGridCreate(GridCreateDataDriver,GridCreateDataCar,GridCreateDataGroup);
            ReadDB();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MoreMethods.HiddenGridCreate(GridCreateDataDriver, GridCreateDataCar, GridCreateDataGroup);
            DeleteDataDB();
            Button_Click(sender, e);
        }
      
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           MoreMethods.VisibleGrid(GridCreateDataDriver, GridCreateDataCar, GridCreateDataGroup, ComboBoxTables);
        }
        private void ButtonCreateCar_Click(object sender, RoutedEventArgs e)
        {

            MoreMethods.CarCreate(TextBoxVIN.Text, TextBoxGosNumber.Text, TextBoxCountDoors.Text, TextBoxBrand.Text, TextBoxModel.Text);
            Button_Click(sender, e);
        }
        private void ButtonCreateDriver_Click(object sender, RoutedEventArgs e)
        {
            MoreMethods.DriverCreate( TextBoxFIO.Text, TextBoxAge.Text, TextBoxPassport);
            Button_Click(sender, e);
        }
        private void ButtonCreateGroup_Click(object sender, RoutedEventArgs e)
        {
            MoreMethods.GroupCreate(TextBoxGroupVIN.Text, TextBoxGroupPass.Text, TextBoxGroupBeforeDate.Text, TextBoxGroupAfterTime.Text);
            Button_Click(sender, e);
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (DataGridTable.SelectedItem != null )
                MoreMethods.UpdateData(DataGridTable);
            else
                MessageBox.Show("Выберите элемент!");
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ClientInfo cl = new ClientInfo();
            cl.ShowDialog();
        }
        private void DataGridTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (ISaveModel)DataGridTable.SelectedItem;
            item.SaveModel();
        }
        
    }
}
