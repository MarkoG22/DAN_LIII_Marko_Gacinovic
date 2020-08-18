using Hotel.Commands;
using Hotel.Models;
using Hotel.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        MainWindow main;

        #region Properties
        // properties for username and password
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        private string userPassword;
        public string UserPassword
        {
            get { return userPassword; }
            set
            {
                userPassword = value;
                OnPropertyChanged("UserPassword");
            }
        }

        private tblManager manager;
        public tblManager Manager
        {
            get { return manager; }
            set { manager = value; }
        }
        #endregion

        // constructor
        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
        }

        #region Commands
        // command for the login button
        private ICommand logIn;
        public ICommand LogIn
        {
            get
            {
                if (logIn == null)
                {
                    logIn = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return logIn;
            }
        }

        private bool CanSaveExecute()
        {
            return true;
        }

        /// <summary>
        /// method for opening proper windows
        /// </summary>
        private void SaveExecute()
        {
            if (IsMaster(username, userPassword))
            {
                MasterView master = new MasterView();
                master.ShowDialog();
            }
            else if (IsEmployee(username, userPassword))
            {
                EmployeeView employee = new EmployeeView();
                employee.ShowDialog();
            }
            else if (IsManager(username, UserPassword))
            {
                try
                {
                    using (Zadatak_49Entities context = new Zadatak_49Entities())
                    {
                        tblAll user = (from y in context.tblAlls where y.Username == username && y.Pasword == userPassword select y).First();
                        manager = (from x in context.tblManagers where x.AllIDman == user.All_ID select x).First();

                        ManagerView managerView = new ManagerView(manager);
                        managerView.ShowDialog();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
            else
            {
                MessageBox.Show("Wrong username or password, please try again.");
            }
        }

        // command for closing the window
        private ICommand logout;
        public ICommand Logout
        {
            get
            {
                if (logout == null)
                {
                    logout = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return logout;
            }
        }

        /// <summary>
        /// method for closing the window
        /// </summary>
        private void CloseExecute()
        {
            try
            {
                main.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        private bool CanCloseExecute()
        {
            return true;
        }
        #endregion

        #region Methods

        /// <summary>
        /// method for checking Master inputs for opening proper window
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool IsMaster(string username, string password)
        {
            string[] lines = File.ReadAllLines(@"../../OwnerAccess.txt");
            List<string> list = new List<string>();

            string user = null;
            string pass = null;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "")
                {
                    continue;
                }
                list = lines[i].Split(':').ToList();
                if (i==0)
                {
                    user = list[1];
                }
                if (i==1)
                {
                    pass = list[1];
                }                
            }

            if (user == username && pass == password)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// method for checking Employee inputs
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool IsEmployee(string username, string password)
        {
            try
            {
                using (Zadatak_49Entities context = new Zadatak_49Entities())
                {
                    tblAll user = (from x in context.tblAlls where x.Username == username && x.Pasword == password select x).First();
                    tblEmploye employee = (from y in context.tblEmployes where y.AllIDemp == user.All_ID select y).First();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// method for checking Manager inputs
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool IsManager(string username, string password)
        {
            try
            {
                using (Zadatak_49Entities context = new Zadatak_49Entities())
                {
                    tblAll user = (from x in context.tblAlls where x.Username == username && x.Pasword == password select x).First();
                    tblManager manager = (from y in context.tblManagers where y.AllIDman == user.All_ID select y).First();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
        #endregion
    }
}
