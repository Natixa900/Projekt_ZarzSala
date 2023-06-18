using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
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
using ProjektApp.Contener;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ProjektApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int IDEmployee = 1;
        #region DatabaseConnections
        private List<Data.Employees> GetEmployees()
        {
            using (var employees = new Data.RezerwacjeDBEntities())
            {
                return employees.Employees.ToList();
            }
        }
        //klienci
        private List<Data.Clients> GetClients()
        {
            using (var clients = new Data.RezerwacjeDBEntities())
            {
                return clients.Clients.ToList();
            }
        }
        //zamówienia
        private List<Data.Rooms> GetRooms()
        {
            using (var rooms = new Data.RezerwacjeDBEntities())
            {
                return rooms.Rooms.ToList();
            }
        }
        //reklamacje
        private List<Data.Reservation> GetReservation()
        {
            using (var reservation = new Data.RezerwacjeDBEntities())
            {
                return reservation.Reservation.ToList();
            }
        }


        #endregion

        #region SomeCrud
        //pokoje
        public void AddNewRooms(Data.Rooms newRoom)
        {
            using (var Rooms = new Data.RezerwacjeDBEntities())
            {

                Rooms.Rooms.Add(newRoom);
                Rooms.SaveChanges();
            }
        }
        public void DeleteRooms(Data.Rooms IDtoDelete)
        {
            using (var RoomList = new Data.RezerwacjeDBEntities())
            {
                var RoomToDelete = RoomList.Rooms.Find(IDtoDelete.IdRoom);
                RoomList.Rooms.Remove(RoomToDelete);
                RoomList.SaveChanges();
            }
           DataGridRooms.ItemsSource = GetRooms();
        }
        //pracownicy
        public void DeleteEmployee(Data.Employees IDtoDelete)
        {
            using (var EmployeeList = new Data.RezerwacjeDBEntities())
            {
                var EmployeeToDelete = EmployeeList.Employees.Find(IDtoDelete.EmployeeID);
              
                    EmployeeList.Employees.Remove(EmployeeToDelete);
                    EmployeeList.SaveChanges();
                
                
            }
            DataGridEmployee.ItemsSource = GetEmployees();
        }
        public void AddNewEmployee(Data.Employees newEmployee)
        {
            using (var Employee = new Data.RezerwacjeDBEntities())
            {

                Employee.Employees.Add(newEmployee);
                Employee.SaveChanges();
            }
        }
        //rezerwacje
        public void DeleteReservation(Data.Reservation IDtoDelete)
        {
            using (var Reservation = new Data.RezerwacjeDBEntities())
            {
                var REsevation = Reservation.Reservation.Find(IDtoDelete.ReservationID );
                Reservation.Reservation.Remove(REsevation);
                Reservation.SaveChanges();
            }
        }
        public void AddNewReserwation(Data.Reservation newReservation)
        {
            using (var Reservation = new Data.RezerwacjeDBEntities())
            {

                Reservation.Reservation.Add(newReservation);
                Reservation.SaveChanges();
            }
        }
        #endregion

        //interface
        public event PropertyChangedEventHandler PropertyChanged; protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        




        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            DataGridReservation.ItemsSource = GetReservation();
            DataGridEmployee.ItemsSource = GetEmployees();
            DataGridRooms.ItemsSource = GetRooms();

          
           


            ChangeTabCommand = new RelayCommand<string>(ChangeTab);

        }



        public ICommand ChangeTabCommand { get; }



        public void ChangeTab(string tabnum)
        {
            if (int.TryParse(tabnum, out int index))
            {
                PanelGlowny.SelectedIndex = index;
            }
        }

        private void Zaloguj_Click(object sender, RoutedEventArgs e)
        {
            var LoginExist = GetEmployees().FirstOrDefault(x => x.EmployeeFName == Login.Text);
            string searchLogin = "";
            string searchPass = "";
            if (LoginExist != null)
            {
                searchLogin = LoginExist.EmployeeFName;
                searchPass = LoginExist.Password;
            }



            if (Login.Text == searchLogin && haslo.Password == searchPass && Login.Text != string.Empty && haslo.Password != string.Empty)
            {
                IDEmployee = LoginExist.EmployeeID;
                MessageBox.Show("Zalogowano");
                ChangeTab("1");

                haslo.Password = string.Empty;

            }
            else
            {
                MessageBox.Show("niepoprawne dane lub puste pole");
            }

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #region sale
        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Dostep.SelectedValue == D)
                {
                    Data.Rooms rooms = new Data.Rooms();
                    rooms.RoomName = RoNumber.Text;
                    rooms.chairNumber = int.Parse(PojemnoscBox.Text);
                    rooms.equipment = WypBox.Text;
                    rooms.available = true;
                    rooms.Price = Cena.Text;
                   
                    AddNewRooms(rooms);
                    DataGridRooms.ItemsSource = GetRooms();
                    MessageBox.Show("Dodane");
                    SalaID.Text = string.Empty;
                    RoNumber.Text = string.Empty;
                    PojemnoscBox.Text = string.Empty;
                    WypBox.Text = string.Empty;
                    Cena.Text = string.Empty;
                    ChangeTab("3");
                }
                else
                {
                    Data.Rooms rooms = new Data.Rooms();
                    rooms.RoomName = RoNumber.Text;
                    rooms.chairNumber = int.Parse(PojemnoscBox.Text);
                    rooms.equipment = WypBox.Text;
                    rooms.available = false;
                    rooms.Price = Cena.Text;
                    AddNewRooms(rooms);
                    MessageBox.Show("Dodane");
                    SalaID.Text = string.Empty;
                    RoNumber.Text = string.Empty;
                    PojemnoscBox.Text = string.Empty;
                    WypBox.Text = string.Empty;
                    Cena.Text = string.Empty;
                    ChangeTab("3");
                }
            }
            catch (Exception)
            {


            }





        }

        private void Usun_Click(object sender, RoutedEventArgs e)
        {
            var selectedRoom = DataGridRooms.SelectedIndex;
            Data.Rooms rooms = new Data.Rooms();
            rooms.IdRoom = GetRooms()[selectedRoom].IdRoom;
            rooms.RoomName = GetRooms()[selectedRoom].RoomName;
            rooms.chairNumber = GetRooms()[selectedRoom].chairNumber;
            rooms.equipment = GetRooms()[selectedRoom].equipment;
            rooms.Price = GetRooms()[selectedRoom].Price;
            rooms.available = GetRooms()[selectedRoom].available;

            DeleteRooms(rooms);
            DataGridRooms.ItemsSource = GetRooms();

        }
        #endregion

        #region PRacownicy
        private void UsunPracownika_Click(object sender, RoutedEventArgs e)
        {
            var selecteUser = DataGridEmployee.SelectedIndex;
            Data.Employees employees = new Data.Employees();
            employees.EmployeeFName = GetEmployees()[selecteUser].EmployeeFName;
            employees.EmployeeID = GetEmployees()[selecteUser].EmployeeID;
            employees.EmployeeLName = GetEmployees()[selecteUser].EmployeeLName;
            employees.Mail = GetEmployees()[selecteUser].Mail;
            employees.Password = GetEmployees()[selecteUser].Password;
            employees.Phone = GetEmployees()[selecteUser].Phone;
            employees.IsAdmin = GetEmployees()[selecteUser].IsAdmin;


            DeleteEmployee(employees);
            MessageBox.Show("Pracownik usunięty ");

            DataGridEmployee.ItemsSource = GetEmployees();



        }

        private void DodajPR_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Admin.SelectedValue == y)
                {
                   
                    Data.Employees employees = new Data.Employees();
                    employees.EmployeeFName = UName.Text;
                    employees.EmployeeLName = ULName.Text;
                    employees.Mail = Umail.Text;
                    employees.Password = UPS.Text;
                    employees.IsAdmin = true;
                    
                  
                    AddNewEmployee(employees);
                    DataGridEmployee.ItemsSource = GetEmployees();
                    
                    MessageBox.Show("Dodane");
                    UName.Text = string.Empty;
                    ULName.Text = string.Empty;
                    UPS.Text = string.Empty;
                    Umail.Text = string.Empty;

                    ChangeTab("6");
                }
                else
                {
                    Data.Employees employees = new Data.Employees();
                    employees.EmployeeFName = UName.Text;
                    employees.EmployeeLName = ULName.Text;
                    employees.Mail = Umail.Text;
                    employees.Password = UPS.Text;
                    employees.IsAdmin = false;
                    AddNewEmployee(employees);
                    DataGridEmployee.ItemsSource = GetEmployees();
                    MessageBox.Show("Dodane");
                    UName.Text = string.Empty;
                    UPS.Text = string.Empty;
                    Umail.Text = string.Empty;
                    ChangeTab("6");
                }
            }
            catch (Exception)
            {


            }
        }
        #endregion


        #region rezerwacje
        private void Reserwacja_Click(object sender, RoutedEventArgs e)
        {
            Data.Reservation reservation = new Data.Reservation();
            if (SalaName!=null && ClientI!=null)
            {
                reservation.ClientID=int.Parse(ClientI.Text);
                reservation.IdRoom = int.Parse(SalaName.Text);
                reservation.ReservationDate = Calen.SelectedDate.Value;
                reservation.EmployeeID = IDEmployee;
                if (wypododatkowe.IsChecked ==true)
                {
                    reservation.equipment = true;
                    AddNewReserwation(reservation);
                    DataGridReservation.ItemsSource = GetReservation();
                }
                else
                {
                    reservation.equipment = false;
                    AddNewReserwation(reservation);
                    DataGridReservation.ItemsSource = GetReservation();
                }

            }




        }

        private void UsunRezerwacje_Click(object sender, RoutedEventArgs e)
        {
            var selectedReservation = DataGridReservation.SelectedIndex;
            Data.Reservation reservation = new Data.Reservation();
            reservation.IdRoom = GetReservation()[selectedReservation].IdRoom;
           reservation.ReservationID = GetReservation()[selectedReservation].ReservationID;
            reservation.ReservationDate = GetReservation()[selectedReservation].ReservationDate;
            reservation.equipment = GetReservation()[selectedReservation].equipment;
            reservation.EmployeeID = GetReservation()[selectedReservation].EmployeeID;
            reservation.ClientID = GetReservation()[selectedReservation].ClientID;


           
            DeleteReservation(reservation);
            DataGridReservation.ItemsSource = GetReservation();
        }
        #endregion
    }
}
