using Hotel.Commands;
using Hotel.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        MainWindow main;
        
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

        // constructor
        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
        }

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

        private void SaveExecute()
        {
            if (IsMaster(username, UserPassword))
            {
                MasterView master = new MasterView();
                master.ShowDialog();
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
                //if (username == list[1] || password == list[1])
                //{
                //    return true;
                //}
            }

            if (user == username && pass == password)
            {
                return true;
            }
            return false;
        }
    }
}
