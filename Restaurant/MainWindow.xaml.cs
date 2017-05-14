using System;
using System.Collections.Generic;
using System.IO;
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

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Lunch> lunchList = new List<Lunch>();
        public Settings currentSettings = new Settings();



        public MainWindow()
        {
            
            InitializeComponent();

            HideEditInterface();
            HideLoginInterface();
            


            GetData();
            SetupSettings();

            ClearOldLunches();
            RewriteData();
            
            SetAvailibleTables();

            foreach (var item in lunchList)
            {
                listBox.Items.Add(item);
            }

        }

        public void ClearOldLunches()
        {
            
            List<Lunch> removeList = new List<Lunch>();
            foreach (Lunch item in lunchList)
            {
                if (!((currentSettings.availibleDates()).Contains(item.date)))
                    removeList.Add(item);
            }
            foreach (Lunch item in removeList)
            {
                lunchList.Remove(item);
                
            }
            
            
        }

        public void HideEditInterface()
        {
            returnButton.Visibility = Visibility.Hidden;
            listBox.Visibility = Visibility.Hidden;
            deleteButton.Visibility = Visibility.Hidden;
            searchBox.Visibility = Visibility.Hidden;
            searchButton.Visibility = Visibility.Hidden;
            searchLabel.Visibility = Visibility.Hidden;
        }

        public void HideStandardInterface()
        {
            timeBox.Visibility = Visibility.Hidden;
            dateBox.Visibility = Visibility.Hidden;
            label.Visibility = Visibility.Hidden;
            phoneLabel.Visibility = Visibility.Hidden;
            nameLabel.Visibility = Visibility.Hidden;
            nameBox.Visibility = Visibility.Hidden;
            phoneBox.Visibility = Visibility.Hidden;
            personsBox.Visibility = Visibility.Hidden;
            completeButton.Visibility = Visibility.Hidden;
            adminButton.Visibility = Visibility.Hidden;
        }

        public void ShowEditInterface()
        {
            returnButton.Visibility = Visibility.Visible;
            listBox.Visibility = Visibility.Visible;
            deleteButton.Visibility = Visibility.Visible;
            searchBox.Visibility = Visibility.Visible;
            searchButton.Visibility = Visibility.Visible;
            searchLabel.Visibility = Visibility.Visible;
        }

        public void ShowStandardInterface()
        {
            timeBox.Visibility = Visibility.Visible;
            dateBox.Visibility = Visibility.Visible;
            label.Visibility = Visibility.Visible;
            phoneLabel.Visibility = Visibility.Visible;
            nameLabel.Visibility = Visibility.Visible;
            nameBox.Visibility = Visibility.Visible;
            phoneBox.Visibility = Visibility.Visible;
            personsBox.Visibility = Visibility.Visible;
            completeButton.Visibility = Visibility.Visible;
            adminButton.Visibility = Visibility.Visible;
        }

        public void ShowLoginInterface()
        {
            returnButton.Visibility = Visibility.Visible;
            passwordBox.Visibility = Visibility.Visible;
            passwordButton.Visibility = Visibility.Visible;
            passwordLabel.Visibility = Visibility.Visible;
        }

        public void HideLoginInterface()
        {
            returnButton.Visibility = Visibility.Hidden;
            passwordBox.Visibility = Visibility.Hidden;
            passwordButton.Visibility = Visibility.Hidden;
            passwordLabel.Visibility = Visibility.Hidden;

        }

        public void SetAvailibleTables()
        {
            foreach (var item in currentSettings.availibleTime())
            {
                int a = currentSettings.tables[int.Parse(personsBox.SelectedValue.ToString())];
                foreach(var l in lunchList)
                {
                    if ((int.Parse(personsBox.SelectedValue.ToString()) == l.persons)&&(dateBox.SelectedValue.ToString() == l.date) && (currentSettings.getTimeNum(item) < currentSettings.getTimeNum(l.time) + currentSettings.getTimeNum(currentSettings.waitTime)) && (currentSettings.getTimeNum(l.time) < currentSettings.getTimeNum(item) + currentSettings.getTimeNum(currentSettings.waitTime)))
                        a -= 1;
                   
                }
                if (a<=0)
                {
                    timeBox.Items.Remove(item);
                    timeBox.SelectedIndex = 0;
                }
            }
        }



        public void SetupSettings()
        {
            using (FileStream fs = new FileStream(@"../../settings.txt", FileMode.Open, FileAccess.Read))
            {
                string[] data;
                Dictionary<int, int> tableList = new Dictionary<int,int>();

                StreamReader sr = new StreamReader(fs, Encoding.Default);

                currentSettings.pass = sr.ReadLine();
                currentSettings.openTime = sr.ReadLine();
                currentSettings.closeTime = sr.ReadLine();
                currentSettings.waitTime = sr.ReadLine();
                currentSettings.days = int.Parse(sr.ReadLine());
                while (!sr.EndOfStream)
                {
                    data = sr.ReadLine().Split(' ');
                    tableList[int.Parse(data[0])] = int.Parse(data[1]);
                }

                currentSettings.tables = tableList;

                sr.Close();
                fs.Close();

                

                foreach(var item in currentSettings.availibleTime())
                {
                    timeBox.Items.Add(item);
                }
                timeBox.SelectedItem = currentSettings.availibleTime()[0];

                foreach (var item in currentSettings.availibleDates())
                {
                    dateBox.Items.Add(item);
                }
                dateBox.SelectedItem = currentSettings.availibleDates()[0];

                foreach (var item in currentSettings.availiblePersons())
                {
                    personsBox.Items.Add(item);
                }
                personsBox.SelectedItem = currentSettings.availiblePersons()[0];

                

            }
            

        }

        public void GetData()
        {
            using (FileStream fs = new FileStream(@"../../data.txt", FileMode.Open, FileAccess.Read))
            {
                string[] data;
                Lunch lunchInput;
                StreamReader sr = new StreamReader(fs, Encoding.Default);

                while (!sr.EndOfStream)
                {
                    data = sr.ReadLine().Split('|');
                    lunchInput = new Lunch(data[0], data[1], data[2], data[3], int.Parse(data[4]));
                    lunchList.Add(lunchInput);
                }

                sr.Close();
                fs.Close();
            }
        }

        public void RewriteData()
        {
            using (FileStream fs = new FileStream(@"../../data.txt", FileMode.Create, FileAccess.Write))
            {

                StreamWriter sr = new StreamWriter(fs, Encoding.Default);

                foreach (Lunch l in lunchList)
                {
                    sr.Write(l.date + "|" + l.time + "|" + l.name + "|" + l.phone+"|"+l.persons);
                    sr.WriteLine();
                }

                sr.Close();
                fs.Close();
            }
        }


        private void adminButton_Click(object sender, RoutedEventArgs e)
        {

            HideStandardInterface();
            ShowLoginInterface();
            //ShowEditInterface();
        }



        

        private void completeButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if ((dateBox.SelectedValue.ToString().IndexOf('|')==-1) &&  (timeBox.SelectedValue.ToString().IndexOf('|')== -1)  &&  (nameBox.Text.IndexOf('|')==-1) && (phoneBox.Text.IndexOf('|') == -1) && (personsBox.SelectedValue.ToString().IndexOf('|') == -1))
                {
                    Lunch l = new Lunch(dateBox.SelectedValue.ToString(), timeBox.SelectedValue.ToString(), nameBox.Text, phoneBox.Text, int.Parse(personsBox.SelectedValue.ToString()));
                    lunchList.Add(l);
                    RewriteData();

                    listBox.Items.Clear();

                    foreach (var item in lunchList)
                    {
                        listBox.Items.Add(item);
                    }

                    nameBox.Clear();
                    phoneBox.Clear();
                }

                else
                {
                    throw new Exception();

                }
            }
            catch
            {
                MessageBox.Show("Ошибка введенных данных!");
            }
            timeBox.Items.Clear();
            foreach (var item in currentSettings.availibleTime())
            {
                timeBox.Items.Add(item);
            }
            timeBox.SelectedItem = currentSettings.availibleTime()[0];

            SetAvailibleTables();

        }



        private void dateBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
                timeBox.Items.Clear();
                foreach (var item in currentSettings.availibleTime())
                {
                    timeBox.Items.Add(item);
                }
                timeBox.SelectedItem = currentSettings.availibleTime()[0];
            try
            {
                SetAvailibleTables();
            }
            catch
            {

            }



        }

        private void timeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void personsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            timeBox.Items.Clear();
            foreach (var item in currentSettings.availibleTime())
            {
                timeBox.Items.Add(item);
            }
            timeBox.SelectedItem = currentSettings.availibleTime()[0];

            SetAvailibleTables();
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            HideEditInterface();
            HideLoginInterface();
            ShowStandardInterface();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Object d = listBox.SelectedItem;
            listBox.Items.Remove(d);
            lunchList.Remove((Lunch)d);
            RewriteData();

            timeBox.Items.Clear();
            foreach (var item in currentSettings.availibleTime())
            {
                timeBox.Items.Add(item);
            }
            timeBox.SelectedItem = currentSettings.availibleTime()[0];

            SetAvailibleTables();
        }

        private void passwordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (passwordBox.Password == currentSettings.pass)
                {
                    HideLoginInterface();
                    ShowEditInterface();
                    passwordBox.Clear();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Неверный пароль!");
            }
           
            
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Lunch l in lunchList)
            {
                if ((l.phone == searchBox.Text)||(l.name == searchBox.Text))
                {
                    searchLabel.Content = l.date + " "+l.time + " "+ l.name + " "+ l.phone + " "+ l.persons;
                    break;
                }
                else
                    searchLabel.Content = "Не найдено";
            }
        }
    }
}
